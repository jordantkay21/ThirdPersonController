using System;
using UnityEngine;

namespace KayosStudios.ThirdPersonController
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        public PlayerModel playerModel { get; private set; }
        public PlayerView playerView { get; private set; }

        private IState _currentState;

        public PlayerStates currentState;
        public GaitState currentGait;

        public Vector2 movementInput;

        private void OnEnable()
        {
            InputHandler.Instance.OnMove += input => movementInput = input;
            InputHandler.Instance.OnSprint += isSprinting => playerModel.isSprinting = isSprinting;
            InputHandler.Instance.OnLook += mouseDelta => ProcessAimInput(mouseDelta);
        }



        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            var inputHandler = InputHandler.Instance;
            playerView = (PlayerView)FindFirstObjectByType(typeof(PlayerView));
            playerModel = new PlayerModel();
        }

        private void Start()
        {
            _currentState = new IdleState();
            _currentState.EnterState(this);
        }

        private void Update()
        {
            _currentState.UpdateState(this);
        }

        public void TransitionToState(IState newState)
        {
            _currentState.ExitState(this);
            _currentState = newState;
            newState.EnterState(this);
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute(this);
        }

        public void ProcessAimInput(Vector2 mouseDelta)
        {
            playerModel.CalculateRotation(mouseDelta);
            playerView.RotateCharacter(playerModel);
        }
    }
}
