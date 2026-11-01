namespace Game.Core;

public class Game : IGameRunner
{
    private readonly Map _map;
    private readonly IMapPrinter _printer;
    private readonly EventLoop _eventLoop;
    private int _x;
    private int _y;

    public Game(Map map, IMapPrinter printer, EventLoop eventLoop)
    {
        _map = map;
        _printer = printer;
        _eventLoop = eventLoop;

        (_x, _y) = map.FindStartPosition();

        _eventLoop.LeftHandler += OnLeft;
        _eventLoop.RightHandler += OnRight;
        _eventLoop.UpHandler += OnUp;
        _eventLoop.DownHandler += OnDown;
    }

    public void Run()
    {
        Console.CursorVisible = false;
        _printer.DrawMap(_map);
        _printer.DrawPlayer(_x, _y);
        _eventLoop.Run();
    }

    public void OnLeft(object sender, EventArgs args)
    {
        Move(-1, 0);
    }

    public void OnRight(object sender, EventArgs args)
    {
        Move(1, 0);
    }

    public void OnUp(object sender, EventArgs args)
    {
        Move(0, -1);
    }

    public void OnDown(object sender, EventArgs args)
    {
        Move(0, 1);
    }

    private void Move(int dx, int dy)
    {
        if (_map.IsWall(_x + dx, _y + dy))
        {
            return;
        }

        _printer.ErasePlayer(_x, _y, _map);
        _x += dx;
        _y += dy;
        _printer.DrawPlayer(_x, _y);
    }
}
