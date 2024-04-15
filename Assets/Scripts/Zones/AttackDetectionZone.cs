using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetectionZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnemyBehaviour enemyScript = GetComponentInParent<EnemyBehaviour>();
            enemyScript.Attack();
        }
    }
}
