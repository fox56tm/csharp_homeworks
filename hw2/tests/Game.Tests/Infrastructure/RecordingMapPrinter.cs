using Game.Core;

namespace GameTests.Infrastructure;

internal sealed class RecordingMapPrinter : IMapPrinter
{
    public List<PrinterCall> Calls { get; } = new();

    public void DrawMap(Map map)
    {
        Calls.Add(new PrinterCall("DrawMap", 0, 0));
    }

    public void DrawPlayer(int x, int y)
    {
        Calls.Add(new PrinterCall("DrawPlayer", x, y));
    }

    public void ErasePlayer(int x, int y, Map map)
    {
        Calls.Add(new PrinterCall("ErasePlayer", x, y));
    }
}

internal sealed record PrinterCall(string Method, int X, int Y);
