using System;
using UnityEngine;

public class LocomotionState : IState
{
    public override void EnterState(PlayerController controller)
    {
        Debug.Log("Player Enters LocomotionState");
        controller.currentState = AnimationState.Locomotion;
    }

    public override void UpdateState(PlayerController controller)
    {
        if (controller.currentGait == GaitState.Idle)
            controller.TransitionToState(new IdleState());


    }

    public override void ExitState(PlayerController controller)
    {
        Debug.Log("Player Exits LocomotionState");
    }
}
