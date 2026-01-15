namespace ToyRobot.Core.Tests;

public class RobotTests
{
    private static Tabletop Board6x6() => new Tabletop(width: 6, height: 6);

    [Fact]
    public void BeforeFirstPlace_MoveTurnAndReport_AreIgnored()
    {
        var robot = new Robot(Board6x6());

        Assert.False(robot.TryMove());
        Assert.False(robot.TryTurnLeft());
        Assert.False(robot.TryTurnRight());
        Assert.Null(robot.Report());

        var state = robot.GetState();
        Assert.False(state.IsPlaced);
    }

    [Fact]
    public void Place_OutOfBoundsPositions_AreRejected()
    {
        var robot = new Robot(Board6x6());

        Assert.False(robot.TryPlace(new Position(6, 0), Direction.North));
        Assert.False(robot.TryPlace(new Position(0, 6), Direction.North));
        Assert.False(robot.TryPlace(new Position(-1, 0), Direction.North));
        Assert.False(robot.TryPlace(new Position(0, -1), Direction.North));

        Assert.Null(robot.Report());
    }

    [Fact]
    public void Place_OnFirstCall_RequiresDirection()
    {
        var robot = new Robot(Board6x6());

        Assert.False(robot.TryPlace(new Position(0, 0)));
        Assert.Null(robot.Report());

        var state = robot.GetState();
        Assert.False(state.IsPlaced);

        robot = new Robot(Board6x6());

        Assert.True(robot.TryPlace(new Position(0, 0), Direction.East));
        Assert.Equal("0,0,EAST", robot.Report());

        state = robot.GetState();
        Assert.True(state.IsPlaced);
    }

    [Fact]
    public void SubsequentPlaceMayOmitDirection_AndRetainsDirection()
    {
        var robot = new Robot(Board6x6());

        Assert.True(robot.TryPlace(new Position(0, 0), Direction.East));
        Assert.Equal("0,0,EAST", robot.Report());

        Assert.True(robot.TryPlace(new Position(3, 4)));
        Assert.Equal("3,4,EAST", robot.Report());
    }

    [Fact]
    public void SubsequentPlaceWithDirection_UpdatesDirection()
    {
        var robot = new Robot(Board6x6());

        Assert.True(robot.TryPlace(new Position(0, 0), Direction.East));
        Assert.True(robot.TryPlace(new Position(3, 4), Direction.South));

        Assert.Equal("3,4,SOUTH", robot.Report());
    }

    [Fact]
    public void MoveAdvancesOneUnitInFacingDirection()
    {
        var robot = new Robot(Board6x6());

        Assert.True(robot.TryPlace(new Position(1, 1), Direction.North));
        Assert.True(robot.TryMove());
        Assert.Equal("1,2,NORTH", robot.Report());

        Assert.True(robot.TryTurnRight());
        Assert.True(robot.TryMove());
        Assert.Equal("2,2,EAST", robot.Report());
    }

    [Fact]
    public void MoveIsBlockedAtEdges_AndDoesNotChangeState()
    {
        var robot = new Robot(Board6x6());

        Assert.True(robot.TryPlace(new Position(0, 0), Direction.South));
        Assert.False(robot.TryMove());
        Assert.Equal("0,0,SOUTH", robot.Report());

        Assert.True(robot.TryPlace(new Position(5, 5), Direction.North));
        Assert.False(robot.TryMove());
        Assert.Equal("5,5,NORTH", robot.Report());
    }

    [Fact]
    public void TurnLeftAndRightRotateNinetyDegrees_WithoutChangingPosition()
    {
        var robot = new Robot(Board6x6());

        Assert.True(robot.TryPlace(new Position(2, 2), Direction.North));

        Assert.True(robot.TryTurnLeft());
        Assert.Equal("2,2,WEST", robot.Report());

        Assert.True(robot.TryTurnLeft());
        Assert.Equal("2,2,SOUTH", robot.Report());

        Assert.True(robot.TryTurnRight());
        Assert.Equal("2,2,WEST", robot.Report());

        Assert.True(robot.TryTurnRight());
        Assert.Equal("2,2,NORTH", robot.Report());
    }

    [Fact]
    public void InvalidSubsequentPlaceOutsideBounds_IsRejectedAndStateUnchanged()
    {
        var robot = new Robot(Board6x6());

        Assert.True(robot.TryPlace(new Position(1, 1), Direction.North));
        Assert.Equal("1,1,NORTH", robot.Report());

        Assert.False(robot.TryPlace(new Position(6, 1)));
        Assert.Equal("1,1,NORTH", robot.Report());
    }
}