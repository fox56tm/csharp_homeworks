namespace Game.Core;

public class Map
{
    private readonly bool[,] _walls;

    public int Width { get; }
    public int Height { get; }

    private Map(bool[,] walls)
    {
        _walls = walls;
        Width = walls.GetLength(0);
        Height = walls.GetLength(1);
    }

    public static Map Load(string path)
    {
        string[] lines = File.ReadAllLines(path);

        if (lines.Length == 0)
        {
            throw new InvalidDataException("The map file is empty.");
        }

        int width = 0;
        foreach (string line in lines)
        {
            if (line.Length > width)
            {
                width = line.Length;
            }
        }

        var walls = new bool[width, lines.Length];

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < width; x++)
            {
                bool isMissing = x >= lines[y].Length;
                walls[x, y] = isMissing || lines[y][x] == '#';
            }
        }

        return new Map(walls);
    }

    public bool IsWall(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height)
        {
            return true;
        }

        return _walls[x, y];
    }

    public char GetChar(int x, int y)
    {
        return IsWall(x, y) ? '#' : ' ';
    }

    public (int x, int y) FindStartPosition()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (!IsWall(x, y))
                {
                    return (x, y);
                }
            }
        }

        throw new InvalidDataException("The map has no free cells.");
    }
}
