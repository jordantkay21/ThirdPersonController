using UnityEngine;

/// <summary>
/// Stores and manages the player's core attributes and state flags.
/// Provides methods for adjusting player data.
/// </summary>
public class PlayerModel 
{
    public bool isSprinting;
    public float moveSpeed;
    public float inputX;
    public float inputZ;
    

    public void AdjustLocomotionData(Vector2 input)
    {
        if (input != Vector2.zero && isSprinting)
            moveSpeed = 2;
        else
            moveSpeed = input.sqrMagnitude;
        
        inputX = input.x;
        inputZ = input.y;
    }

}
