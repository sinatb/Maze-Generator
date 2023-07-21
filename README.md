# Maze Generator

The Following program is a simple Maze Generator with rooms in unity Engine.

## Made With

![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

## Explanation

The mazes in this project are created with recursive backtracking method.At the start a number of rooms are placed on the grid,and the walls between the cells inside a room are deleted. rooms can have intersections, as this will make creation of rooms with complex architecture easier.After this the recursive backtracking method is used on the cells that are not a part of any room to create path between rooms and other cells.
