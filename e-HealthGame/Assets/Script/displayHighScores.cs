using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayHighScores : MonoBehaviour
{

    public Text facileText;
    public Text medioText;
    public Text difficileText;

    public void C()
    {
        facileText.text = "Modalità facile: " + PlayerPrefs.GetInt("EASY_C", 0); 
        medioText.text = "Modalità medio: " + PlayerPrefs.GetInt("MEDIUM_C", 0);
        difficileText.text = "Modalità difficile: " + PlayerPrefs.GetInt("HARD_C", 0);
    }

    public void G()
    {
        facileText.text = "Modalità facile: " + PlayerPrefs.GetInt("EASY_G", 0);
        medioText.text = "Modalità medio: " + PlayerPrefs.GetInt("MEDIUM_G", 0);
        difficileText.text = "Modalità difficile: " + PlayerPrefs.GetInt("HARD_G", 0);
    }

    public void SC()
    {
        facileText.text = "Modalità facile: " + PlayerPrefs.GetInt("EASY_SC", 0);
        medioText.text = "Modalità media: " + PlayerPrefs.GetInt("MEDIUM_SC", 0);
        difficileText.text = "Modalità difficile: " + PlayerPrefs.GetInt("HARD_SC", 0);
    }
}
