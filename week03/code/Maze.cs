using System;
using System.Collections.Generic;

public class Maze
{
    // (x,y) : [left, right, up, down]
    private readonly Dictionary<(int, int), bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<(int, int), bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    /// <summary>
    /// Check to see if you can move left. If you can, then move. If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveLeft()
    {
        var moves = _mazeMap[(_currX, _currY)];
        bool canLeft = moves[0];

        if (!canLeft)
            throw new InvalidOperationException("Can't go that way!");

        _currX -= 1;
    }

    /// <summary>
    /// Check to see if you can move right. If you can, then move. If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveRight()
    {
        var moves = _mazeMap[(_currX, _currY)];
        bool canRight = moves[1];

        if (!canRight)
            throw new InvalidOperationException("Can't go that way!");

        _currX += 1;
    }

    /// <summary>
    /// Check to see if you can move up. If you can, then move. If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveUp()
    {
        var moves = _mazeMap[(_currX, _currY)];
        bool canUp = moves[2];

        if (!canUp)
            throw new InvalidOperationException("Can't go that way!");

        _currY -= 1;
    }

    /// <summary>
    /// Check to see if you can move down. If you can, then move. If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveDown()
    {
        var moves = _mazeMap[(_currX, _currY)];
        bool canDown = moves[3];

        if (!canDown)
            throw new InvalidOperationException("Can't go that way!");

        _currY += 1;
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}
