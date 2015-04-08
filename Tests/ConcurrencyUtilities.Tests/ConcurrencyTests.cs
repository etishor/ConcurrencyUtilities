using System.Collections.Generic;
using System.Threading;
using FluentAssertions;
using Xunit;

namespace ConcurrencyUtilities.Tests
{
    public class ConcurrencyTests
    {
        private static void ConcurrencyTest<T, U>(long total, int threadCount) where T : ValueAdder<U>, new()
        {
            var value = new T();
            var thread = new List<Thread>();

            for (var i = 0; i < threadCount; i++)
            {
                thread.Add(new Thread(() =>
                {
                    for (long j = 0; j < total; j++)
                    {
                        value.Increment();
                    }
                }));
            }

            thread.ForEach(t => t.Start());
            thread.ForEach(t => t.Join());

            var result = value.GetValue();
            if (result is int)
            {
                value.GetValue().Should().Be((int)total * threadCount);
            }
            else
            {
                value.GetValue().Should().Be(total * threadCount);
            }
        }

        [Theory]
        [
        InlineData(1000000, 16),
        InlineData(1000000, 8),
        InlineData(1000000, 4),
        InlineData(1000000, 2),
        InlineData(1000000, 1),
        ]
        public void Concurrency_StripedLongAdder_IsCorrectWithConcurrency(long total, int threadCount)
        {
            ConcurrencyTest<StripedLongAdder, long>(total, threadCount);
        }

        [Theory]
        [
        InlineData(1000000, 16),
        InlineData(1000000, 8),
        InlineData(1000000, 4),
        InlineData(1000000, 2),
        InlineData(1000000, 1),
        ]
        public void Concurrency_ThreadLocalLongAdder_IsCorrectWithConcurrency(long total, int threadCount)
        {
            ConcurrencyTest<ThreadLocalLongAdder, long>(total, threadCount);
        }

        [Theory]
        [
        InlineData(1000000, 16),
        InlineData(1000000, 8),
        InlineData(1000000, 4),
        InlineData(1000000, 2),
        InlineData(1000000, 1),
        ]
        public void Concurrency_AtomicLong_IsCorrectWithConcurrency(long total, int threadCount)
        {
            ConcurrencyTest<AtomicLong, long>(total, threadCount);
        }

        [Theory]
        [
        InlineData(1000000, 16),
        InlineData(1000000, 8),
        InlineData(1000000, 4),
        InlineData(1000000, 2),
        InlineData(1000000, 1),
        ]
        public void Concurrency_PaddedAtomicLong_IsCorrectWithConcurrency(long total, int threadCount)
        {
            ConcurrencyTest<PaddedAtomicLong, long>(total, threadCount);
        }

        [Theory]
        [
        InlineData(1000000, 16),
        InlineData(1000000, 8),
        InlineData(1000000, 4),
        InlineData(1000000, 2),
        InlineData(1000000, 1),
        ]
        public void Concurrency_AtomicInteger_IsCorrectWithConcurrency(int total, int threadCount)
        {
            ConcurrencyTest<AtomicInteger, int>(total, threadCount);
        }
    }
}
