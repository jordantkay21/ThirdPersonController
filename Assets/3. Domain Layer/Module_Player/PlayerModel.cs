using System;
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
        public float moveSpeed;
        public float inputX;
        public float inputZ;
        #endregion

        private float mouseSensitivity = 1f;
        [Tooltip("Adjusts the vertical rotation (up and down)")]
        public float xRotation;
        [Tooltip("Adjusts the horizontal rotation (left and right)")]
        public float yRotation;
        public float turnSpeed = 15;


        public void AdjustLocomotionData(Vector2 input)
        {
            if (input != Vector2.zero && isSprinting)
                moveSpeed = 2;
            else
                moveSpeed = input.sqrMagnitude;

            inputX = input.x;
            inputZ = input.y;
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
