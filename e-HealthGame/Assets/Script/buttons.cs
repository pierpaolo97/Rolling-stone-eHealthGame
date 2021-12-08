using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class buttons : MonoBehaviour
{
    public GameObject impostazioni;
    public GameObject gameover;
    public GameObject carta;
    public GameObject menuLevel;
    public GameObject pausemenu;
    public GameObject Settings;
    public GameObject MenuInfo;
    public GameObject pause;
    public GameObject camera_;
    public GameObject startclouds;
    public GameObject score;
    public GameObject time;
    public GameObject highscores;

    static public bool checktrans2 = true;


    public static bool GameIsPaused = false;



    public void Pause()
    {
        carta.SetActive(false);
        score.SetActive(false);
        time.SetActive(false);
        pause.SetActive(false);
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pausemenu.SetActive(false);
        carta.SetActive(true);
        score.SetActive(true);
        time.SetActive(true);
        pause.SetActive(true);
        Time.timeScale = 1f;
    }

    public void playAgain()
    {
        StartCoroutine(caricaScena());
    }

    public void RestartTutorial()
    {
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.GetInt("scoreLevel", 0);

        SceneManager.LoadScene("Tutorial");
    }

    public void StartGameMenu()
    {
        //Time.timeScale = 1f;
        StartCoroutine(backHome());
        Time.timeScale = 1f;
    }

    public void difficulty()
    {
        carta.SetActive(false);
        gameover.SetActive(false);
        impostazioni.SetActive(true);
    }

    public void setting()
    {
        carta.SetActive(false);
        gameover.SetActive(false);
        Settings.SetActive(true);
    }

    public void back()
    {
        carta.SetActive(false);
        gameover.SetActive(true);
        impostazioni.SetActive(false);
        menuLevel.SetActive(false);
    }

    public void backHighScores()
    {
        highscores.SetActive(false);
        gameover.SetActive(true);
        impostazioni.SetActive(false);
        menuLevel.SetActive(false);
    }


    public void infoMenu()
    {
        carta.SetActive(false);
        gameover.SetActive(false);
        MenuInfo.SetActive(true);
    }

    public void displayHigh()
    {
        carta.SetActive(false);
        gameover.SetActive(false);
        highscores.SetActive(true);
    }




    public void changeLevel()
    {

        menuLevel.SetActive(true);
        /*switch (PlayerPrefs.GetString("LetteraLivello", "C"))
        {
            case "C":
                PlayerPrefs.SetString("LetteraLivello", "G");
                break;
            case "G":
                PlayerPrefs.SetString("LetteraLivello", "C");
                break;
        }
        
        StartCoroutine(caricaScena());*/
    }

    public void levelC()
    {
        PlayerPrefs.SetString("LetteraLivello", "C");
        camera_.GetComponent<setQuestion>().caricaDomande();
    }

    public void levelG()
    {
        PlayerPrefs.SetString("LetteraLivello", "G");
        camera_.GetComponent<setQuestion>().caricaDomande();
    }

    public void levelSC()
    {
        PlayerPrefs.SetString("LetteraLivello", "SC");
        camera_.GetComponent<setQuestion>().caricaDomande();
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

    IEnumerator caricaScena()
    {
        PlayerPrefs.SetInt("scoreLevel", 0);
        PlayerPrefs.SetInt("Level", 0);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator backHome()
    {
        startclouds.SetActive(true);
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene("MainMenu");
        checktrans2 = false;
    }
}
