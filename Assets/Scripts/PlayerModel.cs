using UnityEngine;
using System.Collections.Generic;

public class PlayerModel
{
    #region Player Settings
    #region Locomotion Settings



    [Tooltip("Slowest movement speed of the player when set to a walk state or half press tick")]
    public static float _walkSpeed = 1.4f;

    [Tooltip("Default movement speed of the player")]
    public static float _runSpeed = 2.5f;

    [Tooltip("Top movement speed of the player")]
    public static float _sprintSpeed = 7f;

    [Tooltip("Damping factor for changing speed")]

    public static float _speedChangeDamping = 10f;

    [Tooltip("Rotation smoothing factor.")]
    public static float _rotationSmoothing = 10f;



    #endregion

    #region Shuffle Settings

    [Tooltip("Threshold for button hold duration.")]
    public static float _buttonHoldThreshold = 0.15f;

    [Tooltip("Direction of shuffling on the X-axis.")]
    public static float shuffleDirectionX;

    [Tooltip("Direction of shuffling on the Z-axis.")]
    public static float shuffleDirectionZ;

    #endregion

    #region Strafing

    [Tooltip("Minimum threshold for forward strafing angle.")]
    public static float _forwardStrafeMinThreshold = -55.0f;

    [Tooltip("Maximum threshold for forward strafing angle.")]
    public static float _forwardStrafeMaxThreshold = 125.0f;

    [Tooltip("Current forward strafing value.")]
    public static float forwardStrafe = 1f;

    #endregion

    #region Grounded Settings
    [Tooltip("Current incline angle.")]
    public static float _inclineAngle;
    #endregion

    #region In-Air Settings

    [Tooltip("Force applied when the player jumps.")]
    public static float _jumpForce = 10f;

    [Tooltip("Multiplier for gravity when in the air.")]
    public static float _gravityMultiplier = 2f;

    [Tooltip("Duration of falling.")]
    public static float fallingDuration;

    #endregion

    #region Head Look Settings

    [Tooltip("Delay for head turning.")]
    public static float _headLookDelay;

    [Tooltip("X-axis value for head turning.")]
    public static float _headLookX;

    [Tooltip("Y-axis value for head turning.")]
    public static float _headLookY;

    [Tooltip("Curve for X-axis head turning.")]
    public static AnimationCurve _headLookXCurve;

    #endregion

    #region Body Look Settings

    [Tooltip("Delay for body turning.")]
    public static float _bodyLookDelay;

    [Tooltip("X-axis value for body turning.")]
    public static float _bodyLookX;

    [Tooltip("Y-axis value for body turning.")]
    public static float _bodyLookY;

    [Tooltip("Curve for X-axis body turning.")]
    public static AnimationCurve _bodyLookXCurve;

    #endregion

    #region Lean Settings

    [Tooltip("Delay for leaning.")]
    public static float _leanDelay;

    [Tooltip("Current value for leaning.")]
    public static float _leanValue;

    [Tooltip("Curve for leaning.")]
    public static AnimationCurve _leanCurve;

    [Tooltip("Delay for head leaning looks.")]
    public static float _leansHeadLooksDelay;

    [Tooltip("Flag indicating if an animation clip has ended.")]
    public static bool _animationClipEnd;

    #endregion

    #region Runtime Properties

    public static readonly List<GameObject> currentTargetCandidates = new List<GameObject>();
    public static bool cannotStandUp;
    public static bool crouchKeyPressed;
    public static bool isCrouching;
    public static bool isGrounded = true;
    public static bool isLockedOn;
    public static bool isSliding;
    public static bool isSprinting;
    public static bool isStarting;
    public static bool isStopped = true;
    public static bool isTurningInPlace;
    public static bool isWalking;
    public static bool movementInputHeld;
    public static bool movementInputPressed;
    public static bool movementInputTapped;
    public static float currentMaxSpeed;
    public static float locomotionStartDirection;
    public static float locomotionStartTimer;
    public static float lookingAngle;
    public static float newDirectionDifferenceAngle;
    public static float moveSpeed;
    public static float strafeAngle;
    public static float strafeDirectionX;
    public static float strafeDirectionZ;
    public static GameObject currentLockOnTarget;
    public static Vector3 currentRotation = new Vector3(0f, 0f, 0f);
    public static Vector3 moveDirection;
    public static Vector3 previousRotation;
    public static Vector3 velocity;

    public static bool isStrafing;
    public static bool isAiming;
    #endregion

    #region #Base State Variables

    #endregion
    #endregion
}
