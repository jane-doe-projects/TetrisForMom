using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] int currentScore;
    [SerializeField] int currentLines;
    [SerializeField] int linesTilLevelUp;

    [SerializeField] int scorePerBrick = 10;
    [SerializeField] int scorePerLine = 50;

    public TextMeshProUGUI lineInfo;
    public TextMeshProUGUI scoreInfo;

    private void Start()
    {
        linesTilLevelUp = 10;
    }

    public void IncreaseLineCount()
    {
        currentLines += 1;
        linesTilLevelUp--;
        UpdateLineInfo();
        IncreaseScoreCountForLine();

        if (linesTilLevelUp == 0)
        {
            GameManager.Instance.levelControl.LevelUp();
            linesTilLevelUp = 10 + GameManager.Instance.levelControl.currentLevel;
            GameManager.Instance.soundControl.PlayLevelUp();
        } else
        {
            GameManager.Instance.soundControl.PlayLine();
        }
    }

    public void IncreaseScoreCount()
    {
        currentScore += scorePerBrick;
        UpdateScoreInfo();
    }

    public void IncreaseScoreCountForLine()
    {
        currentScore += scorePerLine;
        UpdateScoreInfo();
    }

    void UpdateLineInfo()
    {
        lineInfo.text = currentLines.ToString();
    }

    void UpdateScoreInfo()
    {
        scoreInfo.text = currentScore.ToString();
    }

    public void ReinitializeUIElements()
    {
        lineInfo = GameObject.Find("Lines").GetComponent<TextMeshProUGUI>();
        scoreInfo = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        ResetCurrentValues();
    }

    void ResetCurrentValues()
    {
        currentLines = 0;
        currentScore = 0;
        UpdateLineInfo();
        UpdateScoreInfo();
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
        currentLines = 0;
        UpdateScoreInfo();
        UpdateLineInfo();
    }
}
