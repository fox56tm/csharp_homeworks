namespace TestApp.Tests.Infrastructure;

internal sealed class TempDllDirectory : IDisposable
{
    private TempDllDirectory(string directoryPath)
    {
        DirectoryPath = directoryPath;
    }

    public string DirectoryPath { get; }

    public static TempDllDirectory CopyTestAssembly()
    {
        var directory = CreateEmpty();
        var source = typeof(TempDllDirectory).Assembly.Location;
        File.Copy(source, Path.Combine(directory.DirectoryPath, Path.GetFileName(source)));
        return directory;
    }

    public static TempDllDirectory CreateEmpty()
    {
        var path = Path.Combine(Path.GetTempPath(), "TestAppTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(path);
        return new TempDllDirectory(path);
    }

    public void Dispose()
    {
        Directory.Delete(DirectoryPath, recursive: true);
    }
}
