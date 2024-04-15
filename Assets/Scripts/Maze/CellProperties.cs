using UnityEngine;

public class CellProperties : MonoBehaviour
{
    public Vector2 cellCoords;

    [Header("cell Specificities")]
    public bool isHavingEnemy;
    public bool isHavingAmmo;
    public bool isHavingKey;

    [Header("Cell Maze Helpful Stats")]
    public bool isVisited;
    public bool isHavingNeighbourAvailable;

    [Header("Cell Walls")]
    public bool northWallStatus;
    public bool southWallStatus;
    public bool eastWallStatus;
    public bool westWallStatus;
    public GameObject northWallGameObject;
    public GameObject southWallGameObject;
    public GameObject eastWallGameObject;
    public GameObject westWallGameObject;
    public GameObject finalNorthWallGameObject;
    public GameObject finalSouthWallGameObject;
    public GameObject finalEastWallGameObject;
    public GameObject finalWestWallGameObject;
    public GameObject ammoPrefab;
    public GameObject enemyPrefab;
    public GameObject keyPrefab;
    public Transform transformSpawn;

    private void Start()
    {
        CheckWichWallToDeactivate();
        CheckIfEntitySpawn();
    }


    public void CheckWichWallToDeactivate()
    {
        if (northWallStatus == false)
        {
            northWallGameObject.SetActive(false);
        }
        if (southWallStatus == false)
        {
            southWallGameObject.SetActive(false);
        }
        if (eastWallStatus == false)
        {
            eastWallGameObject.SetActive(false);
        }
        if (westWallStatus == false)
        {
            westWallGameObject.SetActive(false);
        }
    }

    public void CheckWichFinalWallToDeactivate()
    {
        finalNorthWallGameObject.SetActive(true);
        finalSouthWallGameObject.SetActive(true);
        finalEastWallGameObject.SetActive(true);
        finalWestWallGameObject.SetActive(true);

        if (northWallStatus == false)
        {
            finalNorthWallGameObject.SetActive(true);
            finalSouthWallGameObject.SetActive(false);
            finalEastWallGameObject.SetActive(false);
            finalWestWallGameObject.SetActive(false);
        }
        if (southWallStatus == false)
        {
            finalNorthWallGameObject.SetActive(false);
            finalSouthWallGameObject.SetActive(true);
            finalEastWallGameObject.SetActive(false);
            finalWestWallGameObject.SetActive(false);
        }
        if (eastWallStatus == false)
        {
            finalNorthWallGameObject.SetActive(false);
            finalSouthWallGameObject.SetActive(false);
            finalEastWallGameObject.SetActive(true);
            finalWestWallGameObject.SetActive(false);
        }
        if (westWallStatus == false)
        {
            finalNorthWallGameObject.SetActive(false);
            finalSouthWallGameObject.SetActive(false);
            finalEastWallGameObject.SetActive(false);
            finalWestWallGameObject.SetActive(true);
        }
    }

    public void CheckIfEntitySpawn()
    {
        if (isHavingAmmo)
        {
            isHavingEnemy = false;
            isHavingKey = false;
            Instantiate(ammoPrefab, transformSpawn.position, Quaternion.identity);
        }
        if (isHavingEnemy)
        {
            isHavingAmmo = false;
            isHavingKey = false;
            Instantiate(enemyPrefab, transformSpawn.position, Quaternion.identity);
        }
        if (isHavingKey)
        {
            isHavingAmmo = false;
            isHavingEnemy = false;
            Instantiate(keyPrefab, transformSpawn.position, Quaternion.identity);
        }
    }
}
