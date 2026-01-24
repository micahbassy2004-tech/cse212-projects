using System;
using System.Collections.Generic;

public class Maze
{
    public Dictionary<(int x, int y), bool[]> MazeMap { get; private set; }
    public int CurrentX { get; private set; }
    public int CurrentY { get; private set; }

    public Maze(Dictionary<(int x, int y), bool[]> maze, int startX = 1, int startY = 1)
    {
        MazeMap = maze;
        CurrentX = startX;
        CurrentY = startY;
    }

    public void MoveLeft()
    {
        if (!MazeMap[(CurrentX, CurrentY)][0])
            throw new InvalidOperationException("Can't go that way!");
        CurrentX--;
    }

    public void MoveRight()
    {
        if (!MazeMap[(CurrentX, CurrentY)][1])
            throw new InvalidOperationException("Can't go that way!");
        CurrentX++;
    }

    public void MoveUp()
    {
        if (!MazeMap[(CurrentX, CurrentY)][2])
            throw new InvalidOperationException("Can't go that way!");
        CurrentY--;
    }

    public void MoveDown()
    {
        if (!MazeMap[(CurrentX, CurrentY)][3])
            throw new InvalidOperationException("Can't go that way!");
        CurrentY++;
    }

    // Fixed to match the test
    public string GetStatus()
    {
        return $"Current location (x={CurrentX}, y={CurrentY})";
    }
}
