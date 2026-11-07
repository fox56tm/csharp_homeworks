using Xunit;
using MyLinq.Core;
using static MyLinq.Core.MyLinq;

namespace MyLinq.Tests;

public class GetPrimesTests
{
    [Fact]
    public void GetPrimes_FirstTen_ReturnsFirstTenPrimes()
    {
        var result = new List<int>(GetPrimes().Take(10));

        Assert.Equal(new List<int> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 }, result);
    }

    [Fact]
    public void GetPrimes_StartsWithTwo()
    {
        var result = new List<int>(GetPrimes().Take(1));

        Assert.Equal(new List<int> { 2 }, result);
    }
}
