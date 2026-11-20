using Lazy.Core;

namespace Lazy.Tests;

public sealed class SimpleLazyTests : ILazyContractTests
{
    protected override ILazy<T> CreateLazy<T>(Func<T> supplier) => new SimpleLazy<T>(supplier);
}
