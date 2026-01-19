using ToyRobot.Core;

namespace ToyRobot.ConsoleApp;

internal static class Program
{
    private static void Main()
    {
        PrintGreeting();

        var tabletop = new Tabletop(8, 8);
        var robot = new Robot(tabletop);

        while (true)
        {
            Console.Write("> ");
            var line = Console.ReadLine();

            if (line is null)
                break;

            line = line.Trim();
            if (line.Length == 0)
                continue;

            var command = line.ToUpperInvariant();

            if (command == "EXIT")
            {
                Console.WriteLine("Bye!");
                break;
            }

            if (command.StartsWith("PLACE"))
            {
                HandlePlace(robot, line);
                continue;
            }

            if (command.StartsWith("AVOID"))
            {
                HandleAvoid(robot, line);
                continue;
            }

            switch (command)
            {
                case "MOVE":
                    robot.TryMove();
                    break;

                case "LEFT":
                    robot.TryTurnLeft();
                    break;

                case "RIGHT":
                    robot.TryTurnRight();
                    break;

                case "REPORT":
                    var report = robot.Report();
                    if (report is not null)
                        Console.WriteLine($"Output: {report}");
                    break;

                default:
                    Console.WriteLine("Invalid command");
                    break;
            }
        }
    }

    private static void HandleAvoid(Robot robot, string input)
    {
        // Expected formats:
        // AVOID 1,2

        var args = input[5..].Trim();        
        if (args.Length == 0)
        {
            Console.WriteLine("Invalid arguments");
            return;
        }

        var parts = args.Split(",", StringSplitOptions.TrimEntries);
        if (parts.Length != 2)
        {
            Console.WriteLine("Invalid arguments");
            return;
        }

        if (!int.TryParse(parts[0], out int x) ||
            !int.TryParse(parts[1], out int y))
        {
            Console.WriteLine("Invalid arguments");
            return;
        }

        var position = new Position(x, y);

        robot.TryAvoid(position);
    }

    private static void HandlePlace(Robot robot, string input)
    {
        // Expected formats:
        // PLACE X,Y,DIRECTION
        // PLACE X,Y

        var args = input[5..].Trim();
        if (args.Length == 0)
        {
            Console.WriteLine("Invalid command");
            return;
        }

        var parts = args.Split(',', StringSplitOptions.TrimEntries);
        if (parts.Length < 2 || parts.Length > 3)
        {
            Console.WriteLine("Invalid command");
            return;
        }

        if (!int.TryParse(parts[0], out var x) ||
            !int.TryParse(parts[1], out var y))
        {
            Console.WriteLine("Invalid command");
            return;
        }

        var position = new Position(x, y);

        if (parts.Length == 3)
        {
            if (!Enum.TryParse<Direction>(parts[2].Replace(" ", "_"), true, out var direction))
            {
                Console.WriteLine("Invalid command");
                return;
            }

            robot.TryPlace(position, direction);
        }
        else
        {
            robot.TryPlace(position);
        }
    }

    private static void PrintGreeting()
    {
        Console.WriteLine("Toy Robot Simulator");
        Console.WriteLine("-------------------");
        Console.WriteLine("Commands:");
        Console.WriteLine("  PLACE X,Y,DIRECTION   (e.g. PLACE 1,2,NORTH)");
        Console.WriteLine("  MOVE");
        Console.WriteLine("  LEFT");
        Console.WriteLine("  RIGHT");
        Console.WriteLine("  REPORT");
        Console.WriteLine("  EXIT");
        Console.WriteLine();
    }
}
