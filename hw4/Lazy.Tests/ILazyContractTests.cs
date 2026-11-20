using System.Runtime.CompilerServices;
using Lazy.Core;

namespace Lazy.Tests;

public abstract class ILazyContractTests
{
    protected abstract ILazy<T> CreateLazy<T>(Func<T> supplier);

    [Fact]
    public void Constructor_NullSupplier_ThrowsArgumentNullException()
    {
        // Arrange, Act, Assert
        var exception = Assert.Throws<ArgumentNullException>(() => CreateLazy<int>(null!));

        Assert.Equal("supplier", exception.ParamName);
    }

    [Fact]
    public void Get_BeforeFirstCall_DoesNotInvokeSupplier()
    {
        // Arrange
        var calls = 0;
        var lazy = CreateLazy(() => ++calls);
        var callsBeforeGet = calls;

        // Act
        var result = lazy.Get();

        // Assert
        Assert.Equal(0, callsBeforeGet);
        Assert.Equal(1, result);
    }

    [Fact]
    public void Get_RepeatedCalls_ReturnSameInstanceAndInvokeSupplierOnce()
    {
        // Arrange
        var expected = new object();
        var calls = 0;
        var lazy = CreateLazy(() =>
        {
            calls++;
            return expected;
        });

        // Act
        var first = lazy.Get();
        var second = lazy.Get();
        var third = lazy.Get();

        // Assert
        Assert.Same(expected, first);
        Assert.Same(first, second);
        Assert.Same(first, third);
        Assert.Equal(1, calls);
    }

    [Fact]
    public void Get_SupplierReturnsNull_ReturnsNullAndInvokesSupplierOnce()
    {
        // Arrange
        var calls = 0;
        var lazy = CreateLazy<string?>(() =>
        {
            calls++;
            return null;
        });

        // Act
        var first = lazy.Get();
        var second = lazy.Get();

        // Assert
        Assert.Null(first);
        Assert.Null(second);
        Assert.Equal(1, calls);
    }

    [Fact]
    public void Get_SupplierReturnsDefaultValueType_ReturnsDefaultAndInvokesSupplierOnce()
    {
        // Arrange
        var calls = 0;
        var lazy = CreateLazy(() =>
        {
            calls++;
            return 0;
        });

        // Act
        var first = lazy.Get();
        var second = lazy.Get();

        // Assert
        Assert.Equal(0, first);
        Assert.Equal(0, second);
        Assert.Equal(1, calls);
    }

    [Fact]
    public void Get_SupplierThrows_PropagatesOriginalException()
    {
        // Arrange
        var expected = new InvalidOperationException("supplier failed");
        var lazy = CreateLazy<int>(() => throw expected);

        // Act
        var actual = Assert.Throws<InvalidOperationException>(() => lazy.Get());

        // Assert
        Assert.Same(expected, actual);
    }

    [Fact]
    public void Get_SupplierThrowsOnce_ReturnsRecoveredValueOnNextCall()
    {
        // Arrange
        var attempts = 0;
        var lazy = CreateLazy(() =>
        {
            if (++attempts == 1)
                throw new InvalidOperationException("temporary failure");

            return "recovered";
        });

        // Act
        Assert.Throws<InvalidOperationException>(() => lazy.Get());
        var first = lazy.Get();
        var second = lazy.Get();

        // Assert
        Assert.Equal("recovered", first);
        Assert.Equal("recovered", second);
        Assert.Equal(2, attempts);
    }

    [Fact]
    public void Get_AfterComputation_ReleasesSupplierForGarbageCollection()
    {
        // Arrange
        var (lazy, supplierReference) = CreateLazyWithTemporarySupplier();

        // Act
        lazy.Get();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        // Assert
        Assert.False(supplierReference.IsAlive);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private (ILazy<object> Lazy, WeakReference SupplierReference) CreateLazyWithTemporarySupplier()
    {
        var captured = new object();
        Func<object> supplier = () => captured;
        var supplierReference = new WeakReference(supplier);

        return (CreateLazy(supplier), supplierReference);
    }
}
