using System.Runtime.InteropServices;
using FluentAssertions;
using Xunit;

namespace ConcurrencyUtilities.Tests
{
    public class AtomicIntegerTests
    {
        private AtomicInteger num = new AtomicInteger();

        [Fact]
        public void AtomicInteger_ShouldHaveCorrectSize()
        {
            AtomicInteger.SizeInBytes.Should().Be(Marshal.SizeOf<AtomicInteger>());
        }

        [Fact]
        public void AtomicInteger_DefaultsToZero()
        {
            this.num.GetValue().Should().Be(0);
        }

        [Fact]
        public void AtomicInteger_CanBeCreatedWithValue()
        {
            new AtomicInteger(5).GetValue().Should().Be(5);
        }

        [Fact]
        public void AtomicInteger_CanSetAndReadValue()
        {
            this.num.SetValue(32);
            this.num.GetValue().Should().Be(32);
        }

        [Fact]
        public void AtomicInteger_CanGetAndSet()
        {
            this.num.SetValue(32);
            this.num.GetAndSet(64).Should().Be(32);
            this.num.GetValue().Should().Be(64);
        }

        [Fact]
        public void AtomicInteger_CanGetAndReset()
        {
            this.num.SetValue(32);
            this.num.GetAndReset().Should().Be(32);
            this.num.GetValue().Should().Be(0);
        }

        [Fact]
        public void AtomicInteger_CanBeIncremented()
        {
            this.num.Increment().Should().Be(1);
            this.num.GetValue().Should().Be(1);
        }

        [Fact]
        public void AtomicInteger_CanBeIncrementedMultipleTimes()
        {
            this.num.Increment().Should().Be(1);
            this.num.GetValue().Should().Be(1);

            this.num.Increment().Should().Be(2);
            this.num.GetValue().Should().Be(2);

            this.num.Increment().Should().Be(3);
            this.num.GetValue().Should().Be(3);
        }

        [Fact]
        public void AtomicInteger_CanBeDecremented()
        {
            this.num.Decrement().Should().Be(-1);
            this.num.GetValue().Should().Be(-1);
        }

        [Fact]
        public void AtomicInteger_CanBeDecrementedMultipleTimes()
        {
            this.num.Decrement().Should().Be(-1);
            this.num.Decrement().Should().Be(-2);
            this.num.Decrement().Should().Be(-3);

            this.num.GetValue().Should().Be(-3);
        }

        [Fact]
        public void AtomicInteger_CanAddValue()
        {
            this.num.Add(7).Should().Be(7);
            this.num.GetValue().Should().Be(7);
        }

        [Fact]
        public void AtomicInteger_CanBeAssigned()
        {
            this.num.SetValue(10);
            AtomicInteger y = num;
            y.GetValue().Should().Be(10);
        }

        [Fact]
        public void AtomicInteger_CanGetAndAdd()
        {
            this.num.SetValue(10);
            this.num.GetAndAdd(5).Should().Be(10);
            this.num.GetValue().Should().Be(15);
        }

        [Fact]
        public void AtomicInteger_CanGetAndIncrement()
        {
            this.num.SetValue(10);

            this.num.GetAndIncrement().Should().Be(10);
            this.num.GetValue().Should().Be(11);

            this.num.GetAndIncrement(5).Should().Be(11);
            this.num.GetValue().Should().Be(16);
        }

        [Fact]
        public void AtomicInteger_CanGetAndDecrement()
        {
            this.num.SetValue(10);

            this.num.GetAndDecrement().Should().Be(10);
            this.num.GetValue().Should().Be(9);

            this.num.GetAndDecrement(5).Should().Be(9);
            this.num.GetValue().Should().Be(4);
        }

        [Fact]
        public void AtomicInteger_CanCompareAndSwap()
        {
            this.num.SetValue(10);

            this.num.CompareAndSwap(5, 11).Should().Be(false);
            this.num.GetValue().Should().Be(10);

            this.num.CompareAndSwap(10, 11).Should().Be(true);
            this.num.GetValue().Should().Be(11);
        }
    }
}
