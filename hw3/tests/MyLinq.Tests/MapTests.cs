using Xunit;
using MyLinq.Core;
using static MyLinq.Core.MyLinq;

namespace MyLinq.Tests;

public class MapTests
{
    [Fact]
    public void Map_MultiplyByTwo_ReturnsEachElementDoubled()
    {
        var input = new List<int> { 1, 2, 3 };

        var result = Map(input, x => x * 2);

        Assert.Equal(new List<int> { 2, 4, 6 }, result);
    }

    [Fact]
    public void Map_EmptyList_ReturnsEmptyList()
    {
        var result = Map(new List<int>(), x => x * 2);

        Assert.Empty(result);
    }

    [Fact]
    public void Map_DoesNotModifyOriginalList()
    {
        var input = new List<int> { 1, 2, 3 };

        Map(input, x => x * 2);

        Assert.Equal(new List<int> { 1, 2, 3 }, input);
    }
}
