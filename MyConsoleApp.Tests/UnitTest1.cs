namespace RobotStm.Tests;

[TestFixture]
public class MyConsoleTest
{
    [Test]
    public void TestMove()
    {
        Robot robot = new Robot(2, 2, Direction.NORTH);
        robot.Move();
        Assert.AreEqual(3, robot.Y);
    }

    [Test]
    public void TestTurnLeft()
    {
        Robot robot = new Robot(2, 2, Direction.NORTH);
        robot.TurnLeft();
        Assert.AreEqual(Direction.WEST, robot.Facing);
    }

    [Test]
    public void TestTurnRight()
    {
        Robot robot = new Robot(2, 2, Direction.NORTH);
        robot.TurnRight();
        Assert.AreEqual(Direction.EAST, robot.Facing);
    }

    [Test]
    public void TestReport()
    {
        Robot robot = new Robot(2, 2, Direction.NORTH);
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            robot.Report();
            string expected = "Output: 2,2,NORTH";
            Assert.AreEqual(expected, sw.ToString().Trim());
        }
    }
}
