using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private bool _isVisited = false;
    private int _x;
    private int _y;
    private bool _topWall;
    private bool _rightWall;
    private bool _bottomWall;
    private bool _leftWall;

    public Cell(int x,int y)
    {
        _x = x;
        _y = y;
        _topWall = true;
        _rightWall = true;
        _bottomWall = true;
        _leftWall = true;
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
    
}
