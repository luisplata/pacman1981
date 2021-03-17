using UnityEngine;

public class MapGenerator : MonoBehaviour, IMapGeneratorView
{
    [SerializeField] private GameObject prefabDePunto;
    [SerializeField] private float separacion;
    [SerializeField] private Sprite spritePixel;
    private MapGeneratorLogic logic;
    [SerializeField] private MapSpritesConfiguration powerUpsConfiguration;
    MapSpritesFactory mapSpritesFactory;
    public Cell[,] map;
    void Start()
    {
        mapSpritesFactory = new MapSpritesFactory(Instantiate(powerUpsConfiguration));
        logic = new MapGeneratorLogic(this, separacion);
    }

    public void CreateSpritesInGame(Cell[,] map)
    {
        this.map = map;
        var LastX = transform.position.x;
        var LastY = transform.position.y;
        foreach (Cell cell in map)
        {
            MapSprite mapSrite = mapSpritesFactory.Create(cell.Render);
            mapSrite.name = cell.Render + "_" + cell.Position.X + "_" + cell.Position.Y;
            mapSrite.transform.position = new Vector2(LastX + cell.Position.X, LastY - cell.Position.Y);
            mapSrite.transform.parent = transform;
            mapSrite.Config();
            cell.IsTrigger = mapSrite.ColliderIsTrigger();
            mapSrite.Cell = cell;
        }
    }
}
