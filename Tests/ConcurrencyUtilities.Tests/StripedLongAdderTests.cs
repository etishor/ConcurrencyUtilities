using FluentAssertions;
using Xunit;

namespace ConcurrencyUtilities.Tests
{
    public class StripedLongAdderTests
    {
        private StripedLongAdder num = new StripedLongAdder();

        [Fact]
        public void StripedLongAdder_DefaultsToZero()
        {
            this.num.GetValue().Should().Be(0L);
        }

        [Fact]
        public void StripedLongAdder_CanBeCreatedWithValue()
        {
            new StripedLongAdder(5L).GetValue().Should().Be(5L);
        }

        [Fact]
        public void StripedLongAdder_CanGetAndReset()
        {
            this.num.Add(32);
            long val = this.num.GetAndReset();
            val.Should().Be(32);
            this.num.GetValue().Should().Be(0);
        }

        [Fact]
        public void StripedLongAdder_CanBeIncremented()
        {
            this.num.Increment();
            this.num.GetValue().Should().Be(1L);

        }

        [Fact]
        public void StripedLongAdder_CanBeIncrementedMultipleTimes()
        {
            this.num.Increment();
            this.num.Increment();
            this.num.Increment();

            this.num.GetValue().Should().Be(3L);
        }

        [Fact]
        public void StripedLongAdder_CanBeDecremented()
        {
            this.num.Decrement();
            this.num.GetValue().Should().Be(-1L);

        }

        [Fact]
        public void StripedLongAdder_CanBeDecrementedMultipleTimes()
        {
            this.num.Decrement();
            this.num.Decrement();
            this.num.Decrement();

            this.num.GetValue().Should().Be(-3L);
        }

        [Fact]
        public void StripedLongAdder_CanAddValue()
        {
            this.num.Add(7L);
            this.num.GetValue().Should().Be(7L);
        }
    }
}
