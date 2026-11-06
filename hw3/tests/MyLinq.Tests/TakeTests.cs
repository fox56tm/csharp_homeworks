using Xunit;
using MyLinq.Core;

namespace MyLinq.Tests;

public class TakeTests
{
    [Fact]
    public void Take_FirstThree_ReturnsFirstThreeElements()
    {
        var input = new List<int> { 1, 2, 3, 4, 5 };

        var result = new List<int>(input.Take(3));

        Assert.Equal(new List<int> { 1, 2, 3 }, result);
    }

    [Fact]
    public void Take_MoreThanCount_ReturnsAllElements()
    {
        var input = new List<int> { 1, 2, 3 };

        var result = new List<int>(input.Take(10));

        Assert.Equal(new List<int> { 1, 2, 3 }, result);
    }

    [Fact]
    public void Take_Zero_ReturnsEmptySequence()
    {
        var input = new List<int> { 1, 2, 3 };

        var result = new List<int>(input.Take(0));

        Assert.Empty(result);
    }

    [Fact]
    public void Take_NegativeCount_ReturnsEmptySequence()
    {
        var input = new List<int> { 1, 2, 3 };

        var result = new List<int>(input.Take(-1));

        Assert.Empty(result);
    }
}
