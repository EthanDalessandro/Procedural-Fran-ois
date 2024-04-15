using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWeapon playerWeapon = other.GetComponentInParent<PlayerWeapon>();
            if (playerWeapon._haveMunition == false)
            {
                playerWeapon._haveMunition = true;
                //Play SFX

                Destroy(this.gameObject);
            }
        }
    }
}
