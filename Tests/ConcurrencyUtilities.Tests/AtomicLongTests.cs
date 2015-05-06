using System.Runtime.InteropServices;
using FluentAssertions;
using Xunit;

namespace ConcurrencyUtilities.Tests
{
    public class AtomicLongTests
    {
        private AtomicLong num = new AtomicLong();

        [Fact]
        public void AtomicLong_ShouldHaveCorrectSize()
        {
            AtomicLong.SizeInBytes.Should().Be(Marshal.SizeOf<AtomicLong>());
        }

        [Fact]
        public void AtomicLong_DefaultsToZero()
        {
            this.num.GetValue().Should().Be(0L);
        }

        [Fact]
        public void AtomicLong_CanBeCreatedWithValue()
        {
            new AtomicLong(5L).GetValue().Should().Be(5L);
        }

        [Fact]
        public void AtomicLong_CanSetAndReadValue()
        {
            this.num.SetValue(32);
            this.num.GetValue().Should().Be(32);
        }

        [Fact]
        public void AtomicLong_CanGetAndSet()
        {
            this.num.SetValue(32);
            this.num.GetAndSet(64).Should().Be(32);
            this.num.GetValue().Should().Be(64);
        }

        [Fact]
        public void AtomicLong_CanGetAndReset()
        {
            this.num.SetValue(32);
            this.num.GetAndReset().Should().Be(32);
            this.num.GetValue().Should().Be(0);
        }

        [Fact]
        public void AtomicLong_CanBeIncremented()
        {
            this.num.Increment().Should().Be(1L);
            this.num.GetValue().Should().Be(1L);
        }

        [Fact]
        public void AtomicLong_CanBeIncrementedMultipleTimes()
        {
            this.num.Increment().Should().Be(1L);
            this.num.GetValue().Should().Be(1L);

            this.num.Increment().Should().Be(2L);
            this.num.GetValue().Should().Be(2L);

            this.num.Increment().Should().Be(3L);
            this.num.GetValue().Should().Be(3L);
        }

        [Fact]
        public void AtomicLong_CanBeDecremented()
        {
            this.num.Decrement().Should().Be(-1L);
            this.num.GetValue().Should().Be(-1L);
        }

        [Fact]
        public void AtomicLong_CanBeDecrementedMultipleTimes()
        {
            this.num.Decrement().Should().Be(-1L);
            this.num.Decrement().Should().Be(-2L);
            this.num.Decrement().Should().Be(-3L);

            this.num.GetValue().Should().Be(-3L);
        }

        [Fact]
        public void AtomicLong_CanAddValue()
        {
            this.num.Add(7L).Should().Be(7L);
            this.num.GetValue().Should().Be(7L);
        }

        [Fact]
        public void AtomicLong_CanBeAssigned()
        {
            this.num.SetValue(10L);
            AtomicLong y = num;
            y.GetValue().Should().Be(10L);
        }

        [Fact]
        public void AtomicLong_CanGetAndAdd()
        {
            this.num.SetValue(10L);
            this.num.GetAndAdd(5L).Should().Be(10L);
            this.num.GetValue().Should().Be(15L);
        }

        [Fact]
        public void AtomicLong_CanGetAndIncrement()
        {
            this.num.SetValue(10L);

            this.num.GetAndIncrement().Should().Be(10L);
            this.num.GetValue().Should().Be(11L);

            this.num.GetAndIncrement(5L).Should().Be(11L);
            this.num.GetValue().Should().Be(16L);
        }

        [Fact]
        public void AtomicLong_CanGetAndDecrement()
        {
            this.num.SetValue(10L);

            this.num.GetAndDecrement().Should().Be(10L);
            this.num.GetValue().Should().Be(9L);

            this.num.GetAndDecrement(5L).Should().Be(9L);
            this.num.GetValue().Should().Be(4L);
        }

        [Fact]
        public void AtomicLong_CanCompareAndSwap()
        {
            this.num.SetValue(10L);

            this.num.CompareAndSwap(5L, 11L).Should().Be(false);
            this.num.GetValue().Should().Be(10L);

            this.num.CompareAndSwap(10L, 11L).Should().Be(true);
            this.num.GetValue().Should().Be(11L);
        }
    }
}
