using Game.Core;

string path = args.Length > 0 ? args[0] : "map.txt";

try
{
    Map map = Map.Load(path);
    IMapPrinter printer = new ConsoleMapPrinter();
    EventLoop eventLoop = new EventLoop();
    IGameRunner game = new Game.Core.Game(map, printer, eventLoop);
    game.Run();
}
catch (FileNotFoundException)
{
    Console.WriteLine($"Map file not found: {path}");
    Environment.Exit(1);
}
catch (InvalidDataException ex)
{
    Console.WriteLine(ex.Message);
    Environment.Exit(1);
}
