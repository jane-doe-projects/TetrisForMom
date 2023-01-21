using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowResize : MonoBehaviour
{
    public static int minWidth = 800;
    public static int minHeight = 600;

    private string windowWidthKey = "WinWidth";
    private string windowHeightKey = "WinHeight";

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(windowWidthKey))
        {
            int width = PlayerPrefs.GetInt(windowWidthKey);
            int height = PlayerPrefs.GetInt(windowHeightKey);
            Screen.SetResolution(width, height, false);
        } else
        {
            PlayerPrefs.SetInt(windowWidthKey, minWidth);
            PlayerPrefs.SetInt(windowHeightKey, minHeight);
        }
        PlayerPrefs.Save();
    }

    public void SaveCurrentWindowSize()
    {
        int currentWidth = Screen.width;
        int currentHeight = Screen.height;
        PlayerPrefs.SetInt(windowWidthKey, currentWidth);
        PlayerPrefs.SetInt(windowHeightKey, currentHeight);
        PlayerPrefs.Save();
    }
}
