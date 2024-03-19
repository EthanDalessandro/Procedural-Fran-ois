using UnityEngine;

public class CellProperties : MonoBehaviour
{
    public Vector2 cellCoords;

    [Header("cell Specificities")]
    public bool isHavingEnemy;
    public bool isHavingAmmo;

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


    private void Start()
    {
        CheckWichWallToDeactivate();
    }


    public void CheckWichWallToDeactivate()
    {
        if(northWallStatus == false)
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
        if(westWallStatus == false)
        {
            westWallGameObject.SetActive(false);
        }
    }
}
