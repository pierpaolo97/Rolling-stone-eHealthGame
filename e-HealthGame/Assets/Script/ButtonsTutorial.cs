using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsTutorial : MonoBehaviour
{
    public GameObject pausemenu;
    public GameObject pause;

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void RestartTutorial()
    {
        PlayerPrefs.SetInt("Level", 0);
        SceneManager.LoadScene("Tutorial");
    }

    public void StartGameMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        pause.SetActive(false);
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pausemenu.SetActive(false);
        pause.SetActive(true);
        Time.timeScale = 1f;
    }
}

    
