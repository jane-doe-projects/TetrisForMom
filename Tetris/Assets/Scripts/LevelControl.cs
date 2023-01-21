using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour
{
    public int currentLevel;
    public float levelDropSpeed;
    public Sprite[] levelBackgrounds;

    public GameObject backgroundPanel;
    private Image backgroundImage;
    private float defaultDropSpeed = 0.8f;

    [SerializeField] float speedIncrease = 0.08f;

    public TextMeshProUGUI levelInfo;

    private void Start()
    {
        currentLevel = 1;
        levelDropSpeed = GameManager.Instance.currentFallTime;
        backgroundImage = backgroundPanel.GetComponent<Image>();
        ChangeBackground(currentLevel);
    }

    public void ReinitializeUIElements()
    {
        levelInfo = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        backgroundPanel = GameObject.Find("CameraBackgroundPanel");
        backgroundImage = backgroundPanel.GetComponent<Image>();
        ResetLevel();
    }

    void IncreaseLevel()
    {
        // increases level and updates the drop speed
        currentLevel += 1;
        UpdateLevelInfo();
        if (currentLevel < 11)
            levelDropSpeed -= speedIncrease;
        else
            levelDropSpeed -= speedIncrease / 4;
        GameManager.Instance.currentFallTime = levelDropSpeed;
        Debug.Log("Current drop speed:" + levelDropSpeed);
    }

    public void ResetLevel()
    {
        currentLevel = 1;
        levelDropSpeed = defaultDropSpeed;
        GameManager.Instance.currentFallTime = levelDropSpeed;
        UpdateLevelInfo();
        ChangeBackground(currentLevel);
    }

    void UpdateLevelInfo()
    {
        levelInfo.text = currentLevel.ToString();
    }

    public void LevelUp()
    {
        IncreaseLevel();
        if (!(currentLevel > 10))
            ChangeBackground(currentLevel);
    }

    private void ChangeBackground(int level)
    {
        backgroundImage.sprite = levelBackgrounds[level-1];
    }

}
