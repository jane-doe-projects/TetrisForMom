using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    bool isPaused;
    bool gameOver;
    bool hasHighscore;
    bool showsHelp;

    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject highScorePanel;
    public GameObject helpPanel;

    public float currentFallTime = 0.8f;

    public Score playerScore;
    public LevelControl levelControl;
    public Timer timer;
    public Highscore highscoreControl;
    private SpawnControl spawnControl;
    public SoundControl soundControl;

    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            GameManager.Instance.ReinitializeStates();
            GameManager.Instance.playerScore.ReinitializeUIElements();
            GameManager.Instance.levelControl.ReinitializeUIElements();
            GameManager.Instance.timer.ReinitializeUIElements();
            GameManager.Instance.highscoreControl.ReinitializeUIElements();
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        DisableCursor();
        isPaused = false;
        gameOver = false;
        showsHelp = false;
        playerScore = GetComponent<Score>();
        levelControl = GetComponent<LevelControl>();
        timer = GetComponent<Timer>();
        highscoreControl = GetComponent<Highscore>();
        //highscoreControl.LoadScores();
        spawnControl = GameObject.Find("Spawn").GetComponent<SpawnControl>();
        soundControl = GetComponent<SoundControl>();

        ReinitializeStates();
        playerScore.ReinitializeUIElements();
        levelControl.ReinitializeUIElements();
        timer.ReinitializeUIElements();
        highscoreControl.ReinitializeUIElements();
        highscoreControl.InitializeHighscorePanel();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            SwitchHelpPanel();

        if (Input.GetKeyDown(KeyCode.P))
            SwitchPauseState();
        if (Input.GetKeyDown(KeyCode.F2))
            RestartGame();

        if (Input.GetKeyDown(KeyCode.F8))
            ShowHideHighscorePanel();

        if (Input.GetKeyDown(KeyCode.M))
            SwitchSoundState();

        if (Input.GetKeyDown(KeyCode.F12))
            ExitGame();
    }

    void DisableCursor()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void RestartGame()
    {
        if (isPaused)
            SwitchPauseState();
        if (showsHelp)
            SwitchHelpPanel();
        if (gameOver)
        {
            if (hasHighscore)
            {
                hasHighscore = false;
                highscoreControl.scoreGameOverPanel.SetActive(false);
                StopAllCoroutines();
            }
            else
                gameOverPanel.SetActive(false);
            gameOver = false;

        }

        // reset necessary values instead of reloading and reassigning the scene
        ResetGameValues();
        spawnControl.SpawnRandomBlock();
    }

    void SwitchPauseState()
    {
        if (isPaused)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            Debug.Log("Unpaused.");
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Paused.");
        }

        isPaused = !isPaused;
    }

    void ShowHideHighscorePanel()
    {
        if (!highScorePanel.activeSelf)
            highScorePanel.SetActive(true);
        else
            highScorePanel.SetActive(false);
    }

    public void GameOver()
    {
        if (highscoreControl.CheckIfHighscore(playerScore.GetScore()) != -1) {
            hasHighscore = true;
            highscoreControl.scoreGameOverPanel.SetActive(true);
            highscoreControl.AddToHighscore(playerScore.GetScore());
        } else
        {
            gameOverPanel.SetActive(true);
        }

        soundControl.PlayGameOver();
        gameOver = true;
        timer.StopTimer();
    }


    void ReinitializeStates()
    {
        isPaused = false;
        gameOver = false;

        pausePanel = HelperFunctions.FindInactiveGameObjectWithName("PausePanel");
        gameOverPanel = HelperFunctions.FindInactiveGameObjectWithName("GameOverPanel");
        highScorePanel = HelperFunctions.FindInactiveGameObjectWithName("HighscorePanel");

        if (gameOverPanel == null || pausePanel == null || highScorePanel == null)
            Debug.Log("Panel reinitialization failed.");
        else
            Debug.Log("Restarted.");
    }

    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void ResetGameValues()
    {
        timer.ResetTime();
        playerScore.ResetScore();
        levelControl.ResetLevel();
        ResetPlayfield();
    }

    void ResetPlayfield()
    {
        spawnControl.ResetGrid();
        spawnControl.ClearBlocks();
    }

    void SwitchSoundState()
    {
        if (soundControl.isMuted == 0)
            soundControl.MuteSound();
        else
            soundControl.UnmuteSound();
    }

    void SwitchHelpPanel()
    {
        if(showsHelp)
        {
            helpPanel.SetActive(false);
            showsHelp = false;
        } else
        {
            helpPanel.SetActive(true);
            showsHelp = true;
        }
    }

    private void OnApplicationQuit()
    {
        GetComponent<WindowResize>().SaveCurrentWindowSize();
    }
}
