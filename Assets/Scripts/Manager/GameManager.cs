using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    public enum State
    {
        WaitingToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;

        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnJump += GameInput_OnJump;
    }

    private void OnDestroy()
    {
        if(GameInput.Instance != null)
        {
            GameInput.Instance.OnJump -= GameInput_OnJump;
        }
    }

    private void GameInput_OnJump(object sender, EventArgs e)
    {
        if(IsWaitingToStart() & !isGamePaused)
        {
            PressSpaceToStartUI.Instance.Hide();

            state = State.GamePlaying;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    // verificar dps
    public void GameOver()
    {
        state = State.GameOver;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsWaitingToStart()
    {
        return state == State.WaitingToStart;
    }

    public bool GetIsGamePaused()
    {
        return isGamePaused;
    }
}
