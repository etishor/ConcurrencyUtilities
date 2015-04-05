using FluentAssertions;
using Xunit;

namespace ConcurrencyUtilities.Tests
{
    public class VolatileTests
    {
        [Fact]
        public void VolatileDouble_CanGetAndSetValue()
        {
            var value = new VolatileDouble(1.5);

            value.GetValue().Should().Be(1.5);
            value.SetValue(2.3);
            value.GetValue().Should().Be(2.3);
        }

        [Fact]
        public void VolatileLong_CanGetAndSetValue()
        {
            var value = new VolatileLong(1);

            value.GetValue().Should().Be(1);
            value.SetValue(2);
            value.GetValue().Should().Be(2);
        }
    }
}
