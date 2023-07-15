using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell 
{
    private bool _isVisited = false;
    private int _x;
    private int _y;
    private bool _topWall;
    private bool _rightWall;
    private bool _bottomWall;
    private bool _leftWall;
    private int _group;
    private Room _room;
    public Cell(int x,int y)
    {
        _x = x;
        _y = y;
        _topWall = true;
        _rightWall = true;
        _bottomWall = true;
        _leftWall = true;
        _group = -1;
        _room = null;
    }

    public void ClearWall(Direction dir)
    {
        switch (dir)
        {
            case Direction.Top:
                _topWall = false;
                break;
            case Direction.Right:
                _rightWall = false;
                break;
            case Direction.Bottom:
                _bottomWall = false;
                break;
            case Direction.Left:
                _leftWall = false;
                break;
        }
    }

    public Vector2Int GetCoordinates()
    {
        return new Vector2Int(_x, _y);
    }
    public bool GetIsVisited()
    {
        return _isVisited;
    }
    public void SetIsVisited()
    {
        _isVisited = true;
    }
    public void SetGroup(int group)
    {
        _group = group;
    }
    public void SetRoom(Room room)
    {
        _room = room;
    }
    public List<bool> GetSituation()
    {
        return new List<bool>() { _topWall, _rightWall, _bottomWall, _leftWall };
    }
    public int GetGroup()
    {
        return _group;
    }
    public Room GetRoom()
    {
        return _room;
    }
}
