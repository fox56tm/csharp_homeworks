namespace Game.Core;

public class ConsoleMapPrinter : IMapPrinter
{
    private int _originLeft;
    private int _originTop;

    public void DrawMap(Map map)
    {
        Console.Clear();
        Console.WriteLine();

        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                Console.Write(map.GetChar(x, y));
            }

            Console.WriteLine();
        }

        _originLeft = Console.CursorLeft;
        _originTop = Console.CursorTop - map.Height;
    }

    public void DrawPlayer(int x, int y)
    {
        Console.SetCursorPosition(_originLeft + x, _originTop + y);
        Console.Write('@');
    }

    public void ErasePlayer(int x, int y, Map map)
    {
        Console.SetCursorPosition(_originLeft + x, _originTop + y);
        Console.Write(map.GetChar(x, y));
    }
}
