using Game.Core;
using GameTests.Infrastructure;

namespace GameTests;

public class GameTests
{
    [Fact]
    public void OnRight_PlayerMovesOneCellRight()
    {
        // Arrange
        using var file = TempMapFile.WithContent("#####\n#   #\n#####");
        Map map = Map.Load(file.FilePath);
        var printer = new RecordingMapPrinter();
        var game = new Game.Core.Game(map, printer, new EventLoop());

        // Act
        game.OnRight(null!, EventArgs.Empty);

        // Assert
        Assert.Equal(
            new[]
            {
                new PrinterCall("ErasePlayer", 1, 1),
                new PrinterCall("DrawPlayer", 2, 1)
            },
            printer.Calls);
    }

    [Fact]
    public void OnDown_PlayerMovesOneCellDown()
    {
        // Arrange
        using var file = TempMapFile.WithContent("#####\n#   #\n#   #\n#####");
        Map map = Map.Load(file.FilePath);
        var printer = new RecordingMapPrinter();
        var game = new Game.Core.Game(map, printer, new EventLoop());

        // Act
        game.OnDown(null!, EventArgs.Empty);

        // Assert
        Assert.Equal(
            new[]
            {
                new PrinterCall("ErasePlayer", 1, 1),
                new PrinterCall("DrawPlayer", 1, 2)
            },
            printer.Calls);
    }

    [Fact]
    public void OnLeft_PlayerMovesOneCellLeft()
    {
        // Arrange
        using var file = TempMapFile.WithContent("#####\n#   #\n#####");
        Map map = Map.Load(file.FilePath);
        var printer = new RecordingMapPrinter();
        var game = new Game.Core.Game(map, printer, new EventLoop());
        game.OnRight(null!, EventArgs.Empty);
        printer.Calls.Clear();

        // Act
        game.OnLeft(null!, EventArgs.Empty);

        // Assert
        Assert.Equal(
            new[]
            {
                new PrinterCall("ErasePlayer", 2, 1),
                new PrinterCall("DrawPlayer", 1, 1)
            },
            printer.Calls);
    }

    [Fact]
    public void OnUp_PlayerMovesOneCellUp()
    {
        // Arrange
        using var file = TempMapFile.WithContent("#####\n#   #\n#   #\n#####");
        Map map = Map.Load(file.FilePath);
        var printer = new RecordingMapPrinter();
        var game = new Game.Core.Game(map, printer, new EventLoop());
        game.OnDown(null!, EventArgs.Empty);
        printer.Calls.Clear();

        // Act
        game.OnUp(null!, EventArgs.Empty);

        // Assert
        Assert.Equal(
            new[]
            {
                new PrinterCall("ErasePlayer", 1, 2),
                new PrinterCall("DrawPlayer", 1, 1)
            },
            printer.Calls);
    }

    [Fact]
    public void OnRight_Wall_DoesNotMovePlayer()
    {
        // Arrange
        using var file = TempMapFile.WithContent("####\n# ##\n####");
        Map map = Map.Load(file.FilePath);
        var printer = new RecordingMapPrinter();
        var game = new Game.Core.Game(map, printer, new EventLoop());

        // Act
        game.OnRight(null!, EventArgs.Empty);

        // Assert
        Assert.Empty(printer.Calls);
    }

    [Fact]
    public void OnDown_Wall_DoesNotMovePlayer()
    {
        // Arrange
        using var file = TempMapFile.WithContent("####\n#  #\n####");
        Map map = Map.Load(file.FilePath);
        var printer = new RecordingMapPrinter();
        var game = new Game.Core.Game(map, printer, new EventLoop());

        // Act
        game.OnDown(null!, EventArgs.Empty);

        // Assert
        Assert.Empty(printer.Calls);
    }

    [Fact]
    public void OnLeft_MapBoundary_DoesNotMovePlayer()
    {
        // Arrange
        using var file = TempMapFile.WithContent(" ");
        Map map = Map.Load(file.FilePath);
        var printer = new RecordingMapPrinter();
        var game = new Game.Core.Game(map, printer, new EventLoop());

        // Act
        game.OnLeft(null!, EventArgs.Empty);

        // Assert
        Assert.Empty(printer.Calls);
    }
}
