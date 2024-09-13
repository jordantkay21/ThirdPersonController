using UnityEngine;

namespace KayosStudios.ThirdPersonController
{
    public class IdleState : IState
    {
        public void EnterState(PlayerController controller)
        {
            Debug.Log("Player Enters IdleState");
            controller.currentState = PlayerStates.Idle;
            controller.currentGait = GaitState.Idle;
            controller.playerView.animator.SetInteger(controller.playerView.CurrentGaitHash, 0);
            controller.playerView.animator.SetBool(controller.playerView.isIdleHash, true);
            controller.playerModel.isIdle = true;
            
        }
        public void UpdateState(PlayerController controller)
        {
            if (controller.movementInput != Vector2.zero)
                controller.TransitionToState(new LocomotionState());
        }

        public void ExitState(PlayerController controller)
        {
            Debug.Log("Player Exits IdleState");
            controller.playerView.animator.SetBool(controller.playerView.isIdleHash, false);
            controller.playerModel.isIdle = false;
        }


    }
}
