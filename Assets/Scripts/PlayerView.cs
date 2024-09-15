using UnityEngine;
using static PlayerModel;
using static PlayerController;

public class PlayerView : MonoBehaviour
{
    PlayerController _playerController;
    PlayerModel _playerModel;

    [Tooltip("InputHandler handles player input")]
    [SerializeField]
    private InputHandler _inputHandler;

    #region Animation Variable Hashes

    private readonly int _movementInputTappedHash = Animator.StringToHash("MovementInputTapped");
    private readonly int _movementInputPressedHash = Animator.StringToHash("MovementInputPressed");
    private readonly int _movementInputHeldHash = Animator.StringToHash("MovementInputHeld");
    private readonly int _shuffleDirectionXHash = Animator.StringToHash("ShuffleDirectionX");
    private readonly int _shuffleDirectionZHash = Animator.StringToHash("ShuffleDirectionZ");

    private readonly int _moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int _currentGaitHash = Animator.StringToHash("CurrentGait");

    private readonly int _isJumpingAnimHash = Animator.StringToHash("IsJumping");
    private readonly int _fallingDurationHash = Animator.StringToHash("FallingDuration");

    private readonly int _inclineAngleHash = Animator.StringToHash("InclineAngle");

    private readonly int _strafeDirectionXHash = Animator.StringToHash("StrafeDirectionX");
    private readonly int _strafeDirectionZHash = Animator.StringToHash("StrafeDirectionZ");

    private readonly int _forwardStrafeHash = Animator.StringToHash("ForwardStrafe");
    private readonly int _cameraRotationOffsetHash = Animator.StringToHash("CameraRotationOffset");
    private readonly int _isStrafingHash = Animator.StringToHash("IsStrafing");
    private readonly int _isTurningInPlaceHash = Animator.StringToHash("IsTurningInPlace");

    private readonly int _isCrouchingHash = Animator.StringToHash("IsCrouching");

    private readonly int _isWalkingHash = Animator.StringToHash("IsWalking");
    private readonly int _isStoppedHash = Animator.StringToHash("IsStopped");
    private readonly int _isStartingHash = Animator.StringToHash("IsStarting");

    private readonly int _isGroundedHash = Animator.StringToHash("IsGrounded");

    private readonly int _leanValueHash = Animator.StringToHash("LeanValue");
    private readonly int _headLookXHash = Animator.StringToHash("HeadLookX");
    private readonly int _headLookYHash = Animator.StringToHash("HeadLookY");

    private readonly int _bodyLookXHash = Animator.StringToHash("BodyLookX");
    private readonly int _bodyLookYHash = Animator.StringToHash("BodyLookY");

    private readonly int _locomotionStartDirectionHash = Animator.StringToHash("LocomotionStartDirection");

    #endregion

    #region Components/Script References
    [Header("External Components")]
    [Tooltip("Script controlling camera behavior")]
    public static PlayerCameraController cameraController { get; private set; }

    [Tooltip("Animator component for controlling player animations")]
    [SerializeField]
    private Animator _animator;

    [Tooltip("Character Controller component for controlling player movement")]
    [SerializeField]
    public static CharacterController characterController;

    #endregion
    #region Capsule Settings

    [Header("Capsule Values")]
    [Tooltip("Standing height of the player capsule.")]
    [SerializeField]
    public static float capsuleStandingHeight = 1.8f;

    [Tooltip("Standing center of the player capsule.")]
    [SerializeField]
    public static float capsuleStandingCentre = 0.93f;

    [Tooltip("Crouching height of the player capsule.")]
    [SerializeField]
    public static float capsuleCrouchingHeight = 1.2f;

    [Tooltip("Crouching center of the player capsule.")]
    [SerializeField]
    public static float capsuleCrouchingCentre = 0.6f;

    #endregion

    public static Transform _targetLockOnPos;

    [Tooltip("Offset for camera rotation.")]
    [SerializeField]
    private float cameraRotationOffset;

    [Tooltip("Flag indicating if head turning is enabled.")]
    [SerializeField]
    private bool _enableHeadTurn = true;

    [Tooltip("Flag indicating if body turning is enabled.")]
    [SerializeField]
    private bool _enableBodyTurn = true;

    [Tooltip("Flag indicating if leaning is enabled.")]
    [SerializeField]
    private bool _enableLean = true;

    private void Start()
    {
        _targetLockOnPos = transform.Find("TargetLockOnPos");
    }

    /// <summary>
        ///     Updates the animator to have the latest values.
        /// </summary>
        private void UpdateAnimatorController()
        {
            _animator.SetFloat(_leanValueHash, _leanValue);
            _animator.SetFloat(_headLookXHash, _headLookX);
            _animator.SetFloat(_headLookYHash, _headLookY);
            _animator.SetFloat(_bodyLookXHash, _bodyLookX);
            _animator.SetFloat(_bodyLookYHash, _bodyLookY);

            _animator.SetFloat(_isStrafingHash, isStrafing ? 1.0f : 0.0f);

            _animator.SetFloat(_inclineAngleHash, _inclineAngle);

            _animator.SetFloat(_moveSpeedHash, moveSpeed);
            _animator.SetInteger(_currentGaitHash, (int)currentGait);

            _animator.SetFloat(_strafeDirectionXHash, strafeDirectionX);
            _animator.SetFloat(_strafeDirectionZHash, strafeDirectionZ);
            _animator.SetFloat(_forwardStrafeHash, forwardStrafe);
            _animator.SetFloat(_cameraRotationOffsetHash, cameraRotationOffset);

            _animator.SetBool(_movementInputHeldHash, movementInputHeld);
            _animator.SetBool(_movementInputPressedHash, movementInputPressed);
            _animator.SetBool(_movementInputTappedHash, movementInputTapped);
            _animator.SetFloat(_shuffleDirectionXHash, shuffleDirectionX);
            _animator.SetFloat(_shuffleDirectionZHash, shuffleDirectionZ);

            _animator.SetBool(_isTurningInPlaceHash, isTurningInPlace);
            _animator.SetBool(_isCrouchingHash, isCrouching);

            _animator.SetFloat(_fallingDurationHash, fallingDuration);
            _animator.SetBool(_isGroundedHash, isGrounded);

            _animator.SetBool(_isWalkingHash, isWalking);
            _animator.SetBool(_isStoppedHash, isStopped);

            _animator.SetFloat(_locomotionStartDirectionHash, locomotionStartDirection);
        }

}
