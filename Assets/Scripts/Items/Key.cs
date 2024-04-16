using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject mazePortal = GameObject.FindGameObjectWithTag("Portal");
            FinishPortal scriptPortal = mazePortal.GetComponent<FinishPortal>();
            scriptPortal.KeyCounter--;
            Destroy(gameObject);
        }
    }
}
