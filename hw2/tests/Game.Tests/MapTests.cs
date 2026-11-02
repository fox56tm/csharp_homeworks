using Game.Core;
using GameTests.Infrastructure;

namespace GameTests;

public class MapTests
{
    [Fact]
    public void Load_FileDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange
        string path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".txt");

        // Act
        Action action = () => Map.Load(path);

        // Assert
        Assert.Throws<FileNotFoundException>(action);
    }

    [Fact]
    public void Load_EmptyFile_ThrowsInvalidDataException()
    {
        // Arrange
        using var file = TempMapFile.WithContent("");

        // Act
        Action action = () => Map.Load(file.FilePath);

        // Assert
        Assert.Throws<InvalidDataException>(action);
    }

    [Fact]
    public void Load_LongestLine_DefinesWidth()
    {
        // Arrange
        using var file = TempMapFile.WithContent("#\n###");

        // Act
        Map map = Map.Load(file.FilePath);

        // Assert
        Assert.Equal(3, map.Width);
        Assert.Equal(2, map.Height);
    }

    [Fact]
    public void Load_ShortLine_MissingCellsAreWalls()
    {
        // Arrange
        using var file = TempMapFile.WithContent("###\n#");

        // Act
        Map map = Map.Load(file.FilePath);

        // Assert
        Assert.True(map.IsWall(1, 1));
        Assert.True(map.IsWall(2, 1));
    }

    [Fact]
    public void IsWall_OutsideMap_ReturnsTrue()
    {
        // Arrange
        using var file = TempMapFile.WithContent("###\n# #\n###");
        Map map = Map.Load(file.FilePath);

        // Act
        bool result = map.IsWall(-1, 1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetChar_Wall_ReturnsHash()
    {
        // Arrange
        using var file = TempMapFile.WithContent("###\n# #\n###");
        Map map = Map.Load(file.FilePath);

        // Act
        char result = map.GetChar(0, 0);

        // Assert
        Assert.Equal('#', result);
    }

    [Fact]
    public void GetChar_FreeCell_ReturnsSpace()
    {
        // Arrange
        using var file = TempMapFile.WithContent("###\n# #\n###");
        Map map = Map.Load(file.FilePath);

        // Act
        char result = map.GetChar(1, 1);

        // Assert
        Assert.Equal(' ', result);
    }

    [Fact]
    public void FindStartPosition_ReturnsFirstFreeCellTopToBottomLeftToRight()
    {
        // Arrange
        using var file = TempMapFile.WithContent("###\n# #\n #\n###");
        Map map = Map.Load(file.FilePath);

        // Act
        (int x, int y) result = map.FindStartPosition();

        // Assert
        Assert.Equal((1, 1), result);
    }

    [Fact]
    public void FindStartPosition_NoFreeCells_ThrowsInvalidDataException()
    {
        // Arrange
        using var file = TempMapFile.WithContent("###\n###");
        Map map = Map.Load(file.FilePath);

        // Act
        Action action = () => map.FindStartPosition();

        // Assert
        Assert.Throws<InvalidDataException>(action);
    }
}
