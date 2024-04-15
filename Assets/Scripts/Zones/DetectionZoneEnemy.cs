using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZoneEnemy : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 heading = other.transform.position - this.transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;
            

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (!other.CompareTag("Player")) return;
                
                EnemyBehaviour attackScript = GetComponentInParent<EnemyBehaviour>();
                attackScript.DetectEnemy(other.transform.gameObject);
            }
        }
    }
    
}
