using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up, down, left, right
}

public class MazeGenerator : MonoBehaviour
{
    public GameObject prefabOfTheCell;

    public List<CellProperties> cellBag;
    public List<CellProperties> visitedCellBag;

    public CellProperties[,] gridCells;
    private int visitedCount = 0;
    public CellProperties currentCell;
    public Vector2 currentCellCoords;

    public Vector2 gridSize;

    public bool stepByStep;
    public float stepCooldown;
    private bool stepByStepBoolTreshold;

    private void Start()
    {
        InitializeMaze();
        stepByStepBoolTreshold = true;
    }

    private void Update()
    {
        if (stepByStep == true && stepByStepBoolTreshold == true)
        {
            StartCoroutine(StepByStep(stepCooldown));
        }
    }

    public void InitializeMaze()
    {
        gridCells = new CellProperties[(int)gridSize.x, (int)gridSize.y];
        if (gridSize.x == 0 || gridSize.y == 0)
        {
            return;
        }

        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                GameObject cellInstantiated = Instantiate(prefabOfTheCell, new Vector3(i, 0, j), Quaternion.identity, transform);//On intantie la cellule dans le transform du maze Generator
                cellInstantiated.GetComponent<CellProperties>().cellCoords = new Vector2(i, j);// Set les coordonnées de la cellule créer
                gridCells[i, j] = cellInstantiated.GetComponent<CellProperties>();
                cellBag.Add(cellInstantiated.GetComponent<CellProperties>()); //Ajoute dans le sac la cellule créer
            }
        }

        ChooseRandomCell();
        if (stepByStep == false)
        {
            while (CheckForAllVisitedCell() == true)
            {
                NextCell();
            }
        }
    }

    private void NextCell()
    {
        CellProperties previousCell = currentCell;
        if (isCurrentCellHavingNeighbourg())
        {
            int directionChoosen = Random.Range(0, 4);

            if (directionChoosen == 0)
            {
                CellProperties nextCell = ReturnASpecificCell((int)currentCell.cellCoords.x, (int)currentCell.cellCoords.y + 1);
                if (nextCell != null && nextCell.isVisited == false)
                {
                    currentCell = nextCell;
                    RemoveWalls(currentCell, previousCell, Direction.up);
                    visitedCellBag.Add(currentCell);
                    currentCell.isVisited = true;
                }
            }
            if (directionChoosen == 1)
            {
                CellProperties nextCell = ReturnASpecificCell((int)currentCell.cellCoords.x, (int)currentCell.cellCoords.y - 1);
                if (nextCell != null && nextCell.isVisited == false)
                {
                    currentCell = nextCell;
                    RemoveWalls(currentCell, previousCell, Direction.down);
                    visitedCellBag.Add(currentCell);
                    currentCell.isVisited = true;
                }
            }
            if (directionChoosen == 2)
            {
                CellProperties nextCell = ReturnASpecificCell((int)currentCell.cellCoords.x + 1, (int)currentCell.cellCoords.y);
                if (nextCell != null && nextCell.isVisited == false)
                {
                    currentCell = nextCell;
                    RemoveWalls(currentCell, previousCell, Direction.right);
                    visitedCellBag.Add(currentCell);
                    currentCell.isVisited = true;
                }
            }
            if (directionChoosen == 3)
            {
                CellProperties nextCell = ReturnASpecificCell((int)currentCell.cellCoords.x - 1, (int)currentCell.cellCoords.y);
                if (nextCell != null && nextCell.isVisited == false)
                {
                    currentCell = nextCell;
                    RemoveWalls(currentCell, previousCell, Direction.left);
                    visitedCellBag.Add(currentCell);
                    currentCell.isVisited = true;
                }
            }
            visitedCount = 0;
        }
        else
        {
            visitedCount++;

            if (visitedCellBag.Count >= visitedCount)
            {
                currentCell = visitedCellBag[visitedCellBag.Count - visitedCount];
            }
        }
    }

    public CellProperties ReturnASpecificCell(int x, int y)
    {
        if (x >= 0 && y >= 0 && x <= gridSize.x - 1 && y <= gridSize.y - 1 && gridCells[x, y].isVisited == false)
        {
            return gridCells[x, y];
        }
        return null;
    }

    private void ChooseRandomCell()
    {
        Vector2 randomCoords = new Vector2(Random.Range(-1, cellBag[cellBag.Count - 1].cellCoords.x) + 1, Random.Range(-1, cellBag[cellBag.Count - 1].cellCoords.y) + 1);
        currentCell = ReturnASpecificCell((int)randomCoords.x, (int)randomCoords.y);
        visitedCellBag.Add(currentCell);
        currentCellCoords = currentCell.cellCoords;
        currentCell.isVisited = true;
    }

    public bool isCurrentCellHavingNeighbourg()
    {
        if (ReturnASpecificCell((int)currentCell.cellCoords.x + 1, (int)currentCell.cellCoords.y) != null
            && ReturnASpecificCell((int)currentCell.cellCoords.x + 1, (int)currentCell.cellCoords.y).isVisited == false
            || ReturnASpecificCell((int)currentCell.cellCoords.x - 1, (int)currentCell.cellCoords.y) != null
            && ReturnASpecificCell((int)currentCell.cellCoords.x - 1, (int)currentCell.cellCoords.y).isVisited == false
            || ReturnASpecificCell((int)currentCell.cellCoords.x, (int)currentCell.cellCoords.y + 1) != null
            && ReturnASpecificCell((int)currentCell.cellCoords.x, (int)currentCell.cellCoords.y + 1).isVisited == false
            || ReturnASpecificCell((int)currentCell.cellCoords.x, (int)currentCell.cellCoords.y - 1) != null
            && ReturnASpecificCell((int)currentCell.cellCoords.x, (int)currentCell.cellCoords.y - 1).isVisited == false)
        {
            return true;
        }
        return false;
    }

    public void RemoveWalls(CellProperties currentCell, CellProperties previousCell, Direction directionTaken)
    {
        if (directionTaken == Direction.up)
        {
            currentCell.southWallStatus = false;
            previousCell.northWallStatus = false;
            currentCell.CheckWichWallToDeactivate();
            previousCell.CheckWichWallToDeactivate();
        }
        if (directionTaken == Direction.down)
        {
            currentCell.northWallStatus = false;
            previousCell.southWallStatus = false;
            currentCell.CheckWichWallToDeactivate();
            previousCell.CheckWichWallToDeactivate();
        }
        if (directionTaken == Direction.right)
        {
            currentCell.westWallStatus = false;
            previousCell.eastWallStatus = false;
            currentCell.CheckWichWallToDeactivate();
            previousCell.CheckWichWallToDeactivate();
        }
        if (directionTaken == Direction.left)
        {
            currentCell.eastWallStatus = false;
            previousCell.westWallStatus = false;
            currentCell.CheckWichWallToDeactivate();
            previousCell.CheckWichWallToDeactivate();
        }
    }

    public bool CheckForAllVisitedCell()
    {
        for (int i = 0; i < gridCells.GetLength(0); i++)
        {
            for (int j = 0; j < gridCells.GetLength(1); j++)
            {
                if (gridCells[i, j].isVisited == false)
                {
                    return true;
                }
            }
        }
        stepByStep = false;
        return false;
    }

    public IEnumerator StepByStep(float time)
    {
        stepByStepBoolTreshold = false;
        yield return new WaitForSeconds(time);
        NextCell();
        stepByStepBoolTreshold = true;
    }
}
