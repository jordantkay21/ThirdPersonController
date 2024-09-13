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
        [SerializeField] float raycastDistance = 1.1f; // Raycast distance slightly longer than the player's height

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

            bool isGrounded = playerView.characterController.isGrounded;

            playerModel.UpdateGravity(isGrounded);

            
            playerView.UpdateAnimator(playerModel);
            playerStatsUI.UpdateUIVariables(playerModel);  
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

    }
}
