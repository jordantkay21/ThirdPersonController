using UnityEngine;

public class IdleState : IState
{


    public override void EnterState(PlayerController controller)
    {
        Debug.Log("Player Enters IdleState");
        controller.currentState = AnimationState.Idle;
    }
    public override void UpdateState(PlayerController controller)
    {
        if (controller.currentGait == GaitState.Walk || controller.currentGait == GaitState.Run || controller.currentGait == GaitState.Sprint)
            controller.TransitionToState(new LocomotionState());
    }

    public override void ExitState(PlayerController ccntroller)
    {
        Debug.Log("Player Exits IdleState");
    }


}
