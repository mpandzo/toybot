# Toy Robot Simulator

A simple command-line simulator library of a toy robot moving on a 6x6 tabletop. The robot can be placed, moved, rotated, and queried using text commands.

## Conditions

- There are no obstructions on the table surface.
- The robot is free to roam around the surface of the table, but must be prevented from falling to destruction. Any movement that would result in this must be prevented, however further valid movement commands must still be allowed.
- PLACE puts the toy robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST.
- (0,0) can be considered as the SOUTH WEST corner and (5,5) as the NORTH EAST corner.
- The first valid command to the robot is a PLACE command. After that, any sequence of commands may be issued, in any order, including another PLACE command. The library discards all commands in the sequence until a valid PLACE command has been executed.
- The PLACE command is discarded if it places the robot outside of the table surface.
- Once the robot is on the table, subsequent PLACE commands leave out the direction and only provide the coordinates. When this happens, the robot moves to the new coordinates without changing the direction.
- MOVE moves the toy robot one unit forward in the direction it is currently facing.
- LEFT and RIGHT rotates the robot 90 degrees in the specified direction without changing the position of the robot.
- REPORT announces the X,Y and orientation of the robot.
- If the robot is not on the table it ignores the MOVE, LEFT, RIGHT and REPORT commands.
- All invalid commands and parameters are discarded.

# Requirements

- .NET SDK 8.0+

# Build

```bash
dotnet build
```

# Run

```bash
dotnet run --project ToyRobot.ConsoleApp
```

# Test

```bash
dotnet test
```
