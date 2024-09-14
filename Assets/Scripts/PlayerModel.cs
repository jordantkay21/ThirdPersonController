using System;
using System.Collections;
using UnityEngine;

namespace KayosStudios.ThirdPersonController
{
    public enum GaitState
    {
        Idle,
        Walk,
        Run,
        Sprint
    }

    /// <summary>
    /// Stores and manages the player's core attributes and state flags.
    /// Provides methods for adjusting player data.
    /// </summary>
    public class PlayerModel
    {
        #region Locomotion Variables
        public bool isSprinting;
        public bool isIdle;
        public float moveSpeed;
        public float inputX;
        public float inputZ;
        public float inclineAngle;
        public bool isGrounded = true;
        public bool isJumping = false;
        public float verticalVelocity;
        public float fallDuration;
        public int currentGait;
        #endregion

        #region Aim Variables
        private float mouseSensitivity = 1f;
        [Tooltip("Adjusts the vertical rotation (up and down)")]
        public float xRotation;
        [Tooltip("Adjusts the horizontal rotation (left and right)")]
        public float yRotation;
        public float turnSpeed = 15;
        #endregion

        private float accelerationRate = 3f;
        private float decelerationRate = 2f;
        private float gravity = -9.81f;
        private float jumpForce = 5f;


        public void AdjustLocomotionData(Vector2 input)
        {
            if (input != Vector2.zero)
            {
                if (isSprinting)
                    moveSpeed = Mathf.Lerp(moveSpeed, 2f, accelerationRate * Time.deltaTime);
                else
                    moveSpeed = Mathf.Lerp(moveSpeed, input.sqrMagnitude, accelerationRate * Time.deltaTime);

                inputX = input.x;
                inputZ = input.y;
            }
            else
            {
                //Gradually slow down to 0 when no input is detected
                moveSpeed = Mathf.Lerp(moveSpeed, 0f, Time.deltaTime * decelerationRate);

                //Reset the inputX and inputZ when fully stopped
                if (moveSpeed <= 0.01f)
                {
                    inputX = 0f;
                    inputZ = 0f;
                    moveSpeed = 0;
                }
            }

        }

        public void CalculateGravity()
        {
            if (isGrounded)
            {
                fallDuration = 0f;
                verticalVelocity = 0f;

                if (isJumping)
                {
                    verticalVelocity = jumpForce;
                    isJumping = false; // Reset jump state after applying force
                }
            }
            else
            {
                verticalVelocity += gravity * Time.deltaTime;
                fallDuration += Time.deltaTime; // Track falling duration
            }
        }
        public void Jump()
        {
            if (isGrounded)
            {
                isJumping = true;
            }
        }

        public Vector3 CalculateRootMotion(Vector3 rootMotion)
        {
            rootMotion.y = verticalVelocity * Time.deltaTime;
            return rootMotion;
        }
        public void CalculateRotation(Vector2 mouseDelta)
        {
            //Get mouse input and adjust by sensitivity
            yRotation = mouseDelta.x * mouseSensitivity * Time.deltaTime;
            float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

            //Clamp the vertical rotation to prevent over-rotating
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        }

    }
}
