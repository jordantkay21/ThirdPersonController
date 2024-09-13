using System;
using System.Collections;
using UnityEngine;

namespace KayosStudios.ThirdPersonController
{
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
        public int currentGait; //0 - Idle | 1 - Walking | 2 - Running | 3 - Sprinting
        private float animAccelerationRate = 3f;
        private float animDecelerationRate = 2f;
        #endregion

        #region Aim Variables
        private float mouseSensitivity = 1f;
        [Tooltip("Adjusts the vertical rotation (up and down)")]
        public float xRotation;
        [Tooltip("Adjusts the horizontal rotation (left and right)")]
        public float yRotation;
        public float turnSpeed = 15;
        #endregion

        #region Jump Variables
        bool isJumping;
        #endregion

        #region Grounded Variabled
        bool isGrounded;
        float fallDuration;
        #endregion

        public void AdjustLocomotionData(Vector2 input)
        {
            if (input != Vector2.zero)
            {
                if (isSprinting)
                    moveSpeed = Mathf.Lerp(moveSpeed, 2f, animAccelerationRate * Time.deltaTime);
                else
                    moveSpeed = Mathf.Lerp(moveSpeed, input.sqrMagnitude, animAccelerationRate * Time.deltaTime);

                inputX = input.x;
                inputZ = input.y;
            }
            else
            {
                
                //Gradually slow down to 0 when no input is detected
                moveSpeed = Mathf.Lerp(moveSpeed, 0f, Time.deltaTime * animDecelerationRate);

                //Reset the inputX and inputZ when fully stopped
                if (moveSpeed <= 0.01f)
                {
                    inputX = 0f;
                    inputZ = 0f;
                    moveSpeed = 0;
                }
            }

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
