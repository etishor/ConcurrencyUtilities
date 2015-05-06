using System.Runtime.InteropServices;
using FluentAssertions;
using Xunit;

namespace ConcurrencyUtilities.Tests
{
    public class VolatileTests
    {
        [Fact]
        public void VolatileDouble_HasCorrectSize()
        {
            VolatileDouble.SizeInBytes.Should().Be(Marshal.SizeOf(typeof(VolatileDouble)));
        }

        [Fact]
        public void VolatileDouble_CanGetAndSetValue()
        {
            var value = new VolatileDouble(1.5);

            value.GetValue().Should().Be(1.5);
            value.SetValue(2.3);
            value.GetValue().Should().Be(2.3);
        }
    }
}
