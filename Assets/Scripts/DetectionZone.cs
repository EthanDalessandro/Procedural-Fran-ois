using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    [SerializeField] private Tag _detectionTag;
    private string _thisTag;
    
    private void Start()
    {
        _thisTag = this.gameObject.tag;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_detectionTag.name))
        {
            Vector3 heading = other.transform.position - this.transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;
            

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                print("hit");
                if (hit.transform.gameObject.GetComponentInParent<EnemyBehaviour>())
                {
                    if (_thisTag == "Player")
                    {
                        PlayerMovements attackScript = GetComponentInParent<PlayerMovements>();
                        attackScript.DetectEnemy(hit.transform.gameObject);
                    }
                }
                if (hit.transform.gameObject.GetComponentInParent<PlayerMovements>())
                {
                    if (_thisTag == "Enemy")
                    {
                        EnemyBehaviour attackScript = GetComponentInParent<EnemyBehaviour>();
                        attackScript.DetectEnemy(hit.transform.gameObject);
                    }
                }
            }
        }
    }
    
}
