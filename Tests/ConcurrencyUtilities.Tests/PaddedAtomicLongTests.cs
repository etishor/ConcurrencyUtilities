using FluentAssertions;
using Xunit;

namespace ConcurrencyUtilities.Tests
{
    public class PaddedAtomicLongTests
    {
        private PaddedAtomicLong num = new PaddedAtomicLong();

        [Fact]
        public void PaddedAtomicLong_DefaultsToZero()
        {
            this.num.GetValue().Should().Be(0L);
        }

        [Fact]
        public void PaddedAtomicLong_CanBeCreatedWithValue()
        {
            new PaddedAtomicLong(5L).GetValue().Should().Be(5L);
        }

        [Fact]
        public void PaddedAtomicLong_CanSetAndReadValue()
        {
            this.num.SetValue(32);
            this.num.GetValue().Should().Be(32);
        }

        [Fact]
        public void PaddedAtomicLong_CanGetAndSet()
        {
            this.num.SetValue(32);
            long val = this.num.GetAndSet(64);
            val.Should().Be(32);
            this.num.GetValue().Should().Be(64);
        }

        [Fact]
        public void PaddedAtomicLong_CanGetAndReset()
        {
            this.num.SetValue(32);
            long val = this.num.GetAndReset();
            val.Should().Be(32);
            this.num.GetValue().Should().Be(0);
        }

        [Fact]
        public void PaddedAtomicLong_CanBeIncremented()
        {
            this.num.Increment().Should().Be(1L);
            this.num.GetValue().Should().Be(1L);

        }

        [Fact]
        public void PaddedAtomicLong_CanBeIncrementedMultipleTimes()
        {
            this.num.Increment().Should().Be(1L);
            this.num.Increment().Should().Be(2L);
            this.num.Increment().Should().Be(3L);

            this.num.GetValue().Should().Be(3L);
        }

        [Fact]
        public void PaddedAtomicLong_CanBeDecremented()
        {
            this.num.Decrement().Should().Be(-1L);
            this.num.GetValue().Should().Be(-1L);

        }

        [Fact]
        public void PaddedAtomicLong_CanBeDecrementedMultipleTimes()
        {
            this.num.Decrement().Should().Be(-1L);
            this.num.Decrement().Should().Be(-2L);
            this.num.Decrement().Should().Be(-3L);

            this.num.GetValue().Should().Be(-3L);
        }

        [Fact]
        public void PaddedAtomicLong_CanAddValue()
        {
            this.num.Add(7L).Should().Be(7L);
            this.num.GetValue().Should().Be(7L);
        }

        [Fact]
        public void PaddedAtomicLong_CanBeAssigned()
        {
            this.num.SetValue(10L);
            PaddedAtomicLong y = num;
            y.GetValue().Should().Be(10L);
        }
    }
}
