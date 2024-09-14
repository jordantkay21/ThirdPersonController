using System;
using UnityEngine;

namespace KayosStudios.ThirdPersonController
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        public PlayerModel playerModel { get; private set; }
        public PlayerView playerView { get; private set; }
        public PlayerStatsUI playerStatsUI { get; private set; }

        // Ground Check Settings
        [SerializeField] LayerMask groundLayer;
        [SerializeField] Transform _groundedRaycastOrigin; //Origin Point for the ground check raycast
        [SerializeField] float raycastDistance = .03f; // Raycast distance slightly longer than the player's height

        public GaitState currentGait;

        private void OnEnable()
        {
            InputHandler.Instance.OnMove += input => ProcessLocomotionInput(input);
            InputHandler.Instance.OnSprint += isSprinting => playerModel.isSprinting = isSprinting;
            InputHandler.Instance.OnLook += mouseDelta => ProcessAimInput(mouseDelta);
            InputHandler.Instance.OnJump += () => playerModel.Jump();
        }



        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            
            playerView = (PlayerView)FindFirstObjectByType(typeof(PlayerView));
            playerStatsUI = (PlayerStatsUI)FindFirstObjectByType(typeof(PlayerStatsUI));
            playerModel = new PlayerModel();
        }


        private void Update()
        {
            RaycastGroundCheck();
            playerModel.CalculateGravity();

            if (playerModel.isGrounded)
            {
                playerView.applyRootmotion = true;
            }
            else
            {
                if (playerModel.isJumping)
                {
                    playerView.applyRootmotion = true;
                }
                else
                {
                    playerView.applyRootmotion = false;
                    playerView.ApplyGravity(playerModel.verticalVelocity);
                }
            }
  
            playerView.UpdateAnimator(playerModel);
            playerStatsUI.UpdateUIVariables(playerModel);  
        }

        private void RaycastGroundCheck()
        {
            RaycastHit hit;

            // Perform the raycast from the raycast origin (grounded raycast origin)
            if (Physics.Raycast(_groundedRaycastOrigin.position, Vector3.down, out hit, raycastDistance, groundLayer))
            {
                playerModel.isGrounded = true;

                // Calculate the angle between the ground normal and the up direction
                float angle = Vector3.Angle(hit.normal, Vector3.up);

                // Store the incline angle in the player model
                playerModel.inclineAngle = angle;

                // Draw the ray for debugging purposes (visible in the scene)
                Debug.DrawRay(hit.point, hit.normal * 2f, Color.green); //Show the surface normal
                Debug.DrawRay(_groundedRaycastOrigin.position, Vector3.down * raycastDistance, Color.blue); //Show raycast
            }
            else
            {
                playerModel.isGrounded = false;
                playerModel.inclineAngle = 0f; // Reset the incline angle if not grounded

                // Draw the ray as red when no ground is hit
                Debug.DrawRay(_groundedRaycastOrigin.position, Vector3.down * raycastDistance, Color.red);
            }    
        }

        public void ProcessLocomotionInput(Vector2 input)
        {
            playerModel.AdjustLocomotionData(input);

            if (input == Vector2.zero)
            {
                currentGait = GaitState.Idle;
                playerModel.currentGait = 0;
                playerModel.isIdle = true;
            }
            else if (input.sqrMagnitude <= .5f)
            {
                currentGait = GaitState.Walk;
                playerModel.currentGait = 1;
                playerModel.isIdle = false;
            }
            else
            {
                currentGait = GaitState.Run;
                playerModel.currentGait = 2;
                playerModel.isIdle = false;
            }


            if (playerModel.isSprinting)
            {
                currentGait = GaitState.Sprint;
                playerModel.currentGait = 3;
                playerModel.isIdle = false;
            }
        }

        public void ProcessAimInput(Vector2 mouseDelta)
        {
            playerModel.CalculateRotation(mouseDelta);
            playerView.RotateCharacter(playerModel);
        }

        public void OnJumpAnimEnd()
        {
            playerModel.isJumping = false;
        }

    }
}
