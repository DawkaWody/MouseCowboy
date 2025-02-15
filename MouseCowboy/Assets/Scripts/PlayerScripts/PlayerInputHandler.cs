using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    public bool inGame;
    private bool _setupDone;

    private PlayerInput _playerInput;
    private PlayerMovementController _movementController;
    private PlayerShootingController _shootingController;
    private PlayerAttackController _attackController;

    private void Update()
    {
        if (inGame && !_setupDone)
        {
            DontDestroyOnLoad(this.gameObject);

            Reconfigure();

            _setupDone = true;
        }
    }
    
    public void Reconfigure()
    {
        _playerInput = GetComponent<PlayerInput>();
        var players = FindObjectsOfType<PlayerMovementController>();
        var index = _playerInput.playerIndex;
        _movementController = players.FirstOrDefault(m => m.GetPlayerIndex() == index);

        _shootingController = FindObjectOfType<PlayerShootingController>();
        if (_shootingController.transform.GetComponent<PlayerMovementController>().GetPlayerIndex() != index)
        {
            _shootingController = null;
        }

        _attackController = FindObjectOfType<PlayerAttackController>();
        if (_attackController.transform.GetComponent<PlayerMovementController>().GetPlayerIndex() != index)
        {
            _attackController = null;
        }
    }

    public void OnHMovementRequested(CallbackContext ctx)
    {
        _movementController.SetHInput(ctx.ReadValue<float>());
    }

    public void OnVMovementRequested(CallbackContext ctx)
    {
        _movementController.SetVInput(ctx.ReadValue<float>());
    }

    public void OnShootingRequested(CallbackContext ctx)
    {
        if (_shootingController != null && ctx.performed)
        {
            _shootingController.Shoot();
        }
    }

    public void OnDashRequested(CallbackContext ctx)
    {
        if (_attackController != null)
        {
            _attackController.Dash();
        }
    }

    public void OnAttackRequested(CallbackContext ctx)
    {
        if (_attackController != null && ctx.performed)
        {
            _attackController.Attack();
        }
    }


}
