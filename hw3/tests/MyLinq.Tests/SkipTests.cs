using Xunit;
using MyLinq.Core;

namespace MyLinq.Tests;

public class SkipTests
{
    [Fact]
    public void Skip_One_ReturnsAllButFirstElement()
    {
        var input = new List<int> { 1, 2, 3 };

        var result = new List<int>(input.Skip(1));

        Assert.Equal(new List<int> { 2, 3 }, result);
    }

    [Fact]
    public void Skip_MoreThanCount_ReturnsEmptySequence()
    {
        var input = new List<int> { 1, 2, 3 };

        var result = new List<int>(input.Skip(5));

        Assert.Empty(result);
    }

    [Fact]
    public void Skip_Zero_ReturnsAllElements()
    {
        var input = new List<int> { 1, 2, 3 };

        var result = new List<int>(input.Skip(0));

        Assert.Equal(new List<int> { 1, 2, 3 }, result);
    }

    [Fact]
    public void Skip_NegativeCount_ReturnsAllElements()
    {
        var input = new List<int> { 1, 2, 3 };

        var result = new List<int>(input.Skip(-1));

        Assert.Equal(new List<int> { 1, 2, 3 }, result);
    }

    [Fact]
    public void Skip_ThenTake_ReturnsMiddleElements()
    {
        var input = new List<int> { 1, 2, 3, 4 };

        var result = new List<int>(input.Skip(1).Take(2));

        Assert.Equal(new List<int> { 2, 3 }, result);
    }
}
