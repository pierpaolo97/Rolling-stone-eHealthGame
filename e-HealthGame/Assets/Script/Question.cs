using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Question
{
    public Texture2D texture;
    public string word;
    public string[] risposte;
    public string rispostaGiusta;
    public AudioClip audioWord;
}
