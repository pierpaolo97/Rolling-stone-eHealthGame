using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;



public class setQuestion : MonoBehaviour
{
    public Question[] questionsC;
    public Question[] questionsS;
    private static List<Question> unansweredQuestions;
    private Question currentQuestion;
    public GameObject domanda;

    private Renderer rend;
    private TextMeshPro textParola;
    public GameObject risposte;

    private TextMeshPro r1;
    private TextMeshPro r2;
    public GameObject audioObject;
    public AudioSource audioSource;
    public Texture2D audioTexture;

    void Start()
    {
        rend = domanda.transform.Find("Immagine").GetComponent<MeshRenderer>();
        textParola = domanda.transform.Find("Parola").GetComponent<TextMeshPro>();

        r1 = risposte.transform.Find("R1").transform.Find("textR1").GetComponent<TextMeshPro>();
        r2 = risposte.transform.Find("R2").transform.Find("textR2").GetComponent<TextMeshPro>();

        if (unansweredQuestions == null || unansweredQuestions.Count==0)
        {
            switch (PlayerPrefs.GetString("LetteraLivello", "C"))
            {
                case "C":
                    unansweredQuestions = questionsC.ToList<Question>();
                    break;
                case "S":
                    unansweredQuestions = questionsS.ToList<Question>();
                    break;
            }
        }

        SetCurrentQuestion();            
    }

    void SetCurrentQuestion()
    {
        int x = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[x];
        unansweredQuestions.RemoveAt(x);
        
        int y = Random.Range(0, 2);
        switch (y)
        {
            case 0:
                r1.text = currentQuestion.risposte[0];
                r2.text = currentQuestion.risposte[1];
                break;
            case 1:
                r1.text = currentQuestion.risposte[1];
                r2.text = currentQuestion.risposte[0];
                break;
        }

        if (string.Equals(r1.text, currentQuestion.rispostaGiusta))
        {
            risposte.transform.Find("R1").transform.Find("Risposta").tag = "True";
        }
        if (string.Equals(r2.text, currentQuestion.rispostaGiusta))
        {
            risposte.transform.Find("R2").transform.Find("Risposta").tag = "True";
        }

        string diff = PlayerPrefs.GetString("difficolta", "Easy");
        int p = Random.Range(0, 100);

        switch (diff)
        {
            case "Easy":
                Debug.LogWarning("Easy");
                if (p >= 10)
                {
                    rend.material.mainTexture = currentQuestion.texture;
                    textParola.text = currentQuestion.word;
                }
                else
                {
                    rend.material.mainTexture = audioTexture;
                    textParola.text = "";
                    audioObject.SetActive(true);
                    StartCoroutine(playAudio());
                }
                break;

            case "Medium":
                Debug.LogWarning("Medium");
                if (p >= 30)
                {
                    rend.material.mainTexture = currentQuestion.texture;
                    textParola.text = currentQuestion.word;
                }
                else
                {
                    rend.material.mainTexture = audioTexture;
                    textParola.text = "";
                    audioObject.SetActive(true);
                    StartCoroutine(playAudio());
                }
                break;

            case "Hard":
                Debug.LogWarning("Hard");
                if (p >= 50)
                {
                    rend.material.mainTexture = currentQuestion.texture;
                    textParola.text = currentQuestion.word;
                }
                else
                {
                    rend.material.mainTexture = audioTexture;
                    textParola.text = "";
                    audioObject.SetActive(true);
                    StartCoroutine(playAudio());
                }
                break;
        }
    }

    IEnumerator playAudio()
    {
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(currentQuestion.audioWord);      
    }



}
