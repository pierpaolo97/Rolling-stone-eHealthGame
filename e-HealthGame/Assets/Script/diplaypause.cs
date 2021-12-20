using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class diplaypause : MonoBehaviour
{
    public Text menuPauseText;
    string word = "";

    private void Start()
    {
        string diff = PlayerPrefs.GetString("difficolta", "Easy");
        switch (diff)
        {
            case "Easy":
                word = "Facile";
                break;

            case "Medium":
                word = "Media";
                break;

            case "Hard":
                word = "Difficile";
                break;
        }
        menuPauseText.text = "Stai giocando la lettera " + PlayerPrefs.GetString("LetteraLivello","C") +"\n" + " con difficoltà " + word;
    }

}
