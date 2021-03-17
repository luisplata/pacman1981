public interface IMapGenerator
{
    void SaveMap(Cell[,] map);
    Cell[,] GetMap();
}
