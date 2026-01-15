namespace ToyRobot.Core;

public static class DirectionExtensions
{
    public static (int dx, int dy) ToDelta(this Direction d) => d switch
    {
        Direction.North => (0, 1),
        Direction.East => (1, 0),
        Direction.South => (0, -1),
        Direction.West => (-1, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(d))
    };

    public static Direction TurnLeft(this Direction d) => d switch
    {
        Direction.North => Direction.West,
        Direction.West => Direction.South,
        Direction.South => Direction.East,
        Direction.East => Direction.North,
        _ => throw new ArgumentOutOfRangeException(nameof(d))
    };

    public static Direction TurnRight(this Direction d) => d switch
    {
        Direction.North => Direction.East,
        Direction.East => Direction.South,
        Direction.South => Direction.West,
        Direction.West => Direction.North,
        _ => throw new ArgumentOutOfRangeException(nameof(d))
    };
}