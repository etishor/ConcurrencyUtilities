
namespace ConcurrencyUtilities.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run("NoOp", () => { });

            ValueAdderIncrement();
        }

        private static void ValueAdderIncrement()
        {
            var atomicLong = new AtomicLong(0L);
            BenchmarkRunner.Run("AtomicLong.ValueAdderIncrement", () => atomicLong.Increment());

            var threadLocalAdder = new ThreadLocalLongAdder(0L);
            BenchmarkRunner.Run("ThreadLocalLongAdder.ValueAdderIncrement", () => threadLocalAdder.Increment());

            var strippedLongAdder = new StripedLongAdder(0L);
            BenchmarkRunner.Run("StripedLongAdder.ValueAdderIncrement", () => strippedLongAdder.Increment());
        }
    }
}
