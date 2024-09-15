using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 mouseDelta;
    public Vector2 moveComposite;

    public float movementInputDuration;
    public bool movementInputDetected;

    private Controls _controls;

    public Action onAimActivated;
    public Action onAimDeactivated;

    public Action onCrouchActivated;
    public Action onCrouchDeactivated;

    public Action onJumpPerformed;

    public Action onLockOnToggled;

    public Action onSprintActivated;
    public Action onSprintDeactivated;

    public Action onWalkToggled;

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
        }

        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveComposite = context.ReadValue<Vector2>();
        movementInputDetected = moveComposite.magnitude > 0;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        onJumpPerformed?.Invoke();
    }
    public void OnToggleWalk(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        onWalkToggled?.Invoke();
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
            onSprintActivated?.Invoke();
        else if (context.canceled)
            onSprintDeactivated?.Invoke();
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
            onCrouchActivated?.Invoke();
        else if (context.canceled)
            onCrouchDeactivated?.Invoke();
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.started)
            onAimActivated?.Invoke();
        else if (context.canceled)
            onAimDeactivated?.Invoke();
    }
    public void OnLockOn(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        onLockOnToggled?.Invoke();
        onSprintDeactivated?.Invoke();
    }







}
