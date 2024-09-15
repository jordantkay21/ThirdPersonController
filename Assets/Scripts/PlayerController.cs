using System;
using UnityEngine;
using static PlayerModel;
using static PlayerView;

public enum GaitState
{
    Idle,
    Walk,
    Run,
    Sprint
}
public class PlayerController : MonoBehaviour
{
    [Tooltip("InputHandler handles player input")]
    [SerializeField]
    private InputHandler _inputHandler;

    #region Grounded Settings
    [Header("Grounded Angle")]

    [Tooltip("Position of the rear ray for grounded angle check.")]
    [SerializeField]
    private Transform _rearRayPos;

    [Tooltip("Position of the front ray for grounded angle check.")]
    [SerializeField]
    private Transform _frontRayPos;

    [Tooltip("Layer mask for checking ground.")]
    [SerializeField]
    private LayerMask _groundLayerMask;

    [Tooltip("Useful for rough ground")]
    [SerializeField]
    private float _groundedOffset = -0.14f;
    #endregion

    [Tooltip("Whether the character always faces the camera facing direction")]
    public bool _alwaysStrafe = true;

    [Tooltip("Flag indicating if head turning is enabled.")]
    public bool _enableHeadTurn = true;

    [Tooltip("Flag indicating if body turning is enabled.")]
    public bool _enableBodyTurn = true;

    [Tooltip("Flag indicating if leaning is enabled.")]
    public bool _enableLean = true;

    public static IPlayerState currentState;
    public static GaitState currentGait;

    private void Start()
    {
        _inputHandler.onAimActivated += ActivateAim;
        _inputHandler.onAimDeactivated += DeactivateAim;

        _inputHandler.onLockOnToggled += ToggleLockOn;

        _inputHandler.onWalkToggled += ToggleWalk;

        _inputHandler.onSprintActivated += ActivateSprint;
        _inputHandler.onSprintDeactivated += DeactivateSprint;

        _inputHandler.onCrouchActivated += ActivateCrouch;
        _inputHandler.onCrouchDeactivated += DeactivateCrouch;

        PlayerModel.isStrafing = _alwaysStrafe;

        SwitchState(new LocomotionState());
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    #region State Logic
    #region Aim & Lock On
    private void ActivateAim()
    {
        isAiming = true;
        isStrafing = !isStrafing;
    }

    private void DeactivateAim()
    {
        isAiming = true;
        isStrafing = !isStrafing && (_alwaysStrafe);
    }

    #region Target Canidates Logic
    public void AddTargetCandidate(GameObject newTarget)
    {
        if (newTarget != null)
        {
            currentTargetCandidates.Add(newTarget);
        }
    }

    public void RemoveTargetCandidate(GameObject targetToRemove)
    {
        if (currentTargetCandidates.Contains(targetToRemove))
            currentTargetCandidates.Remove(targetToRemove);
    }
    #endregion

    private void ToggleLockOn()
    {
        EnableLockOn(!isLockedOn);
    }

    private void EnableLockOn(bool enable)
    {
        isLockedOn = enable;
        isStrafing = false;

        isStrafing = enable ? !isSprinting : _alwaysStrafe || isAiming;

        cameraController.LockOn(enable, _targetLockOnPos);

        if (enable && currentLockOnTarget != null)
        {
            currentLockOnTarget.GetComponent<ObjectLockOn>().Highlight(true, true);
        }
    }
    #endregion

    #region Walking State
    private void ToggleWalk()
    {
        EnableWalk(!isWalking);
    }

    private void EnableWalk(bool enable)
    {
        isWalking = enable && isGrounded && isSprinting;
    }
    #endregion

    #region Sprint State

    private void ActivateSprint()
    {
        if (!isCrouching)
        {
            EnableWalk(false);
            isSprinting = true;
            isStrafing = false;
        }
    }

    private void DeactivateSprint()
    {
        isSprinting = false;

        if (_alwaysStrafe || isAiming || isLockedOn)
            isStrafing = true;
    }
    #endregion

    #region Crouching State
    private void ActivateCrouch()
    {
        crouchKeyPressed = true;

        if (isGrounded)
        {
            CapsuleCrouchingSize(true);
            DeactivateSprint();
            isCrouching = true;
        }
    }

    private void DeactivateCrouch()
    {
        crouchKeyPressed = false;

        if (!cannotStandUp && !isSliding)
        {
            CapsuleCrouchingSize(false);
            isCrouching = true;
        }
    }

    public void ActivateSliding()
    {
        isSliding = true;
    }

    public void DeactivateSliding()
    {
        isSliding = false;
    }

    private void CapsuleCrouchingSize(bool crouching)
    {
        if (crouching)
        {
            characterController.center = new Vector3(0f, capsuleCrouchingCentre, 0f);
            characterController.height = capsuleCrouchingHeight;
        }
        else
        {
            characterController.center = new Vector3(0f, capsuleStandingCentre, 0f);
            characterController.height = capsuleStandingHeight;
        }
    }
    #endregion
    #endregion

    #region Shared Logic
    public void SwitchState(IPlayerState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void ExecuteCommand(ICommand command)
    {
        command.Execute(this);
    }
    #endregion
}
