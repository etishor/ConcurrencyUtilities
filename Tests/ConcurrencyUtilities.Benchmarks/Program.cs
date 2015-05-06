


namespace ConcurrencyUtilities.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            AtomicLong[] data = new AtomicLong[0];

            BenchmarkRunner.DefaultMaxThreads = 8;
            BenchmarkRunner.DefaultTotalSeconds = 5;

            var strippedLongAdder = new StripedLongAdder(0L);
            var threadLocalAdder = new ThreadLocalLongAdder(0L);

            //BenchmarkRunner.Run("ThreadLocalLongAdder.Increment", () => threadLocalAdder.Increment());
            //var sz = ThreadLocalLongAdder.GetEstimatedFootprintInBytes(threadLocalAdder);

            BenchmarkRunner.Run("StripedLongAdder.Increment", () => strippedLongAdder.Increment());


            //BenchmarkRunner.Run("ThreadLocalLongAdder.Increment", () => threadLocalAdder.Increment());

            //BenchmarkRunner.Run("NoOp", () => { });

            ValueAdderIncrement();
        }

        private static void ValueAdderIncrement()
        {
            var atomicLong = new AtomicLong(0L);
            BenchmarkRunner.Run("AtomicLong.Increment", () => atomicLong.Increment());

            var threadLocalAdder = new ThreadLocalLongAdder(0L);
            BenchmarkRunner.Run("ThreadLocalLongAdder.Increment", () => threadLocalAdder.Increment());

            var strippedLongAdder = new StripedLongAdder(0L);
            BenchmarkRunner.Run("StripedLongAdder.Increment", () => strippedLongAdder.Increment());
        }
    }
}
