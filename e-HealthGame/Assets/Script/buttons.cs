using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class buttons : MonoBehaviour
{
    public GameObject impostazioni;
    public GameObject gameover;

    public void playAgain()
    {
        StartCoroutine(caricaScena());
    }


    public void settings()
    {
        gameover.SetActive(false);
        impostazioni.SetActive(true);
    }

    public void back()
    {
        gameover.SetActive(true);
        impostazioni.SetActive(false);
    }

    public void changeLevel()
    {
        switch (PlayerPrefs.GetString("LetteraLivello", "C"))
        {
            case "C":
                PlayerPrefs.SetString("LetteraLivello", "S");
                break;
            case "S":
                PlayerPrefs.SetString("LetteraLivello", "C");
                break;
        }
        
        StartCoroutine(caricaScena());
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

}
