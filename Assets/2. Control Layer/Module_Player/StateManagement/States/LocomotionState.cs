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
            controller.playerModel.isIdle = false;
            controller.playerView.animator.SetBool(controller.playerView.isIdleHash, false);
        }

        public void UpdateState(PlayerController controller)
        {
            controller.playerModel.AdjustLocomotionData(controller.movementInput);
            controller.playerView.MoveCharacter(controller.playerModel);

            if (controller.movementInput == Vector2.zero)
            {
                controller.currentGait = GaitState.Idle;
                controller.TransitionToState(new IdleState());
            }
            else if (controller.movementInput.sqrMagnitude <= .5f)
            {
                controller.currentGait = GaitState.Walk;
                controller.playerView.animator.SetInteger(controller.playerView.CurrentGaitHash, 1);
            }
            else
            {
                controller.currentGait = GaitState.Run;
                controller.playerView.animator.SetInteger(controller.playerView.CurrentGaitHash, 2);
            }


            if (controller.playerModel.isSprinting)
            {
                controller.currentGait = GaitState.Sprint;
                controller.playerView.animator.SetInteger(controller.playerView.CurrentGaitHash, 3);
            }

        }

        public void ExitState(PlayerController controller)
        {
            Debug.Log("Player Exits LocomotionState");
        }
    }
}
