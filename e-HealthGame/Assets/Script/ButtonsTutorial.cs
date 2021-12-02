using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsTutorial : MonoBehaviour
{
    public GameObject pausemenu;
    public GameObject pause;
    public GameObject startclouds;
    static public bool checktrans = true;


    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void RestartTutorial()
    {
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.GetInt("scoreLevel", 0);
        SceneManager.LoadScene("Tutorial");
    }

    public void StartGameMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(backHome());
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

    IEnumerator backHome()
    {
        startclouds.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene("MainMenu");
        checktrans = false;
    }
}

    
