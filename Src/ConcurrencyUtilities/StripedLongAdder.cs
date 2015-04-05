/*
 * Striped64 & LongAdder classes were ported from Java and had this copyright:
 * 
 * Written by Doug Lea with assistance from members of JCP JSR-166
 * Expert Group and released to the public domain, as explained at
 * http://creativecommons.org/publicdomain/zero/1.0/
 * 
 * Source: http://gee.cs.oswego.edu/cgi-bin/viewcvs.cgi/jsr166/src/jsr166e/Striped64.java?revision=1.8
 * 
 * This class has been ported to .NET by Iulian Margarintescu and will retain the same license as the Java Version
 * 
 */

// ReSharper disable TooWideLocalVariableScope
// ReSharper disable InvertIf
// ReSharper disable ForCanBeConvertedToForeach
// ReSharper disable LoopCanBeConvertedToQuery
namespace ConcurrencyUtilities
{
    /// <summary>
    /// One or more variables that together maintain an initially zero sum.
    /// When updates are contended cross threads, the set of variables may grow dynamically to reduce contention.
    /// Method GetValue() returns the current total combined across the variables maintaining the sum.
    /// 
    /// This class is usually preferable to AtomicLong when multiple threads update a common sum that is used for purposes such
    /// as collecting statistics, not for fine-grained synchronization control.
    /// 
    /// Under low update contention, the two classes have similar characteristics. 
    /// But under high contention, expected throughput of this class is significantly higher, at the expense of higher space consumption.
    /// 
    /// </summary>
    public sealed class StripedLongAdder : Striped64, ValueAdder<long>
    {
        public StripedLongAdder() { }

        public StripedLongAdder(long value)
        {
            Add(value);
        }

        public long GetValue()
        {
            var @as = this.cells; Cell a;
            var sum = Base;
            if (@as != null)
            {
                for (var i = 0; i < @as.Length; ++i)
                {
                    if ((a = @as[i]) != null)
                        sum += a.Value;
                }
            }
            return sum;
        }

        public long GetAndReset()
        {
            var @as = this.cells; Cell a;
            var sum = GetAndResetBase();
            if (@as != null)
            {
                for (var i = 0; i < @as.Length; ++i)
                {
                    if ((a = @as[i]) != null)
                    {
                        sum += a.GetAndReset();
                    }
                }
            }
            return sum;
        }

        public void Reset()
        {
            var @as = this.cells; Cell a;
            Base = 0L;
            if (@as != null)
            {
                for (var i = 0; i < @as.Length; ++i)
                {
                    if ((a = @as[i]) != null)
                    {
                        a.Value = 0L;
                    }
                }
            }
        }

        public void Increment()
        {
            Add(1L);
        }

        public void Increment(long value)
        {
            Add(value);
        }

        public void Decrement()
        {
            Add(-1L);
        }

        public void Decrement(long value)
        {
            Add(-value);
        }

        public void Add(long value)
        {
            Cell[] @as;
            long b, v;
            int m;
            Cell a;
            if ((@as = this.cells) != null || !CompareAndSwapBase(b = Base, b + value))
            {
                var uncontended = true;
                if (@as == null || (m = @as.Length - 1) < 0 || (a = @as[GetProbe() & m]) == null || !(uncontended = a.Cas(v = a.Value, v + value)))
                {
                    LongAccumulate(value, uncontended);
                }
            }
        }
    }
}