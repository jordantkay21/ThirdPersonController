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

        #region Animation Hash Variables
        public int isIdleHash { get; private set; }
        public int moveSpeedHash { get; private set; }
        public int inputXHash { get; private set; }
        public int inputZHash { get; private set; }
        public int isGroundedHash { get; private set; }
        #endregion

        #region Component Init
        private void InitilizeStringHash()
        {
            isIdleHash = Animator.StringToHash("isIdle");
            moveSpeedHash = Animator.StringToHash("MoveSpeed");
            inputXHash = Animator.StringToHash("InputX");
            inputZHash = Animator.StringToHash("InputZ");
            isGroundedHash = Animator.StringToHash("isGrounded");
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

        public void MoveCharacter(PlayerModel model)
        {
            animator.SetFloat(moveSpeedHash, model.moveSpeed);
            animator.SetFloat(inputXHash, model.inputX);
            animator.SetFloat(inputZHash, model.inputZ);
        }

        internal void RotateCharacter(PlayerModel playerModel)
        {
            float yawCamera = mainCamera.transform.rotation.eulerAngles.y;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), playerModel.turnSpeed * Time.deltaTime);
        }
    }
}
