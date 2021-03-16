using UnityEngine;

public class MapGenerator : MonoBehaviour, IMapGeneratorView
{
    [SerializeField] private GameObject prefabDePunto;
    public GameObject[,] map;
    [SerializeField] private float separacion;
    [SerializeField] private Sprite spritePixel;
    private MapGeneratorLogic logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = new MapGeneratorLogic(this, separacion);
    }

    public void CreateSpritesInGame(Cell[,] map)
    {
        var LastX = transform.position.x;
        var LastY = transform.position.y;
        foreach (Cell cell in map)
        {
            var go = new GameObject(cell.Render + "_" + cell.Position.X + "_" + cell.Position.Y);
            go.transform.position = new Vector2(LastX + cell.Position.X, LastY - cell.Position.Y);
            go.transform.parent = transform;
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = spritePixel;
            if (cell.Render.Equals("Background"))
            {
                sr.color = Color.black;
            }
            if (cell.Render.Contains("Pared") || cell.Render.Contains("Esquina"))
            {
                sr.color = Color.red;
            }
            if (cell.Render.Contains("EsquinaDelgada") || cell.Render.Contains("ParedDelgada"))
            {
                sr.color = Color.blue;
            }
            if (cell.Render.Contains("Point"))
            {
                sr.color = Color.white;
            }
            if (cell.Render.Contains("PowerUp"))
            {
                sr.color = Color.grey;
            }
        }
        /*for(var i = 0; i < map.Length; i++)
        {
            for(var o = 0; o < map[0].; o++)
            {
                map[i, o] = Instantiate(prefabDePunto);
                map[i, o].name += "_" + i + o;
                map[i, o].transform.position = new Vector2(transform.position.x + (o * separacion), transform.position.y + (i * separacion));
                map[i, o].transform.parent = transform;
            }
        }*/
    }
}
