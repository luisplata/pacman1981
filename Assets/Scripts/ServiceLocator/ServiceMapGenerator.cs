public class ServiceMapGenerator : IMapGenerator
{
    private Cell[,] map;
    public Cell[,] GetMap()
    {
        return map;
    }

    public void SaveMap(Cell[,] map)
    {
        this.map = map;
    }
}