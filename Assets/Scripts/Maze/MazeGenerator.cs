using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.AI.Navigation;

public enum Direction
{
    up, down, left, right
}

public class MazeGenerator : MonoBehaviour
{
    public GameObject prefabOfTheCell;
    public GameObject playerPrefab, portailPrefab;
    public NavMeshSurface groundNavMesh;

    public int keyToSpawnInTotal;
    public int enemyToSpawnInTotal;
    public int ammoToSpawnInTotal;

    public List<CellProperties> cellBag;
    public List<CellProperties> visitedCellBag;

    public ScriptableObjectSettings gridSizeX;
    public ScriptableObjectSettings gridSizeY;
    public ScriptableObjectSettings cooldownForStepByStep;

    public CellProperties[,] gridCells;
    private int visitedCount = 0;
    private CellProperties currentCell;
    private Vector2 currentCellCoords;

    public Vector2 gridSize;

    public bool stepByStep;
    public float stepCooldown;
    private bool stepByStepBoolTreshold;

    private void Awake()
    {
        gridSize.x = (int)gridSizeX.floatToStock;
        gridSize.y = (int)gridSizeY.floatToStock;
        stepCooldown = cooldownForStepByStep.floatToStock;
    }

    private void Start()
    {
        InitializeMaze();
        stepByStepBoolTreshold = true;
        if (visitedCellBag[visitedCellBag.Count - 1] != null && stepByStep == false)
        {
            GameObject player = Instantiate(playerPrefab, visitedCellBag[Random.Range(0, visitedCellBag.Count - 1)].transform.position, Quaternion.identity);
            player.transform.position = new Vector3(player.transform.position.x, -0.45f, player.transform.position.z);
        }
        SpawnEntityInCellsRandomly();
        Instantiate(portailPrefab, visitedCellBag[0].transformSpawn.position , Quaternion.identity, visitedCellBag[0].transform);
        
        groundNavMesh.BuildNavMesh();
    }


    public void SpawnEntityInCellsRandomly()
    {
        while (ammoToSpawnInTotal > 0 || enemyToSpawnInTotal > 0 || keyToSpawnInTotal > 0)
        {
            int choosedCellIndex = Random.Range(0, visitedCellBag.Count - 1);

            if (visitedCellBag[choosedCellIndex].isHavingAmmo == false && visitedCellBag[choosedCellIndex].isHavingEnemy == false && visitedCellBag[choosedCellIndex].isHavingKey == false)
            {
                int x = Random.Range(0, 3);
                if (x == 0 && ammoToSpawnInTotal > 0)
                {
                    visitedCellBag[choosedCellIndex].isHavingAmmo = true;
                    ammoToSpawnInTotal--;
                }
                if (x == 1 && enemyToSpawnInTotal > 0)
                {
                    visitedCellBag[choosedCellIndex].isHavingEnemy = true;
                    enemyToSpawnInTotal--;
                }
                if (x == 2 && keyToSpawnInTotal > 0)
                {
                    visitedCellBag[choosedCellIndex].isHavingKey = true;
                    keyToSpawnInTotal--;
                }
                visitedCellBag[choosedCellIndex].CheckIfEntitySpawn();
            }
        }
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
                GameObject cellInstantiated = Instantiate(prefabOfTheCell, new Vector3(i * 10, 2.5f, j * 10), Quaternion.identity, transform);//On intantie la cellule dans le transform du maze Generator
                cellInstantiated.GetComponent<CellProperties>().cellCoords = new Vector2(i, j);// Set les coordonn�es de la cellule cr�er
                gridCells[i, j] = cellInstantiated.GetComponent<CellProperties>();
                cellBag.Add(cellInstantiated.GetComponent<CellProperties>()); //Ajoute dans le sac la cellule cr�er
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

        //player.position = new Vector3(currentCell.cellCoords.x, 0, currentCell.cellCoords.y);
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
        currentCell.transform.DOLocalMoveY(currentCell.transform.position.y + 0.1f, 0.25f).SetLoops(2, LoopType.Yoyo);
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
