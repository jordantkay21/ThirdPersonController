using System;
using UnityEngine;

namespace KayosStudios.ThirdPersonController
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class PlayerView : MonoBehaviour
    {
        public CharacterController characterController { get; private set; }
        public Animator animator { get; private set; }
        public Camera mainCamera { get; private set; }

        public Transform head;

        public bool applyRootmotion = true;



        #region Animation Hash Variables
        public int isIdleHash { get; private set; }
        public int isGroundedHash { get; private set; }
        public int isJumpingHash { get; private set; }
        public int moveSpeedHash { get; private set; }
        public int inputXHash { get; private set; }
        public int inputZHash { get; private set; }
        public int inclineAngleHash { get; private set; }
        public int fallDurationHash { get; private set; }
        public int currentGaitHash { get; private set; }
        #endregion

        #region Component Init
        private void InitilizeStringHash()
        {
            isIdleHash = Animator.StringToHash("isIdle");
            isGroundedHash = Animator.StringToHash("isGrounded");
            isJumpingHash = Animator.StringToHash("isJumping");
            moveSpeedHash = Animator.StringToHash("MoveSpeed");
            inputXHash = Animator.StringToHash("InputX");
            inputZHash = Animator.StringToHash("InputZ");
            inclineAngleHash = Animator.StringToHash("InclineAngle");
            fallDurationHash = Animator.StringToHash("FallDuration");
            currentGaitHash = Animator.StringToHash("CurrentGait");

        }

        private void InitializeAnimator()
        {
            animator = GetComponent<Animator>();
        }

        private void InitializeCharacterController()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void InitializeMainCamera()
        {
            mainCamera = Camera.main;
        }
        #endregion


        private void Awake()
        {
            InitializeAnimator();
            InitilizeStringHash();
            InitializeCharacterController();
            InitializeMainCamera();
        }

        private void OnAnimatorMove()
        {

                Vector3 rootMotion = PlayerController.Instance.playerModel.CalculateRootMotion(animator.deltaPosition);

                characterController.Move(rootMotion);

        }

        public void ApplyGravity(float fallVelocity)
        {
            characterController.Move(new Vector3(0, fallVelocity * Time.deltaTime, 0));
        }

        public void UpdateAnimator(PlayerModel model)
        {
            animator.SetBool(isGroundedHash, model.isGrounded);
            animator.SetBool(isJumpingHash, model.isJumping);
            animator.SetBool(isIdleHash, model.isIdle);

            animator.SetFloat(moveSpeedHash, model.moveSpeed);
            animator.SetFloat(inputXHash, model.inputX);
            animator.SetFloat(inputZHash, model.inputZ);
            animator.SetFloat(inclineAngleHash, model.inclineAngle);
            animator.SetFloat(fallDurationHash, model.fallDuration);
            animator.SetInteger(currentGaitHash, model.currentGait);
        }

        internal void RotateCharacter(PlayerModel playerModel)
        {
            float yawCamera = mainCamera.transform.rotation.eulerAngles.y;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), playerModel.turnSpeed * Time.deltaTime);
        }
    }
}
