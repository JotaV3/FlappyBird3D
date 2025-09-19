using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int score;

    private void Awake()
    {
        Instance = this;
    }

    public void AddPoint(int point = 1)
    {
        score += point;
        ScoreUI.Instance.UpdateScoreText(score);
    }
}
