using System.Collections.Generic;
using System.Threading;
using FluentAssertions;
using Xunit;

namespace ConcurrencyUtilities.Tests
{
    public class ConcurrencyTests
    {
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
            StripedLongAdder value = new StripedLongAdder();
            List<Thread> thread = new List<Thread>();

            for (int i = 0; i < threadCount; i++)
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
            value.GetValue().Should().Be(total * threadCount);
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
            ThreadLocalLongAdder value = new ThreadLocalLongAdder();
            List<Thread> thread = new List<Thread>();

            for (int i = 0; i < threadCount; i++)
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
            value.GetValue().Should().Be(total * threadCount);
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
            AtomicLong value = new AtomicLong();
            List<Thread> thread = new List<Thread>();

            for (int i = 0; i < threadCount; i++)
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
            value.GetValue().Should().Be(total * threadCount);
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
            PaddedAtomicLong value = new PaddedAtomicLong();
            List<Thread> thread = new List<Thread>();

            for (int i = 0; i < threadCount; i++)
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
            value.GetValue().Should().Be(total * threadCount);
        }
    }
}
