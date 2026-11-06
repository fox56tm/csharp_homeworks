namespace Game.Core;

public interface IMapPrinter
{
    void DrawMap(Map map);
    void DrawPlayer(int x, int y);
    void ErasePlayer(int x, int y, Map map);
}
