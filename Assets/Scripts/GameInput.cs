using System;
using Unity.Cinemachine;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnJump;
    public event EventHandler OnPauseAction;

    private PlayerInputActions playerInputActions;
    private Vector2 lookInput;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Jump.performed += Jump_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
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

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
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
        return playerInputActions.Player.Look.ReadValue<Vector2>(); ;
    }

    public Vector3 GetInputNormalized()
    {
        Vector3 inputVector = playerInputActions.Player.Move.ReadValue<Vector3>();
        return inputVector;
    }
}
