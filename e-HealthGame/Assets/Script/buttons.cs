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

    public void playAgain()
    {
        StartCoroutine(caricaScena());
    }


    public void settings()
    {
        carta.SetActive(false);
        gameover.SetActive(false);
        impostazioni.SetActive(true);
    }

    public void back()
    {
        carta.SetActive(false);
        gameover.SetActive(true);
        impostazioni.SetActive(false);
        menuLevel.SetActive(false);
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

    IEnumerator caricaScena()
    {
        PlayerPrefs.SetInt("scoreLevel", 0);
        PlayerPrefs.SetInt("Level", 0);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
