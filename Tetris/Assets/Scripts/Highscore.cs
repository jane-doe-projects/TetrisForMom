using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class Highscore : MonoBehaviour
{
    private SavedScore[] savedScores;
    public TextMeshProUGUI highscoreInputName;
    public TMP_InputField highscoreInputField;
    public GameObject scoreGameOverPanel;

    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;
    public GameObject slot5;

    public void AddToHighscore(int score)
    {

        StartCoroutine("GetUserInput");
    }

    public int CheckIfHighscore(int score)
    {
        // check highscore list and see if score can be placed as new item
        for (int i = 0; i < savedScores.Length; i++)
        {
            if (savedScores[i].score < score)
                return i;
        }
        return -1;
    }

    [System.Serializable]
    public class SavedScore
    {
        public string name;
        public int score;
        public string date;
    }
 

    public void InitializeHighscorePanel()
    {
        if (PlayerPrefs.HasKey("Score5"))
        {
            LoadExistingHighscores();
        } else
        {
            CreateEmptyHighscores();
            LoadExistingHighscores();
        }
    }

    public void ReinitializeUIElements()
    {
        slot1 = HelperFunctions.FindInactiveGameObjectWithName("Slot1");
        slot2 = HelperFunctions.FindInactiveGameObjectWithName("Slot2");
        slot3 = HelperFunctions.FindInactiveGameObjectWithName("Slot3");
        slot4 = HelperFunctions.FindInactiveGameObjectWithName("Slot4");
        slot5 = HelperFunctions.FindInactiveGameObjectWithName("Slot5");
        InitializeHighscorePanel();
    }

    void CreateEmptyHighscores()
    {
        // set new empty scores
        PlayerPrefs.SetString("Name1", "empty");
        PlayerPrefs.SetString("Name2", "empty");
        PlayerPrefs.SetString("Name3", "empty");
        PlayerPrefs.SetString("Name4", "empty");
        PlayerPrefs.SetString("Name5", "empty");

        PlayerPrefs.SetString("Date1", "empty");
        PlayerPrefs.SetString("Date2", "empty");
        PlayerPrefs.SetString("Date3", "empty");
        PlayerPrefs.SetString("Date4", "empty");
        PlayerPrefs.SetString("Date5", "empty");

        PlayerPrefs.SetInt("Score1", 0);
        PlayerPrefs.SetInt("Score2", 0);
        PlayerPrefs.SetInt("Score3", 0);
        PlayerPrefs.SetInt("Score4", 0);
        PlayerPrefs.SetInt("Score5", 0);

        PlayerPrefs.Save();
    }

    void LoadExistingHighscores()
    {
        savedScores = new SavedScore[5];
        savedScores[0] = new SavedScore();
        savedScores[1] = new SavedScore();
        savedScores[2] = new SavedScore();
        savedScores[3] = new SavedScore();
        savedScores[4] = new SavedScore();

        savedScores[0].name = PlayerPrefs.GetString("Name1");
        savedScores[0].score = PlayerPrefs.GetInt("Score1");
        savedScores[0].date = PlayerPrefs.GetString("Date1");

        slot1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Name1");
        slot1.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score1").ToString();
        slot1.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Date1");

        savedScores[1].name = PlayerPrefs.GetString("Name2");
        savedScores[1].score = PlayerPrefs.GetInt("Score2");
        savedScores[1].date = PlayerPrefs.GetString("Date2");

        slot2.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Name2");
        slot2.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score2").ToString();
        slot2.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Date2");

        savedScores[2].name = PlayerPrefs.GetString("Name3");
        savedScores[2].score = PlayerPrefs.GetInt("Score3");
        savedScores[2].date = PlayerPrefs.GetString("Date3");

        slot3.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Name3");
        slot3.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score3").ToString();
        slot3.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Date3");

        savedScores[3].name = PlayerPrefs.GetString("Name4");
        savedScores[3].score = PlayerPrefs.GetInt("Score4");
        savedScores[3].date = PlayerPrefs.GetString("Date4");

        slot4.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Name4");
        slot4.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score4").ToString();
        slot4.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Date4");

        savedScores[4].name = PlayerPrefs.GetString("Name5");
        savedScores[4].score = PlayerPrefs.GetInt("Score5");
        savedScores[4].date = PlayerPrefs.GetString("Date5");

        slot5.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Name5");
        slot5.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score5").ToString();
        slot5.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Date5");

    }

    private void ReplaceHighscore(int position, int score, string highscoreName)
    {
        string currentTimeDate = System.DateTime.Now.ToString("HH:mm dd/MM/yyyy");
        if (position == 4)
        {
            PlayerPrefs.SetString("Name5", highscoreName);
            PlayerPrefs.SetInt("Score5", score);
            PlayerPrefs.SetString("Date5", currentTimeDate);
            PlayerPrefs.Save();
        } else if (position == 3)
        {
            PlayerPrefs.SetString("Name5", PlayerPrefs.GetString("Name4"));
            PlayerPrefs.SetInt("Score5", PlayerPrefs.GetInt("Score4"));
            PlayerPrefs.SetString("Date5", PlayerPrefs.GetString("Date4"));

            PlayerPrefs.SetString("Name4", highscoreName);
            PlayerPrefs.SetInt("Score4", score);
            PlayerPrefs.SetString("Date4", currentTimeDate);

            PlayerPrefs.Save();
        } else if (position == 2)
        {
            PlayerPrefs.SetString("Name5", PlayerPrefs.GetString("Name4"));
            PlayerPrefs.SetInt("Score5", PlayerPrefs.GetInt("Score4"));
            PlayerPrefs.SetString("Date5", PlayerPrefs.GetString("Date4"));

            PlayerPrefs.SetString("Name4", PlayerPrefs.GetString("Name3"));
            PlayerPrefs.SetInt("Score4", PlayerPrefs.GetInt("Score3"));
            PlayerPrefs.SetString("Date4", PlayerPrefs.GetString("Date3"));

            PlayerPrefs.SetString("Name3", highscoreName);
            PlayerPrefs.SetInt("Score3", score);
            PlayerPrefs.SetString("Date3", currentTimeDate);
            PlayerPrefs.Save();
        } else if (position == 1)
        {
            PlayerPrefs.SetString("Name5", PlayerPrefs.GetString("Name4"));
            PlayerPrefs.SetInt("Score5", PlayerPrefs.GetInt("Score4"));
            PlayerPrefs.SetString("Date5", PlayerPrefs.GetString("Date4"));

            PlayerPrefs.SetString("Name4", PlayerPrefs.GetString("Name3"));
            PlayerPrefs.SetInt("Score4", PlayerPrefs.GetInt("Score3"));
            PlayerPrefs.SetString("Date4", PlayerPrefs.GetString("Date3"));

            PlayerPrefs.SetString("Name3", PlayerPrefs.GetString("Name2"));
            PlayerPrefs.SetInt("Score3", PlayerPrefs.GetInt("Score2"));
            PlayerPrefs.SetString("Date3", PlayerPrefs.GetString("Date2"));

            PlayerPrefs.SetString("Name2", highscoreName);
            PlayerPrefs.SetInt("Score2", score);
            PlayerPrefs.SetString("Date2", currentTimeDate);

            PlayerPrefs.Save();
        } else if (position == 0)
        {
            PlayerPrefs.SetString("Name5", PlayerPrefs.GetString("Name4"));
            PlayerPrefs.SetInt("Score5", PlayerPrefs.GetInt("Score4"));
            PlayerPrefs.SetString("Date5", PlayerPrefs.GetString("Date4"));

            PlayerPrefs.SetString("Name4", PlayerPrefs.GetString("Name3"));
            PlayerPrefs.SetInt("Score4", PlayerPrefs.GetInt("Score3"));
            PlayerPrefs.SetString("Date4", PlayerPrefs.GetString("Date3"));

            PlayerPrefs.SetString("Name3", PlayerPrefs.GetString("Name2"));
            PlayerPrefs.SetInt("Score3", PlayerPrefs.GetInt("Score2"));
            PlayerPrefs.SetString("Date3", PlayerPrefs.GetString("Date2"));

            PlayerPrefs.SetString("Name2", PlayerPrefs.GetString("Name1"));
            PlayerPrefs.SetInt("Score2", PlayerPrefs.GetInt("Score1"));
            PlayerPrefs.SetString("Date2", PlayerPrefs.GetString("Date1"));

            PlayerPrefs.SetString("Name1", highscoreName);
            PlayerPrefs.SetInt("Score1", score);
            PlayerPrefs.SetString("Date1", currentTimeDate);

            PlayerPrefs.Save();
        }

    }

    IEnumerator GetUserInput()
    {
        int score = GameManager.Instance.playerScore.GetScore();
        int position = CheckIfHighscore(score);
        highscoreInputField.ActivateInputField();

        while (!Input.GetKeyDown(KeyCode.Return))
        {
            yield return null;
        }
        scoreGameOverPanel.SetActive(false);

        string highscoreName = highscoreInputName.text;
        ReplaceHighscore(position, score, highscoreName);
        InitializeHighscorePanel();
    }

}
