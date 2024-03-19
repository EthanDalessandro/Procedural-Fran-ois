using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] public bool _haveMunition = true;
    [SerializeField] private GameObject _cannon, _generator;
    public GameObject _enemyInRange;
    [SerializeField] private float _cannonMovespeed = 2;

    void DetectEnemy()
    {
        if (_enemyInRange == null) return;
        Vector3 heading = _enemyInRange.transform.position - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _cannon.transform.rotation = Quaternion.Slerp(_cannon.transform.rotation, targetRotation, _cannonMovespeed * Time.deltaTime);
    }
    private void Update()
    {
        DetectEnemy();
    }

    void Shoot()
    {
        if (!_haveMunition) return;
        
        
    }
    
    
}
    