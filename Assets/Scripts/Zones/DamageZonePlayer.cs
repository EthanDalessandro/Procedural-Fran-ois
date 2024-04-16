using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageZonePlayer : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 1f;

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyLife enemyLife = other.GetComponent<EnemyLife>();
            enemyLife.HurtEnemy(100);
        }
        else if (other.CompareTag("Player"))
        {
           PlayerLife playerLife = other.GetComponentInParent<PlayerLife>();
           playerLife.HurtPlayer(50);
        }
    }
}
