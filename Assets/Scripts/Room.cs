using UnityEngine;

public class Room
{
    private Cell _startPoint;
    private int width;
    private int height;
    private int number;
    public Room(Cell _startPoint,int number,int width,int height)
    {
        this._startPoint = _startPoint;
        this.height = height;
        this.width = width;
        this.number = number;
    }
    public Cell GetStart()
    {
        return _startPoint;
    }
    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
    public int GetNumber()
    {
        return number;
    }
}
