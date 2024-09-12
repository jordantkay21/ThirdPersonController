using System;
using UnityEngine;

namespace KayosStudios.ThirdPersonController
{
    public class LocomotionState : IState
    {
        public void EnterState(PlayerController controller)
        {
            Debug.Log("Player Enters LocomotionState");
            controller.currentState = PlayerStates.Locomotion;
        }

        public void UpdateState(PlayerController controller)
        {
            if (controller.currentGait == GaitState.Idle)
                controller.TransitionToState(new IdleState());


        }

        public void ExitState(PlayerController controller)
        {
            Debug.Log("Player Exits LocomotionState");
        }
    }
}
