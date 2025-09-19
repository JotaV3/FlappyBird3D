using System;
using TMPro;
using UnityEngine;

public class GameTimerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameTimerText;

    private float elapsedTime = 0f;
    private int seconds = 0;
    private int minutes = 0;

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        elapsedTime += Time.deltaTime;

        seconds = Mathf.FloorToInt(elapsedTime % 60);
        minutes = Mathf.FloorToInt(elapsedTime / 60);

        gameTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
