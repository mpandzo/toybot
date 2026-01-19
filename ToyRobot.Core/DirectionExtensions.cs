namespace ToyRobot.Core;

public static class DirectionExtensions
{
    public static (int dx, int dy) ToDelta(this Direction d) => d switch
    {
        Direction.North => (0, 1),
        Direction.North_East => (1, 1),
        Direction.North_West => (-1, 1),
        Direction.East => (1, 0),
        Direction.South => (0, -1),
        Direction.South_East => (1, -1),
        Direction.South_West => (-1, -1),
        Direction.West => (-1, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(d))
    };

    public static Direction TurnLeft(this Direction d) => d switch
    {
        Direction.North => Direction.North_West,
        Direction.North_West => Direction.West,
        Direction.West => Direction.South_West,
        Direction.South_West => Direction.South,
        Direction.South => Direction.South_East,
        Direction.South_East => Direction.East,
        Direction.East => Direction.North_East,
        Direction.North_East => Direction.North,
        _ => throw new ArgumentOutOfRangeException(nameof(d))
    };

    public static Direction TurnRight(this Direction d) => d switch
    {
        Direction.North => Direction.North_East,
        Direction.North_East => Direction.East,
        Direction.East => Direction.South_East,
        Direction.South_East => Direction.South,
        Direction.South => Direction.South_West,
        Direction.South_West => Direction.West,
        Direction.West => Direction.North_West,
        Direction.North_West => Direction.North,
        _ => throw new ArgumentOutOfRangeException(nameof(d))
    };
}