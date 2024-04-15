using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] public int _life = 100;
    [SerializeField] public EnemyBehaviour _enemyBehaviour;

    public void HurtEnemy(int damages)
    {
        _life -= damages;
        if (_life <= 0)
        {
            _enemyBehaviour.IsKilled();
        }
    }
}
