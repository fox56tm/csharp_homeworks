using TestApp.Tests.Fixtures;
using TestApp.Tests.Infrastructure;

namespace TestApp.Tests;

public class ParserTests
{
    [Fact]
    public void ParsePath_NonExistentPath_ThrowsDirectoryNotFoundException()
    {
        // Arrange
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        var parser = new Parser();

        // Act
        var action = () => parser.ParsePath(path);

        // Assert
        Assert.Throws<DirectoryNotFoundException>(action);
    }

    [Fact]
    public void ParsePath_FolderWithTestClass_IncludesClass()
    {
        // Arrange
        using var directory = TempDllDirectory.CopyTestAssembly();
        var parser = new Parser();

        // Act
        var result = parser.ParsePath(directory.DirectoryPath);

        // Assert
        Assert.Contains(result, type => type == typeof(ParserSampleTestClass));
    }

    [Fact]
    public void ParsePath_FolderWithClassWithoutTests_ExcludesClass()
    {
        // Arrange
        using var directory = TempDllDirectory.CopyTestAssembly();
        var parser = new Parser();

        // Act
        var result = parser.ParsePath(directory.DirectoryPath);

        // Assert
        Assert.DoesNotContain(result, type => type == typeof(ParserClassWithoutTests));
    }

    [Fact]
    public void ParsePath_FolderWithAbstractTestClass_ExcludesClass()
    {
        // Arrange
        using var directory = TempDllDirectory.CopyTestAssembly();
        var parser = new Parser();

        // Act
        var result = parser.ParsePath(directory.DirectoryPath);

        // Assert
        Assert.DoesNotContain(result, type => type == typeof(ParserAbstractTestClass));
    }

    [Fact]
    public void ParsePath_FolderWithoutDlls_ReturnsEmpty()
    {
        // Arrange
        using var directory = TempDllDirectory.CreateEmpty();
        var parser = new Parser();

        // Act
        var result = parser.ParsePath(directory.DirectoryPath);

        // Assert
        Assert.Empty(result);
    }
}
