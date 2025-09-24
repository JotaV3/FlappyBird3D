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

        // jump
        playerInputActions.Player.Jump.performed += Jump_performed;

        // player movement
        //playerInputActions.Player.Move.performed += Move_performed;

        // mouse movement
        playerInputActions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerInputActions.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            playerInputActions.Player.Disable();
        }
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        playerInputActions.Player.Jump.Enable();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        playerInputActions.Player.Jump.Disable();
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
