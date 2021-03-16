public class Cell
{
    public Cell(string name, float weidht, float height, float separacion)
    {
        Type = name;
        this.Name = name;
        this.Position = new Position(height * separacion, weidht * separacion);
        this.PositionInList = new Position(height, weidht);
        Render = "";
    }

    public Position Position { get; set; }
    public Position PositionInList { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Render { get; set; }
}