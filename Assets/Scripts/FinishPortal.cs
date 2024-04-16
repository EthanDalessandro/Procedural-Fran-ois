using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPortal : MonoBehaviour
{
    private BoxCollider _portalCollider;
    private MeshRenderer _meshRenderer;
    [SerializeField] private Material _openMaterial;
    private int _keyCounter = 3;
    public int KeyCounter
    {
        get { return _keyCounter; }
        set
        {
            _keyCounter = value;
            if (_keyCounter <= 0) ActivatePortal();
            print(value);
        }
    }

    private void Awake()
    {
        _portalCollider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Player"))
        {
            print("Finish");
            //Finish
        }
    }

    private void ActivatePortal()
    {
        _portalCollider.isTrigger = true;
        _meshRenderer.material = _openMaterial;
    }
}
