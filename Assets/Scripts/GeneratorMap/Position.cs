public class Position
{
    public Position(float x, float y)
    {
        X = x;
        Y = y;
    }

    public float X { get; private set; }
    public float Y { get; private set; }

    public void AddX(float x)
    {
        X += x;
    }
    public void AddY(float y)
    {
        Y += y;
    }
    public override string ToString()
    {
        return $"X {X} : Y {Y}";
    }
}