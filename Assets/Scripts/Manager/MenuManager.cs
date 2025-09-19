using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [SerializeField] private MenuUI menuUI;
    [SerializeField] private SettingsUI settingsUI;

    public event EventHandler OnCloseSettingsUI;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // pausar
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        if(playerInputActions != null)
        {
            playerInputActions.Player.Disable();
            playerInputActions.Player.Pause.performed -= Pause_performed;
            playerInputActions.Dispose();
        }
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!GameManager.Instance.IsGameOver())
        {// se não estiver no estado GameOver
            if (settingsUI.isActiveAndEnabled)
            {// se as configurações estiverem abertas, feche-a
                CloseSettingsUI();
            }
            else if (menuUI.isActiveAndEnabled)
            {// se o menu estiver ativo, esconda-o e despause o jogo
                menuUI.Hide();
                GameManager.Instance.TogglePauseGame();
            }
            else
            {// se o menu estiver desativado, ative-o e pause o jogo
                menuUI.Show();
                GameManager.Instance.TogglePauseGame();
            }
        }
    }

    public void OpenSettingsUI()
    {
        settingsUI.Show();
        menuUI.Hide();
    }

    public void CloseSettingsUI()
    {
        settingsUI.Hide();
        menuUI.Show();

        OnCloseSettingsUI?.Invoke(this, EventArgs.Empty);
    }
}
