using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class diplaypause : MonoBehaviour
{
    public Text menuPauseText;

    private void Start()
    {
        menuPauseText.text = "Stai giocando la lettera " + PlayerPrefs.GetString("LetteraLivello","C") +"\n" + " in modalità " + PlayerPrefs.GetString("difficolta","Easy");
    }

}
