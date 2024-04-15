using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] public bool _haveMunition = true;
    [SerializeField] private float _cannonMovespeed = 2, _enemyShootRange = 12.5f;
    [SerializeField] private GameObject _cannon, _bulletToShoot, _defaultCannonPos;
    [SerializeField] private Transform _generator;
    public GameObject _enemyInRange;

    void DetectEnemy()
    {
        if (_enemyInRange == null) _enemyInRange = _defaultCannonPos;
        Vector3 heading = _enemyInRange.transform.position - _cannon.transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _cannon.transform.rotation = Quaternion.Slerp(_cannon.transform.rotation, targetRotation, _cannonMovespeed * Time.deltaTime);
    }
    private void Update()
    {
        DetectEnemy();
        VerifyEnemyRange();
    }

    public void Shoot()
    {
        if (!_haveMunition)
        {
            print("Can't shoot");
            return;
        }

        Instantiate(_bulletToShoot, _generator.position, _generator.rotation);
        //PlaySFX
        _haveMunition = false;
    }
    
    public void EnemyInRange(GameObject detectedEnemy)
    {
        _enemyInRange = detectedEnemy;
    }

    private void VerifyEnemyRange()
    {
        if (Vector3.Distance(_enemyInRange.transform.position, transform.position) >= _enemyShootRange)
        {
            _enemyInRange = _defaultCannonPos;
        }
    }

    public void ResetVision()
    {
        _enemyInRange = null;
    }
    
}
    