namespace Game.Core;

public class EventLoop
{
    public event EventHandler LeftHandler = (sender, args) => { };
    public event EventHandler RightHandler = (sender, args) => { };
    public event EventHandler UpHandler = (sender, args) => { };
    public event EventHandler DownHandler = (sender, args) => { };

    public void Run()
    {
        while (true)
        {
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    LeftHandler(this, EventArgs.Empty);
                    break;
                case ConsoleKey.RightArrow:
                    RightHandler(this, EventArgs.Empty);
                    break;
                case ConsoleKey.UpArrow:
                    UpHandler(this, EventArgs.Empty);
                    break;
                case ConsoleKey.DownArrow:
                    DownHandler(this, EventArgs.Empty);
                    break;
                case ConsoleKey.Escape:
                    return;
            }
        }
    }
}
