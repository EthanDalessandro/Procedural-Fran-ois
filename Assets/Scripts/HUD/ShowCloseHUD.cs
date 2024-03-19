using UnityEngine;

public class ShowCloseHUD : MonoBehaviour
{
    public void CloseOpenObject(GameObject objectToPlayWith)
    {
        objectToPlayWith.SetActive(!objectToPlayWith.activeSelf);
    }
}