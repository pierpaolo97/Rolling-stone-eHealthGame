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
    public Question[] questionsG;
    public Question[] questionsSC;
    public static List<Question> unansweredQuestions;
    public Question currentQuestion;
    public GameObject domanda;

    private Renderer rend;
    private TextMeshPro textParola;
    public GameObject risposte;
    public GameObject carta;
    public GameObject cartasonoro;

    private TextMeshPro r1;
    private TextMeshPro r2;
    private Text textCarta;
    private RawImage photoCarta;

    public GameObject audioObject;
    public AudioSource audioSource;
    public Texture2D audioTexture;
    public GameObject transition;

    public List<GameObject> mondi;
    public GameObject currentWorld;
    

    void Awake()
    {

        if (PlayerPrefs.GetInt("Level", 0)== 0)
        {
            transition.SetActive(true);
        }
       
        setCurrentWorld();
        //Debug.Log(PlayerPrefs.GetString("LetteraLivello"));
        rend = domanda.transform.Find("Immagine").GetComponent<MeshRenderer>();
        textParola = domanda.transform.Find("Parola").GetComponent<TextMeshPro>();
        textCarta = carta.transform.Find("TextImg").GetComponent<Text>();
        photoCarta = carta.transform.Find("PhotoImg").GetComponent<RawImage>();

        r1 = risposte.transform.Find("R1").transform.Find("textR1").GetComponent<TextMeshPro>();
        r2 = risposte.transform.Find("R2").transform.Find("textR2").GetComponent<TextMeshPro>();

       
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            caricaDomande();
        }
        
        SetCurrentQuestion();
    }

    public void caricaDomande()
    {
        switch (PlayerPrefs.GetString("LetteraLivello", "C"))
        {
            case "C":
                unansweredQuestions = questionsC.ToList<Question>();
                break;
            case "G":
                unansweredQuestions = questionsG.ToList<Question>();
                break;
            case "SC":
                unansweredQuestions = questionsSC.ToList<Question>();
                break;
        }
        //Debug.Log("dall'altro codice");
    }

    void setCurrentWorld()
    {
        int z = Random.Range(0, mondi.Count);
        //Debug.Log(z);
        if (z == PlayerPrefs.GetInt("CurrentWorld", -1))
        {
            setCurrentWorld();
        }
        else
        {
            PlayerPrefs.SetInt("CurrentWorld", z);
            currentWorld = mondi[z];
            currentWorld.SetActive(true);
        }
        
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
                //Debug.LogWarning("Easy");
                if (p >= 10)
                {
                    parola();
                }
                else
                {
                    audioParola();
                }
                break;

            case "Medium":
                //Debug.LogWarning("Medium");
                if (p >= 30)
                {
                    parola();
                }
                else
                {
                    audioParola();
                }
                break;

            case "Hard":
                //Debug.LogWarning("Hard");
                if (p >= 50)
                {
                    parola();
                }
                else
                {
                    audioParola();
                }
                break;
        }
    }

    IEnumerator playAudio()
    {
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(currentQuestion.audioWord);
    }

    private void parola()
    {
        carta.SetActive(true);
        rend.material.mainTexture = currentQuestion.texture;
        textParola.text = currentQuestion.word;
        textCarta.text = currentQuestion.word;
        photoCarta.texture = currentQuestion.texture;
    }

    private void audioParola()
    {
        cartasonoro.SetActive(true);
        rend.material.mainTexture = audioTexture;
        textParola.text = "";
        audioObject.SetActive(true);
        StartCoroutine(playAudio());
    }

}
