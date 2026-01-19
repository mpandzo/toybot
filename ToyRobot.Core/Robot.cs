namespace ToyRobot.Core;

public sealed class Robot
{
    private HashSet<(int, int)> _obstructions;
    private readonly Tabletop _tabletop;

    private bool _placed;
    private Position _position;
    private Direction _direction;

    public Robot(Tabletop tabletop)
    {
        _tabletop = tabletop ?? throw new ArgumentNullException(nameof(tabletop));
        _obstructions = new HashSet<(int, int)>();
    }

    public bool TryPlace(Position position, Direction direction)
    {
        if (!_tabletop.IsValid(position)) return false;
        if (_obstructions.Contains((position.X, position.Y))) return false;

        _position = position;
        _direction = direction;
        _placed = true;
        
        return true;
    }

    public bool TryPlace(Position position)
    {
        if (!_placed) return false;
        if (!_tabletop.IsValid(position)) return false;
        if (_obstructions.Contains((position.X, position.Y))) return false;

        _position = position;
        return true;
    }

    public bool TryMove()
    {
        if (!_placed) return false;

        var (dx, dy) = _direction.ToDelta();
        var next = new Position(_position.X + dx, _position.Y + dy);

        if (!_tabletop.IsValid(next)) return false;
        if (_obstructions.Contains((next.X, next.Y))) return false;

        _position = next;
        return true;
    }

    public bool TryTurnLeft()
    {
        if (!_placed) return false;
        _direction = _direction.TurnLeft();
        return true;
    }

    public bool TryTurnRight()
    {
        if (!_placed) return false;
        _direction = _direction.TurnRight();
        return true;
    }

    public RobotState GetState() => _placed
        ? new RobotState(true, _position, _direction)
        : new RobotState(false, default, default);

    public string? Report() => _placed
        ? $"{_position.X},{_position.Y},{_direction.ToString().Replace("_", " ").ToUpperInvariant()}"
        : null;

    public bool TryAvoid(Position p)
    {
        if (!_placed) return false;
        if (!_tabletop.IsValid(p)) return false;
        if (_placed && _position.X == p.X && _position.Y == p.Y) return false;

        if (_obstructions.Contains((p.X, p.Y))) return false;

        _obstructions.Add((p.X, p.Y));

        return true;
    }
}