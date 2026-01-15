namespace ToyRobot.Core;

public readonly record struct RobotState(
    bool IsPlaced,
    Position Position,
    Direction Direction
);