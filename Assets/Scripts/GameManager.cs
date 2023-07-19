using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Maze maze;

    public int width;
    public int height;
    public int rooms;
    public GameObject cellPrefab;
    // Start is called before the first frame update
    void Start()
    {
        maze = new Maze(width,height,rooms);
//        List<Cell> path = new List<Cell>();
//        path.Add(maze.GetCell(0,0));
//        maze.Generate(maze.GetCell(0,0),path);
        maze.GenerateDungeon();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var position = new Vector3(3.9f * i, 0, 3.9f * j);
                GameObject cell = Instantiate(cellPrefab,
                    position,
                    Quaternion.identity,
                    transform);
                var controller = cell.GetComponent<CellController>();
                controller.init(maze.GetCell(j,i).GetSituation());
            }
        }
    }
}
