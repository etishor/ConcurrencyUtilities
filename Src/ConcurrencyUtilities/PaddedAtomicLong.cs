
using System.Runtime.InteropServices;
using System.Threading;

namespace ConcurrencyUtilities
{
    /// <summary>
    /// Padded version of the AtomicLong to avoid false CPU cache sharing. Recommanded for cases where instances of 
    /// AtomicLong end up close to eachother in memory - when stored in an array for ex. 
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 64 * 2)]
    public struct PaddedAtomicLong : AtomicValue<long>
    {
        [FieldOffset(64)]
        private long value;

        public PaddedAtomicLong(long value)
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
