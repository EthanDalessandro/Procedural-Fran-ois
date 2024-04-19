using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZoneEnemy : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 1f;
    private bool _hitPlayer;
    private void Awake()
    {
        _hitPlayer = false;
    }   

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
        if (other.CompareTag("Player") && !_hitPlayer)
        {
            PlayerLife playerLife = other.GetComponentInParent<PlayerLife>();
            playerLife.HurtPlayer(50);
            _hitPlayer = true;
        }
    }
}
