using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _taget;
    [SerializeField] private float _distanceToLoseAggro = 15;
    [SerializeField] private GameObject _damageZone;
    private GameObject _targetEnemy;
    private bool _isEnemyDetected;
    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    public void DetectEnemy(GameObject detectedEnemy)
    {
        _isEnemyDetected = true;
        _targetEnemy = detectedEnemy;
    }
    private void Update()
    {
        if (_isEnemyDetected)
        {
            _taget.SetDestination(_targetEnemy.transform.position);
            if (Vector3.Distance(_startPosition, _targetEnemy.transform.position) >= _distanceToLoseAggro)
            {
                _isEnemyDetected = false;
            }
        }
        else
        {
            _taget.SetDestination(_startPosition);
        }
        
    }

    public void Attack()
    {
        StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        _taget.speed = 0;
        
        yield return new WaitForSeconds(.5f);
        
        Instantiate(_damageZone, transform.position, Quaternion.identity);
        
        yield return new WaitForSeconds(1f);
        
        _taget.speed = 5;
    }

    public void IsKilled()
    {
        Destroy(this.gameObject);
    }
}
