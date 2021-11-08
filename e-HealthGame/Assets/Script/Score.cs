using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    public Text scoreText;

    void Awake()
    {
        score = PlayerPrefs.GetInt("scoreLevel", 0);
        scoreText.text = "Score:" + score.ToString();
    }

}
