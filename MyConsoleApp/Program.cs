using System;


namespace RobotStm
{
public class Robot
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public Direction Facing { get; private set; }

    public Robot(int x, int y, Direction facing)
    {
        X = x;
        Y = y;
        Facing = facing;
    }

    public void Move()
    {
        switch (Facing)
        {
            case Direction.NORTH:
                if (Y < Tabletop.Height - 1)
                    Y++;
                break;
            case Direction.SOUTH:
                if (Y > 0)
                    Y--;
                break;
            case Direction.EAST:
                if (X < Tabletop.Width - 1)
                    X++;
                break;
            case Direction.WEST:
                if (X > 0)
                    X--;
                break;
        }
    }

    public void TurnLeft()
    {
        switch (Facing)
        {
            case Direction.NORTH:
                Facing = Direction.WEST;
                break;
            case Direction.SOUTH:
                Facing = Direction.EAST;
                break;
            case Direction.EAST:
                Facing = Direction.NORTH;
                break;
            case Direction.WEST:
                Facing = Direction.SOUTH;
                break;
        }
    }

    public void TurnRight()
    {
        switch (Facing)
        {
            case Direction.NORTH:
                Facing = Direction.EAST;
                break;
            case Direction.SOUTH:
                Facing = Direction.WEST;
                break;
            case Direction.EAST:
                Facing = Direction.SOUTH;
                break;
            case Direction.WEST:
                Facing = Direction.NORTH;
                break;
        }
    }

    public void Report()
    {
        Console.WriteLine($"Output: {X},{Y},{Facing}");
    }
}

public enum Direction
{
    NORTH,
    SOUTH,
    EAST,
    WEST
}

public class Tabletop
{
    public const int Width = 5;
    public const int Height = 5;

    public static bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
}

public class CommandParser
{
    private static Robot robot;

    public static void Parse(string command)
    {
        if (robot == null && !command.StartsWith("PLACE"))
        {
            Console.WriteLine("The first valid command should be PLACE.");
            return;
        }

        string[] parts = command.Split(' ');
        string action = parts[0];
        if (action == "PLACE")
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Invalid PLACE command.");
                return;
            }

            string[] args = parts[1].Split(',');
            if (args.Length != 3 || !int.TryParse(args[0], out int x) || !int.TryParse(args[1], out int y) ||
                !Enum.TryParse(args[2], out Direction facing))
            {
                Console.WriteLine("Invalid PLACE command.");
                return;
            }

            if (!Tabletop.IsValidPosition(x, y))
            {
                Console.WriteLine("Invalid position for robot.");
                return;
            }

            robot = new Robot(x, y, facing);
        }
        else if (robot != null)
        {
            switch (action)
            {
                case "MOVE":
                    robot.Move();
                    break;
                case "LEFT":
                    robot.TurnLeft();
                    break;
                case "RIGHT":
                    robot.TurnRight();
                    break;
                case "REPORT":
                    robot.Report();
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Toy Robot Simulation");
        Console.WriteLine("Enter commands (type 'EXIT' to quit):");

        string command;
        do
        {
            command = Console.ReadLine()?.Trim().ToUpper();
            CommandParser.Parse(command);
        } while (command != "EXIT");
    }
}
    
}