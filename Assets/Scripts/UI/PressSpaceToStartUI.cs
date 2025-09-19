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

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
