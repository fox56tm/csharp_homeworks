namespace GameTests.Infrastructure;

internal sealed class TempMapFile : IDisposable
{
    private readonly string _directoryPath;

    private TempMapFile(string directoryPath)
    {
        _directoryPath = directoryPath;
        FilePath = Path.Combine(directoryPath, "map.txt");
    }

    public string FilePath { get; }

    public static TempMapFile WithContent(string content)
    {
        string directoryPath = Path.Combine(Path.GetTempPath(), "GameTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directoryPath);

        var file = new TempMapFile(directoryPath);
        File.WriteAllText(file.FilePath, content);

        return file;
    }

    public void Dispose()
    {
        Directory.Delete(_directoryPath, recursive: true);
    }
}
