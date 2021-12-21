using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayHighScores : MonoBehaviour
{

    public Text facileText;
    public Text medioText;
    public Text difficileText;
    public Text lettera;

    private void Start()
    {
        switch (PlayerPrefs.GetString("LetteraLivello", "C"))
        {
            case "C":
                C();
                break;
            case "G":
                G();
                break;
            case "SC":
                SC();
                break;
        }
    }



    public void C()
    {
        facileText.text = "Difficoltà facile: " + PlayerPrefs.GetInt("EASY_C", 0); 
        medioText.text = "Difficoltà media: " + PlayerPrefs.GetInt("MEDIUM_C", 0);
        difficileText.text = "Difficoltà difficile: " + PlayerPrefs.GetInt("HARD_C", 0);
        lettera.text = "C";
    }

    public void G()
    {
        facileText.text = "Difficoltà facile: " + PlayerPrefs.GetInt("EASY_G", 0);
        medioText.text = "Difficoltà media: " + PlayerPrefs.GetInt("MEDIUM_G", 0);
        difficileText.text = "Difficoltà difficile: " + PlayerPrefs.GetInt("HARD_G", 0);
        lettera.text = "G";
    }

    public void SC()
    {
        facileText.text = "Difficoltà facile: " + PlayerPrefs.GetInt("EASY_SC", 0);
        medioText.text = "Difficoltà media: " + PlayerPrefs.GetInt("MEDIUM_SC", 0);
        difficileText.text = "Difficoltà difficile: " + PlayerPrefs.GetInt("HARD_SC", 0);
        lettera.text = "SC";
    }
}
