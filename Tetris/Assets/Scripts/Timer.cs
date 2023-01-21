using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float elapsedTime;
    public float startTime;
    public bool runTimer;

    public TextMeshProUGUI matchTimer;
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        startTime = Time.time;
        runTimer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (runTimer)
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        // update the elapsed match time and the timer shown in the ui
        elapsedTime = Time.time - startTime;
        string minutes = (elapsedTime / 60).ToString("00");
        string seconds = (elapsedTime % 60).ToString("00");

        matchTimer.text = minutes + ":" + seconds;
    }

    public void ReinitializeUIElements()
    {
        matchTimer = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        ResetTime();
    }

    public void ResetTime()
    {
        elapsedTime = 0;
        startTime = Time.time;
        runTimer = true;
    }

    public void StopTimer()
    {
        runTimer = false;
    }

}
