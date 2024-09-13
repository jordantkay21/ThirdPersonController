using UnityEngine;

namespace KayosStudios.ThirdPersonController
{
    public class IdleState : IState
    {
        public void EnterState(PlayerController controller)
        {
            Debug.Log("Player Enters IdleState");
            controller.currentState = PlayerStates.Idle;
        }
        public void UpdateState(PlayerController controller)
        {
            if (controller.currentGait == GaitState.Walk || controller.currentGait == GaitState.Run || controller.currentGait == GaitState.Sprint)
                controller.TransitionToState(new LocomotionState());
        }

        public void ExitState(PlayerController ccntroller)
        {
            Debug.Log("Player Exits IdleState");
        }


    }
}
