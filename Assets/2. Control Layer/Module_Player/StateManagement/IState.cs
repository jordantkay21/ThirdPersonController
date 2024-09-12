using UnityEngine;

public enum GameStates
{
    Playing,
    Pause
}

namespace KayosStudios.ThirdPersonController
{
    public enum PlayerStates
    {
        Idle,
        Locomotion,
        Jump,
        Fall,
        Crouch
    }

    public enum GaitState
    {
        Idle,
        Crouch,
        Walk,
        Run,
        Sprint
    }

    public interface IState
    {
        /// <summary>
        /// Called when entering this state. Use to initialize state-specific variables, set up behavior, or trigger animations.
        /// </summary>
        /// <param name="controller"></param>
        public void EnterState(PlayerController controller);
        /// <summary>
        /// Called every frame while in this state. Use for logic that needs to be continuously checked or updated, such as movement or ongoing effects.
        /// </summary>
        /// <param name="controller"></param>
        public void UpdateState(PlayerController controller);
        /// <summary>
        /// Called when exiting this state. Use to clean up or reset variables, stop animations, or handle transitions.
        /// </summary>
        /// <param name="controller"></param>
        public void ExitState(PlayerController controller);

    }
}
