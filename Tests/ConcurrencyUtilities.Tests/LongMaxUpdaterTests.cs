using FluentAssertions;
using Xunit;

namespace ConcurrencyUtilities.Tests
{
    public class LongMaxUpdaterTests
    {
        private LongMaxUpdater num = new LongMaxUpdater();

        [Fact]
        public void LongMaxUpdater_DefaultsToLongMinValue()
        {
            this.num.Max().Should().Be(long.MinValue);
        }

        [Fact]
        public void LongMaxUpdater_CanBeCreatedWithValue()
        {
            new LongMaxUpdater(5L).Max().Should().Be(5L);
        }

        [Fact]
        public void LongMaxUpdater_CanGetAndReset()
        {
            this.num.Update(32);
            long val = this.num.MaxThenReset();

            val.Should().Be(32);
            this.num.Max().Should().Be(long.MinValue);
        }

        [Fact]
        public void LongMaxUpdater_CanBeUpdated_To_Max()
        {
            this.num.Update(3);
            this.num.Max().Should().Be(3L);
        }

        [Fact]
        public void LongMaxUpdater_CanBeIncrementedMultipleTimes()
        {
            this.num.Update(3);
            this.num.Update(5);
            this.num.Update(4);

            this.num.Max().Should().Be(5L);
        }
    }
}
