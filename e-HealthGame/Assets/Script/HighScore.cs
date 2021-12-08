using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public void checkHighScore(int score)
    {

        string diff = PlayerPrefs.GetString("difficolta", "Easy");
        string lettera = PlayerPrefs.GetString("LetteraLivello", "C");

        //Debug.Log(diff);
        //Debug.Log(lettera);

        switch (diff)
        {
            case "Easy":
                switch (lettera)
                {
                    case "C":
                        if (score > PlayerPrefs.GetInt("EASY_C", -1))
                        {
                            PlayerPrefs.SetInt("EASY_C", score);
                        }
                        break;
                    case "G":
                        if (score > PlayerPrefs.GetInt("EASY_G", -1))
                        {
                            PlayerPrefs.SetInt("EASY_G", score);
                        }
                        break;
                    case "SC":
                        if (score > PlayerPrefs.GetInt("EASY_SC", -1))
                        {
                            PlayerPrefs.SetInt("EASY_SC", score);
                        }
                        break;
                }
                break;
            case "Medium":
                switch (lettera)
                {
                    case "C":
                        if (score > PlayerPrefs.GetInt("MEDIUM_C", -1))
                        {
                            PlayerPrefs.SetInt("MEDIUM_C", score);
                        }
                        break;
                    case "G":
                        if (score > PlayerPrefs.GetInt("MEDIUM_G", -1))
                        {
                            PlayerPrefs.SetInt("MEDIUM_G", score);
                        }
                        break;
                    case "SC":
                        if (score > PlayerPrefs.GetInt("MEDIUM_SC", -1))
                        {
                            PlayerPrefs.SetInt("MEDIUM_SC", score);
                        }
                        break;
                }
                break;
            case "Hard":
                switch (lettera)
                {
                    case "C":
                        if (score > PlayerPrefs.GetInt("HARD_C", -1))
                        {
                            PlayerPrefs.SetInt("HARD_C", score);
                        }
                        break;
                    case "G":
                        if (score > PlayerPrefs.GetInt("HARD_G", -1))
                        {
                            PlayerPrefs.SetInt("HARD_G", score);
                        }
                        break;
                    case "SC":
                        if (score > PlayerPrefs.GetInt("HARD_SC", -1))
                        {
                            PlayerPrefs.SetInt("HARD_SC", score);
                        }
                        break;
                }
                break;
        }

        /*Debug.Log(PlayerPrefs.GetInt("EASY_C", -1));
        Debug.Log(PlayerPrefs.GetInt("EASY_G", -1));
        Debug.Log(PlayerPrefs.GetInt("EASY_SC", -1));

        Debug.Log(PlayerPrefs.GetInt("MEDIUM_C", -1));
        Debug.Log(PlayerPrefs.GetInt("MEDIUM_G", -1));
        Debug.Log(PlayerPrefs.GetInt("MEDIUM_SC", -1));

        Debug.Log(PlayerPrefs.GetInt("HARD_C", -1));
        Debug.Log(PlayerPrefs.GetInt("HARD_G", -1));
        Debug.Log(PlayerPrefs.GetInt("HARD_SC", -1));*/





    }
}
