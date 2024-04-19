using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPortal : MonoBehaviour
{
    private BoxCollider _portalCollider;
    private MeshRenderer _meshRenderer;
    private SwitchScene _sceneManager;
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
        _sceneManager = GetComponent<SwitchScene>();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("SceneChange");
        if (CompareTag("Player"))
        {
            print("DoSceneChange");
            _sceneManager.SceneToSwitchTo("MainMenu");
        }
    }

    private void ActivatePortal()
    {
        _portalCollider.isTrigger = true;
        _meshRenderer.material = _openMaterial;
    }
}
