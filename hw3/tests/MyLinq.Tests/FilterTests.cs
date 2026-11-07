using Xunit;
using MyLinq.Core;
using static MyLinq.Core.MyLinq;
using static MyLinq.Core.MyLinqMapFilterFold;

namespace MyLinq.Tests;

public class FilterTests
{
    [Fact]
    public void Filter_EvenNumbers_ReturnsOnlyEvenElements()
    {
        var input = new List<int> { 1, 2, 3, 4, 5, 6 };

        var result = Filter(input, x => x % 2 == 0);

        Assert.Equal(new List<int> { 2, 4, 6 }, result);
    }

    [Fact]
    public void Filter_NoMatchingElements_ReturnsEmptyList()
    {
        var input = new List<int> { 1, 3, 5 };

        var result = Filter(input, x => x % 2 == 0);

        Assert.Empty(result);
    }

    [Fact]
    public void Filter_EmptyList_ReturnsEmptyList()
    {
        var result = Filter(new List<int>(), x => x > 0);

        Assert.Empty(result);
    }

    [Fact]
    public void Filter_DoesNotModifyOriginalList()
    {
        var input = new List<int> { 1, 2, 3, 4 };

        Filter(input, x => x % 2 == 0);

        Assert.Equal(new List<int> { 1, 2, 3, 4 }, input);
    }
}
