using System;
using UnityEngine;

public class PressSpaceToStartUI : MonoBehaviour
{
    public static PressSpaceToStartUI Instance { get; private set; }

    public void Awake()
    {
        Instance = this;

        Show();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            Hide();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
