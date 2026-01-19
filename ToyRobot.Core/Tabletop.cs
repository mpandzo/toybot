namespace ToyRobot.Core;

public sealed class Tabletop
{
    public int MinX { get; }
    public int MaxX { get; }
    public int MinY { get; }
    public int MaxY { get; }
    
    public Tabletop(int width = 8, int height = 8)
    {
        if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
        if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));

        MinX = 0;
        MinY = 0;
        MaxX = width - 1;
        MaxY = height - 1;
    }

    public bool IsValid(Position p) => p.X >= MinX && p.X <= MaxX && p.Y >= MinY && p.Y <= MaxY;
}