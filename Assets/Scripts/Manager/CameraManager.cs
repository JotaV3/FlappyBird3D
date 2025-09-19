using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public void Awake()
    {
        Instance = this;

        LockCursorOnScreen();
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GamePaused_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            UnlockCursorOnScreen();
        }
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        LockCursorOnScreen();
    }

    private void GamePaused_OnGamePaused(object sender, System.EventArgs e)
    {
        UnlockCursorOnScreen();
    }

    public void LockCursorOnScreen()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursorOnScreen()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
