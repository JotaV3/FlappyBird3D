using System;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI Instance { get; private set; }

    [SerializeField] TextMeshProUGUI scoreText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}
