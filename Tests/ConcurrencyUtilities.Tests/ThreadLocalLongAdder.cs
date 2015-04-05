using FluentAssertions;
using Xunit;

namespace ConcurrencyUtilities.Tests
{
    public class ThreadLocalLongAdderTests
    {
        private ThreadLocalLongAdder num = new ThreadLocalLongAdder();

        [Fact]
        public void ThreadLocalLongAdder_DefaultsToZero()
        {
            this.num.GetValue().Should().Be(0L);
        }

        [Fact]
        public void ThreadLocalLongAdder_CanBeCreatedWithValue()
        {
            new ThreadLocalLongAdder(5L).GetValue().Should().Be(5L);
        }

        [Fact]
        public void ThreadLocalLongAdder_CanGetAndReset()
        {
            this.num.Add(32);
            long val = this.num.GetAndReset();
            val.Should().Be(32);
            this.num.GetValue().Should().Be(0);
        }

        [Fact]
        public void ThreadLocalLongAdder_CanBeIncremented()
        {
            this.num.Increment();
            this.num.GetValue().Should().Be(1L);

        }

        [Fact]
        public void ThreadLocalLongAdder_CanBeIncrementedMultipleTimes()
        {
            this.num.Increment();
            this.num.Increment();
            this.num.Increment();

            this.num.GetValue().Should().Be(3L);
        }

        [Fact]
        public void ThreadLocalLongAdder_CanBeDecremented()
        {
            this.num.Decrement();
            this.num.GetValue().Should().Be(-1L);

        }

        [Fact]
        public void ThreadLocalLongAdder_CanBeDecrementedMultipleTimes()
        {
            this.num.Decrement();
            this.num.Decrement();
            this.num.Decrement();

            this.num.GetValue().Should().Be(-3L);
        }

        [Fact]
        public void ThreadLocalLongAdder_CanAddValue()
        {
            this.num.Add(7L);
            this.num.GetValue().Should().Be(7L);
        }
    }
}
