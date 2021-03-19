using System;

public class Cell
{
    public Cell(string name, float weidht, float height, float separacion)
    {
        Type = name;
        this.Name = name;
        this.Position = new Position(weidht * separacion, height * separacion);
        this.PositionInList = new Position(weidht, height);
        Render = "";
    }

    public Position Position { get; set; }
    public Position PositionInList { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Render { get; set; }
    public bool IsTrigger { get; set; }
    public MapSprite Sprite { get; set; }
}