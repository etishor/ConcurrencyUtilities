/*
 * Striped64 & LongAdder classes were ported from Java and had this copyright:
 * 
 * Written by Doug Lea with assistance from members of JCP JSR-166
 * Expert Group and released to the public domain, as explained at
 * http://creativecommons.org/publicdomain/zero/1.0/
 * 
 * Source: http://gee.cs.oswego.edu/cgi-bin/viewcvs.cgi/jsr166/src/jsr166e/Striped64.java?revision=1.8
 * 
 * This class has been ported to .NET by Jorrit Salverda and will retain the same license as the Java Version
 * 
 */

// ReSharper disable TooWideLocalVariableScope
// ReSharper disable InvertIf
// ReSharper disable ForCanBeConvertedToForeach
// ReSharper disable LoopCanBeConvertedToQuery

using System;

namespace ConcurrencyUtilities
{
        /**
     * One or more variables that together maintain a running {@code long}
     * maximum with initial value {@code long.MinValue}.  When updates
     * (method {@link #Update}) are contended across threads, the set of
     * variables may grow dynamically to reduce contention.  Method {@link
     * #Max} (or, equivalently, {@link #LongValue}) returns the current
     * maximum across the variables maintaining updates.
     *
     * <p>This class extends {@link Number}, but does <em>not</em> define
     * methods such as {@code Equals}, {@code HashCode} and {@code
     * CompareTo} because instances are expected to be mutated, and so are
     * not useful as collection keys.
     *
     */
    public class LongMaxUpdater : Striped64
    {
        /**
         * Version of max for use in retryUpdate
         */
        private Func<long, long, long> maxFunc = new Func<long, long, long>((l1, l2) => l1 > l2 ? l1 : l2);

        /**
            * Creates a new instance with initial maximum of {@code
            * Long.MIN_VALUE}.
            */
        public LongMaxUpdater()
        {
            Base.SetValue(long.MinValue);
        }

        public LongMaxUpdater(long value)
        {
            Base.SetValue(value);
        }

        /**
            * Updates the maximum to be at least the given value.
            *
            * @param x the value to update
            */
        public void Update(long value)
        {
            Cell[] @as;
            long b, v;
            int m;
            Cell a;

            if ((@as = this.Cells) != null || (b = Base.GetValue()) < value && !Base.CompareAndSwap(b, value))
            {
                var uncontended = true;
                if (@as == null || (m = @as.Length - 1) < 0 || (a = @as[GetProbe() & m]) == null || ((v = a.Value.GetValue()) < value && !(uncontended = a.Value.CompareAndSwap(v, value))))
                {
                    LongAccumulate(value, uncontended);
                }

            }
        }

        /**
            * Returns the current maximum.  The returned value is
            * <em>NOT</em> an atomic snapshot; invocation in the absence of
            * concurrent updates returns an accurate result, but concurrent
            * updates that occur while the value is being calculated might
            * not be incorporated.
            *
            * @return the maximum
            */
        public long Max()
        {
            var @as = this.Cells;
            long max = Base.GetValue();
            if ((@as = this.Cells) != null)
            {
                int n = @as.Length;
                long v;
                for (int i = 0; i < n; ++i)
                {
                    Cell a = @as[i];
                    if (a != null && (v = a.Value.GetValue()) > max)
                    {
                        max = v;
                    }
                }
            }
            return max;
        }

        /**
            * Resets variables maintaining updates to {@code Long.MIN_VALUE}.
            * This method may be a useful alternative to creating a new
            * updater, but is only effective if there are no concurrent
            * updates.  Because this method is intrinsically racy, it should
            * only be used when it is known that no threads are concurrently
            * updating.
            */
        public void Reset()
        {
            var @as = this.Cells; Cell a;
            Base.SetValue(long.MinValue);
            if (@as != null)
            {
                for (var i = 0; i < @as.Length; ++i)
                {
                    if ((a = @as[i]) != null)
                    {
                        a.Value.SetValue(long.MinValue);
                    }
                }
            }
        }

        /**
            * Equivalent in effect to {@link #max} followed by {@link
            * #reset}. This method may apply for example during quiescent
            * points between multithreaded computations.  If there are
            * updates concurrent with this method, the returned value is
            * <em>not</em> guaranteed to be the final value occurring before
            * the reset.
            *
            * @return the maximum
            */
        public long MaxThenReset()
        {
            var @as = this.Cells;
            long max = Base.GetAndSet(long.MinValue);
            if ((@as = this.Cells) != null)
            {
                int n = @as.Length;
                long v;
                for (int i = 0; i < n; ++i)
                {
                    Cell a = @as[i];
                    if (a != null && (v = a.Value.GetAndReset()) > max)
                    {
                        max = v;
                    }
                }
            }
            return max;
        }

        /**
            * Equivalent to {@link #max}.
            *
            * @return the maximum
            */
        public long LongValue()
        {
            return Max();
        }

        /**
            * Returns the {@link #max} as an {@code int} after a narrowing
            * primitive conversion.
            */
        public int IntValue()
        {
            return (int)Max();
        }

        /**
            * Returns the {@link #max} as a {@code float}
            * after a widening primitive conversion.
            */
        public float FloatValue()
        {
            return (float)Max();
        }

        /**
            * Returns the {@link #max} as a {@code double} after a widening
            * primitive conversion.
            */
        public double DoubleValue()
        {
            return (double)Max();
        }
    }
}
