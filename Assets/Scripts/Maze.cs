using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Maze  
{
    private Cell[,] maze;
    private int width;
    private int height;
    private int numRooms;
    private List<Room> rooms;
    public Cell GetCell(int x,int y)
    {
        return maze[x, y];
    }
    public Maze(int width , int height,int numRooms)
    {
        this.height = height;
        this.width = width;
        this.numRooms = numRooms;
        rooms = new List<Room>(numRooms);
        maze = new Cell[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                maze[i, j] = new Cell(j,i);
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
            int rnd = Random.Range(0, directions.Count);
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
    //Returns a random cell as a starting point for a new room
    private Cell GetRoomStart(int roomw,int roomh)
    {
        //the starting point for rooms is from (10,10) to the end
        int rndx = Random.Range(2, width - roomw);
        int rndy = Random.Range(2, height - roomh);
        return maze[rndy, rndx];
    }
    //A function to carve out a room in the maze from the random start
    private void CarveRoom(Room r)
    {
        //store room values in loacl vars
        int height = r.GetHeight();
        int width = r.GetWidth();
        Cell start = r.GetStart();
        
        for (int i = start.GetCoordinates().y; i < start.GetCoordinates().y + height; i++)
        {
            for (int j = start.GetCoordinates().x; j < start.GetCoordinates().x + width; j++)
            {
                DeleteWalls(maze[i,j],maze[i,j+1]);
                DeleteWalls(maze[i,j],maze[i+1,j]);
            }
        }
        for (int i = start.GetCoordinates().y; i<start.GetCoordinates().y + height ; i++)
            DeleteWalls(maze[i,start.GetCoordinates().x+width],maze[i+1,start.GetCoordinates().x+width]);
        for (int i = start.GetCoordinates().x; i<start.GetCoordinates().x + width ; i++)
            DeleteWalls(maze[start.GetCoordinates().y+height,i],maze[start.GetCoordinates().y+height,i+1]);
    }
    //A function to set the room number of a group of cells
    private void SetCellRoom(Room r)
    {
        int height = r.GetHeight();
        int width = r.GetWidth();
        Cell start = r.GetStart();
        
        for (int i = start.GetCoordinates().y; i < start.GetCoordinates().y + height; i++)
        {
            for (int j = start.GetCoordinates().x; j < start.GetCoordinates().x + width; j++)
            {
                maze[i,j].SetRoom(r);
            }
        }
    }
    //Returns neighbouring cell of bcell in direction dir
    private List<Cell> GetUngroupedCells()
    {
        List<Cell> ret = new List<Cell>();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (maze[i,j].GetGroup() == -1 && maze[i,j].GetRoom()!=null)
                    ret.Add(maze[i,j]);
            }
        }

        return ret;
    }
    private Cell GetNeighbour(Cell bcell,Direction dir)
    {
        var bx = bcell.GetCoordinates().x;
        var by = bcell.GetCoordinates().y;

        if (dir == Direction.Top && by < height - 1)
            return maze[by+1, bx];
        if (dir == Direction.Right && bx < width - 1)
            return maze[by, bx+1];
        if (dir == Direction.Bottom && by > 0)
            return maze[by-1, bx];
        if (dir == Direction.Left && bx > 0)
            return maze[by, bx-1];
        return null;
    }
    //a function to create a maze with recursive backtracking
    public void Generate(Cell curr, List<Cell> path,int group)
    {
        Debug.Log(curr.GetCoordinates());
        curr.SetIsVisited();
        curr.SetGroup(group);
        var cellDirs = GetRandomDirectionList();
        for (var i = 0; i < cellDirs.Count; i++)
        {
            var c2 = GetNeighbour(curr, cellDirs[i]);
            if (c2 != null && !c2.GetIsVisited() && c2.GetGroup()==-1 && c2.GetRoom()==null)
            {
                DeleteWalls(curr,c2);
                path.Add(c2);
                Generate(c2,path,group);
            }
        }
        if (path.Count == 1)
            return;
        path.RemoveAt(path.Count-1);
        Generate(path[^1],path,group);
    }
    //a function to generate a dungeon with set amount of rooms.
    public void GenerateDungeon()
    {
        for (var i = 0; i < numRooms-1; i++)
        {
            var width = Random.Range(2, 9);
            var height = Random.Range(2, 9);
            var Start = GetRoomStart(width, height);
            rooms.Add(new Room(Start, i, width, height));
            CarveRoom(rooms[^1]);
            SetCellRoom(rooms[^1]);
        }
        var paths = new List<Cell> {maze[0,0]};
        Generate(maze[0,0],paths,0);
    }
}
