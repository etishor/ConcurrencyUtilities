using System.Threading;

namespace ConcurrencyUtilities
{
    /// <summary>
    /// Atomic long value. Operations exposed on this class are performed using System.Threading.Interlocked class and are thread safe.
    /// For AtomicLong values that are stored in arrays PaddedAtomicLong is recommanded.
    /// </summary>
    public struct AtomicLong : AtomicValue<long>
    {
        private long value;

        public AtomicLong(long value)
        {
            this.value = value;
        }

        public long GetValue()
        {
            return Thread.VolatileRead(ref this.value);
        }

        public void SetValue(long value)
        {
            Thread.VolatileWrite(ref this.value, value);
        }

        public long Add(long value)
        {
            return Interlocked.Add(ref this.value, value);
        }

        public long Increment()
        {
            return Interlocked.Increment(ref this.value);
        }

        public long Increment(long value)
        {
            return Add(value);
        }

        public long Decrement()
        {
            return Interlocked.Decrement(ref this.value);
        }

        public long Decrement(long value)
        {
            return Add(-value);
        }

        public long GetAndReset()
        {
            return GetAndSet(0L);
        }

        public long GetAndSet(long newValue)
        {
            return Interlocked.Exchange(ref this.value, newValue);
        }

        public bool CompareAndSwap(long expected, long updated)
        {
            return Interlocked.CompareExchange(ref this.value, updated, expected) == expected;
        }
    }
}
