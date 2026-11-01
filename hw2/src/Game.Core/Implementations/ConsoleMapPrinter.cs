namespace Game.Core;

public class ConsoleMapPrinter : IMapPrinter
{
    public void DrawMap(Map map)
    {
        for (int y = 0; y < map.Height; y++)
        {
            Console.SetCursorPosition(0, y);

            for (int x = 0; x < map.Width; x++)
            {
                Console.Write(map.GetChar(x, y));
            }
        }
    }

    public void DrawPlayer(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write('@');
    }

    public void ErasePlayer(int x, int y, Map map)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(map.GetChar(x, y));
    }
}
