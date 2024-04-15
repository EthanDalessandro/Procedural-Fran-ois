using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject mazeDoor = GameObject.FindGameObjectWithTag("Door");
            Door scriptDoor = mazeDoor.GetComponent<Door>();
            scriptDoor.OpenDoor();
            Destroy(gameObject);
        }
    }
}
