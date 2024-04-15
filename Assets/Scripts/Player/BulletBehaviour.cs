using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{ 
    private Rigidbody _rigidbody;
    [SerializeField] private GameObject _damageZone, _explosionParticles;
    [SerializeField] private float _bulletSpeed = 500f;
    private float _lifeTime = 2f;

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(transform.forward * _bulletSpeed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Enemy"))
        {
            Instantiate(_damageZone, transform.position, Quaternion.identity);
            Instantiate(_explosionParticles, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
