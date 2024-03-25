using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private Rigidbody _playerRb;
    private InputAction _onMoveAction;
    private Vector2 _moveAction;

    void Start(){
        _playerRb = GetComponent<Rigidbody>();
        PlayerInput _playerInput = GetComponent<PlayerInput>();
        _onMoveAction = _playerInput.actions.FindAction("Move");
    }

    void FixedUpdate(){
        ReadValues();
        UpdateMovements();
    }
    void ReadValues(){
        _moveAction = _onMoveAction.ReadValue<Vector2>();
    }
    void UpdateMovements(){
        _playerRb.velocity = new Vector3(_moveAction.x * _movementSpeed, _playerRb.velocity.y,_moveAction.y * _movementSpeed);
    }
    
    public void DetectEnemy(GameObject detectedEnemy)
    {
        
    }
    
}