using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingMenu : MonoBehaviour
{

    public GameObject StartingSettings;
    public GameObject pausemenu;
    public GameObject pause;

    public GameObject transition;
    public GameObject endtransition;

    public void levelC()
    {
        PlayerPrefs.SetString("LetteraLivello", "C");
    }

    public void levelG()
    {
        PlayerPrefs.SetString("LetteraLivello", "G");
    }

    public void levelSC()
    {
        PlayerPrefs.SetString("LetteraLivello", "SC");

    }

    public void easy()
    {
        PlayerPrefs.SetString("difficolta", "Easy");
    }

    public void medium()
    {
        PlayerPrefs.SetString("difficolta", "Medium");
    }

    public void hard()
    {
        PlayerPrefs.SetString("difficolta", "Hard");
    }

    public void options()
    {
        StartingSettings.SetActive(true);
    }

    public void back()
    {

        StartingSettings.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        PlayerPrefs.SetInt("scoreLevel", 0);
        PlayerPrefs.SetInt("Level", 0);
        StartCoroutine(loadGame());
    }

    public void RestartTutorial()
    {
        PlayerPrefs.SetInt("Level", 0);
        StartCoroutine(loadTutorial());
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

    IEnumerator loadGame()
    {
        transition.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator loadTutorial()
    {
        transition.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene("Tutorial");
    }



}
