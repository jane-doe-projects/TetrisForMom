using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundControl : MonoBehaviour
{
    public AudioMixer mainMixer;
    public GameObject soundOnIndicator;
    public GameObject soundOffIndicator;

    public AudioSource levelUp;
    public AudioSource gameOver;
    public AudioSource line;

    public int isMuted;

    private void Start()
    {
        if (PlayerPrefs.HasKey("IsMuted"))
        {
            isMuted = PlayerPrefs.GetInt("IsMuted");
            if (isMuted == 1)
            {
                MuteSound();
            } else
            {
                UnmuteSound();
            }
        } else
        {
            PlayerPrefs.SetInt("IsMuted", 0);
        }
        PlayerPrefs.Save();
    }

    public void MuteSound()
    {
        mainMixer.SetFloat("MainVolume", -80);
        soundOffIndicator.SetActive(true);
        soundOnIndicator.SetActive(false);
        PlayerPrefs.SetInt("IsMuted", 1);
        PlayerPrefs.Save();
        isMuted = 1;
    }

    public void UnmuteSound()
    {
        mainMixer.SetFloat("MainVolume", 0);
        soundOnIndicator.SetActive(true);
        soundOffIndicator.SetActive(false);
        PlayerPrefs.SetInt("IsMuted", 0);
        PlayerPrefs.Save();
        isMuted = 0;
        
    }

    public void PlayLevelUp()
    {
        levelUp.PlayOneShot(levelUp.clip);
    }

    public void PlayLine()
    {
        line.PlayOneShot(line.clip);
    }

    public void PlayGameOver()
    {
        gameOver.PlayOneShot(gameOver.clip);
    }
}
