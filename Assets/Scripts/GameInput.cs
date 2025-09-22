using System;
using Unity.Cinemachine;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnJump;

    private PlayerInputActions playerInputActions;
    private Vector2 lookInput;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // pular
        playerInputActions.Player.Jump.performed += Jump_performed;

        // movimento do mouse
        playerInputActions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerInputActions.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    private void OnDestroy()
    {
        if(playerInputActions != null)
        {
            playerInputActions.Player.Disable();
            playerInputActions.Player.Jump.performed -= Jump_performed;
            playerInputActions.Dispose();
        }
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJump?.Invoke(this, EventArgs.Empty);
    }

    public void DisableJump()
    {
        playerInputActions.Player.Jump.Disable();
    }

    public Vector2 GetLookInput()
    {
        return lookInput;
    }
}
