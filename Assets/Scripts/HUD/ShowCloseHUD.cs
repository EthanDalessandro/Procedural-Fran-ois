using UnityEngine;

public class ShowCloseHUD : MonoBehaviour
{
    public GameObject ObjectToPlayWith;

    public void CloseOpenObject()
    {
        ObjectToPlayWith.SetActive(!ObjectToPlayWith.activeSelf);
    }
}