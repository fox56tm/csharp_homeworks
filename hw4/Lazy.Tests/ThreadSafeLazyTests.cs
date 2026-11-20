using System.Reflection;
using Lazy.Core;

namespace Lazy.Tests;

public sealed class ThreadSafeLazyTests : ILazyContractTests
{
    private const int ConcurrentCallCount = 16;
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(10);

    protected override ILazy<T> CreateLazy<T>(Func<T> supplier) => new ThreadSafeLazy<T>(supplier);

    [Fact]
    public void Get_ConcurrentCalls_InvokeSupplierOnceAndReturnSameInstance()
    {
        // Arrange
        var expected = new object();
        var calls = 0;
        var lazy = new ThreadSafeLazy<object>(() =>
        {
            Interlocked.Increment(ref calls);
            Thread.Sleep(20);
            return expected;
        });
        var results = new object[ConcurrentCallCount];

        // Act
        RunConcurrently(index => results[index] = lazy.Get());

        // Assert
        Assert.Equal(1, calls);
        Assert.All(results, result => Assert.Same(expected, result));
    }

    [Fact]
    public void Get_ConcurrentCallsWithNullResult_InvokeSupplierOnceAndReturnNull()
    {
        // Arrange
        var calls = 0;
        var lazy = new ThreadSafeLazy<string?>(() =>
        {
            Interlocked.Increment(ref calls);
            Thread.Sleep(20);
            return null;
        });
        var results = new string?[ConcurrentCallCount];

        // Act
        RunConcurrently(index => results[index] = lazy.Get());

        // Assert
        Assert.Equal(1, calls);
        Assert.All(results, result => Assert.Null(result));
    }

    [Fact]
    public void Get_ConcurrentCallsWhenSupplierThrows_KeepConsistentValueAfterFailure()
    {
        // Arrange
        var expected = new object();
        var attempts = 0;
        var failures = 0;
        var lazy = new ThreadSafeLazy<object>(() =>
        {
            if (Interlocked.Increment(ref attempts) == 1)
                throw new InvalidOperationException("first attempt failed");

            Thread.Sleep(20);
            return expected;
        });
        var results = new object?[ConcurrentCallCount];

        // Act
        RunConcurrently(index =>
        {
            try
            {
                results[index] = lazy.Get();
            }
            catch (InvalidOperationException)
            {
                Interlocked.Increment(ref failures);
            }
        });

        // Assert
        Assert.Equal(1, failures);
        Assert.Equal(2, attempts);
        Assert.All(results.Where(result => result is not null), result => Assert.Same(expected, result));
    }

    [Fact]
    public void Get_AfterComputation_DoesNotWaitForInitializationLock()
    {
        // Arrange
        var lazy = new ThreadSafeLazy<object>(() => new object());
        var expected = lazy.Get();
        var syncField = typeof(ThreadSafeLazy<object>).GetField("_sync", BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.NotNull(syncField);
        var sync = syncField.GetValue(lazy);
        Assert.NotNull(sync);

        // Act
        using var done = new ManualResetEventSlim();
        bool completed;
        object? actual = null;
        lock (sync)
        {
            var worker = new Thread(() =>
            {
                actual = lazy.Get();
                done.Set();
            })
            { IsBackground = true };

            worker.Start();
            completed = done.Wait(Timeout);
        }

        // Assert
        Assert.True(completed);
        Assert.Same(expected, actual);
    }

    private static void RunConcurrently(Action<int> action)
    {
        using var ready = new CountdownEvent(ConcurrentCallCount);
        using var start = new ManualResetEventSlim();

        var tasks = Enumerable.Range(0, ConcurrentCallCount)
            .Select(index => Task.Factory.StartNew(
                () =>
                {
                    ready.Signal();
                    start.Wait();
                    action(index);
                },
                CancellationToken.None,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default))
            .ToArray();

        try
        {
            Assert.True(ready.Wait(Timeout));
        }
        finally
        {
            start.Set();
        }

        Assert.True(Task.WaitAll(tasks, Timeout));
    }
}
