using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _doorLeft, _doorRight;
    private Animation _openDoorAnimation;

    private void Awake()
    {
        _openDoorAnimation = GetComponent<Animation>();
    }

    public void OpenDoor()
    {
        _openDoorAnimation.Play();
    }
}
