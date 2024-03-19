using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    [SerializeField] private PlayerWeapon _playerWeapon;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vector3 heading = other.transform.position - this.transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Enemy"))
                {
                    _playerWeapon._enemyInRange = hit.transform.gameObject;
                }
            }
        }
    }
    
}
