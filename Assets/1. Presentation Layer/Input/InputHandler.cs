using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance;

    public InputSystem_Actions InputActions { get; private set; }


    public event Action<Vector2> OnMove; //Event Triggered when movement input is recieved
    public event Action<Vector2> OnLook; //Event triggered when look input is recieved
    public event Action<bool> OnSprint; //Triggered when the sprint button is pressed/released

    public Vector2 moveInput { get; private set; }
    private void OnEnable()
    {
        InputActions.Player.Enable();
    }
    private void OnDisable()
    {
        InputActions.Player.Disable();
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        InputActions = new InputSystem_Actions();

        InputActions.Player.Move.performed += ctx => SetMoveInput(ctx.ReadValue<Vector2>());
        InputActions.Player.Move.canceled += ctx => SetMoveInput(Vector2.zero);
        InputActions.Player.Look.performed += ctx => OnLook?.Invoke(ctx.ReadValue<Vector2>());
        InputActions.Player.Sprint.performed += ctx => OnSprint?.Invoke(true);
        InputActions.Player.Sprint.canceled += ctx => OnSprint?.Invoke(false);
    }

    private void Update()
    {
        OnMove?.Invoke(moveInput);
    }

    private void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

}
