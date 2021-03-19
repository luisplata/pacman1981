using UnityEngine;

public class MapGenerator : MonoBehaviour, IMapGeneratorView
{
    [SerializeField] private float separacion;
    private MapGeneratorLogic logic;
    [SerializeField] private MapSpritesConfiguration powerUpsConfiguration;
    MapSpritesFactory mapSpritesFactory;
    void Start()
    {
        mapSpritesFactory = new MapSpritesFactory(Instantiate(powerUpsConfiguration));
        logic = new MapGeneratorLogic(this, separacion);
    }

    public void CreateSpritesInGame(Cell[,] map)
    {
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
            if (cell.Render.Contains("Pacman"))
            {
                PacmanOfMap pacman = (PacmanOfMap)mapSrite;
                pacman.SetInputStrategy(GetComponent<InputStragety>());
            }
        }
    }
}
