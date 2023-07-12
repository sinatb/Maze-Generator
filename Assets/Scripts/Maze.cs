using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Maze  
{
    private Cell[,] maze;
    private int width;
    private int height;

    public Cell GetCell(int x,int y)
    {
        return maze[x, y];
    }
    public Maze(int width , int height)
    {
        this.height = height;
        this.width = width;
        maze = new Cell[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                maze[i, j] = new Cell(i, j);
            }
        }
    }
    //Creates the default direction list that will be used for creating other lists
    private List<Direction> CreateDirectionList()
    {
        return new List<Direction>() { Direction.Top, Direction.Right, Direction.Bottom, Direction.Left };
    }
    //Creates a list with 4 random directions created for each cell
    private List<Direction> GetRandomDirectionList()
    {
        List<Direction> directions = CreateDirectionList();
        List<Direction> RDirections = new List<Direction>();
        while (directions.Count > 0)
        {
            int rnd = Random.Range(0, directions.Count-1);
            RDirections.Add(directions[rnd]);
            directions.RemoveAt(rnd);
        }
        return RDirections;
    }
    //Gets 2 Cell and deletes the walls between them to create a path
    private void DeleteWalls(Cell c1,Cell c2)
    {
        var cc1 = c1.GetCoordinates();
        var cc2 = c2.GetCoordinates();
        if (cc1.x > cc2.x)
        {
            c1.ClearWall(Direction.Left);
            c2.ClearWall(Direction.Right);
        }else if (cc1.x < cc2.x)
        {
            c1.ClearWall(Direction.Right);
            c2.ClearWall(Direction.Left);
        }else if (cc1.y > cc2.y)
        {
            c1.ClearWall(Direction.Bottom);
            c2.ClearWall(Direction.Top);
        }else if (cc1.y < cc2.y)
        {
            c1.ClearWall(Direction.Top);
            c2.ClearWall(Direction.Bottom);
        }
    }
    //Returns neighbouring cell of bcell in direction dir
    private Cell GetNeighbour(Cell bcell,Direction dir)
    {
        var bx = bcell.GetCoordinates().x;
        var by = bcell.GetCoordinates().y;

        if (dir == Direction.Top && by < height - 1)
            return maze[bx, by + 1];
        if (dir == Direction.Right && bx < width - 1)
            return maze[bx + 1, by];
        if (dir == Direction.Bottom && by > 0)
            return maze[bx, by - 1];
        if (dir == Direction.Left && bx > 0)
            return maze[bx - 1, by];
        return null;
    }
    //a function to create a maze with recursive backtracking
    public void Generate(Cell curr, List<Cell> path)
    {
        if (path.Count == 0)
            return;
        var cellDirs = GetRandomDirectionList();
        for (var i = 0; i < cellDirs.Count; i++)
        {
            var c2 = GetNeighbour(curr, cellDirs[i]);
            if (c2 != null && !c2.GetIsVisited())
            {
                DeleteWalls(curr,c2);
                c2.SetIsVisited();
                path.Add(c2);
                Generate(c2,path);
            }
        }
        path.RemoveAt(path.Count-1);
        Generate(path[^1],path);
    }
}
