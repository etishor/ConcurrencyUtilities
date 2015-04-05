using System.Collections.Generic;
using System.Threading;
using FluentAssertions;
using Xunit;

namespace ConcurrencyUtilities.Tests
{
    public class AtomicLongTests
    {
        private AtomicLong num = new AtomicLong();

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
            long val = this.num.GetAndSet(64);
            val.Should().Be(32);
            this.num.GetValue().Should().Be(64);
        }

        [Fact]
        public void AtomicLong_CanGetAndReset()
        {
            this.num.SetValue(32);
            long val = this.num.GetAndReset();
            val.Should().Be(32);
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
            this.num.Increment().Should().Be(2L);
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

        

    }
}
