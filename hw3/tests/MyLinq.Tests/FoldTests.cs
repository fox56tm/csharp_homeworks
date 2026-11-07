using Xunit;
using MyLinq.Core;
using static MyLinq.Core.MyLinq;
using static MyLinq.Core.MyLinqMapFilterFold;
namespace MyLinq.Tests;

public class FoldTests
{
    [Fact]
    public void Fold_MultiplyBySeedOne_ReturnsProductOfAllElements()
    {
        var input = new List<int> { 1, 2, 3 };

        var result = Fold(input, 1, (acc, elem) => acc * elem);

        Assert.Equal(6, result);
    }

    [Fact]
    public void Fold_EmptyList_ReturnsSeed()
    {
        var result = Fold(new List<int>(), 42, (acc, elem) => acc + elem);

        Assert.Equal(42, result);
    }

    [Fact]
    public void Fold_AccumulatorOfDifferentType_ConcatenatesElements()
    {
        var input = new List<int> { 1, 2, 3 };

        var result = Fold(input, "", (acc, elem) => acc + elem);

        Assert.Equal("123", result);
    }

    [Fact]
    public void Fold_Subtraction_AccumulatesInOrder()
    {
        var input = new List<int> { 1, 2, 3 };

        var result = Fold(input, 0, (acc, elem) => acc - elem);

        Assert.Equal(-6, result);
    }
}
