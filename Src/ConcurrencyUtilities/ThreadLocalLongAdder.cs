
using System.Threading;

namespace ConcurrencyUtilities
{
    /// <summary>
    /// This class is similar in funationality with the StripedLongAdder, but uses the ThreadLocal class to 
    /// keep a value for each thread. The GetValue method sums all the values and returns the result.
    /// 
    /// This class is a bit baster (in micro-benchmarks) than the StripedLongAdder for incrementing a value on multiple threads, 
    /// but it creates a ValueHolder instance for each thread that increments the value, not just for when contention is present. 
    /// Considering this, the StripedLongAdder might be a better solution in some cases (multiple threads, relatively low contention).
    /// </summary>
    public sealed class ThreadLocalLongAdder : ValueAdder<long>
    {
        private sealed class ValueHolder
        {
            public long Value;

            public long GetAndReset()
            {
                return Interlocked.Exchange(ref this.Value, 0L);
            }
        }

        private readonly ThreadLocal<ValueHolder> local = new ThreadLocal<ValueHolder>(() => new ValueHolder(), true);

        public ThreadLocalLongAdder() { }

        public ThreadLocalLongAdder(long value)
        {
            this.local.Value.Value = value;
        }

        public long GetValue()
        {
            long sum = 0;
            foreach (var value in this.local.Values)
            {
                sum += value.Value;
            }
            return sum;
        }

        public long GetAndReset()
        {
            long sum = 0;
            foreach (var val in this.local.Values)
            {
                sum += val.GetAndReset();
            }
            return sum;
        }

        public void Reset()
        {
            foreach (var value in this.local.Values)
            {
                value.Value = 0L;
            }
        }

        public void Add(long value)
        {
            this.local.Value.Value += value;
        }

        public void Increment()
        {
            this.local.Value.Value++;
        }

        public void Decrement()
        {
            this.local.Value.Value--;
        }

        public void Increment(long value)
        {
            Add(value);
        }

        public void Decrement(long value)
        {
            Add(-value);
        }
    }
}
