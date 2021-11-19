using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Tutorial_Collision : MonoBehaviour
{
    private Animation anim;
    public GameObject Player;

    [SerializeField]
    private float timeBetweenQuestion = 1.3f;

    public int tocco = 0;
    public Text timeText;
    public float timeStart = 0;
    public int scoreValue = 0;
    
    public GameObject carta;
    public GameObject cartasonoro;
    public GameObject right;
    public GameObject wrong;
    public int scoreAnswer = 0;

    public GameObject scoreObject;
    public GameObject timeObject;

    private Vector3 savePosCollision;
    public GameObject camera;

    public GameObject domanda;
    public GameObject menuTutorial;
    public Text tutorialScoreText;
    public Text tutorialLevelText;
    private Renderer rend;
    private TextMeshPro textParola;
    public Texture2D audioTexture;
    public AudioClip audioParola;
    public AudioSource audioSource;
    public GameObject audioLivello;


    public GameObject cartaCerchio;
    public GameObject domandaCerchio;
    public GameObject testoCerchio;
    public GameObject timeScoreCerchio;

    public GameObject risposteCerchio;

    public GameObject play1;
    public GameObject play2;
    public GameObject play3;


    public float delay = 0.1f;
    public string primoText = "ciao amici topolini ";
    public string secondoText = "ciao amici gattini ";
    public string terzoText = "ciao fratm ";
    private string currentText = "";

    private void Start()
    {
        scoreValue = this.GetComponent<Score>().score;
        Debug.LogWarning(PlayerPrefs.GetString("difficolta"));
        textParola = domanda.transform.Find("Parola").GetComponent<TextMeshPro>();
        rend = domanda.transform.Find("Immagine").GetComponent<MeshRenderer>();
        



        int level = PlayerPrefs.GetInt("Level", 0);
        
        if (level > 1)
        {
            audioLivello.SetActive(true);
            cartasonoro.SetActive(true);
            rend.material.mainTexture = audioTexture;
            textParola.text = "";
            StartCoroutine(playAudio());
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        savePosCollision = collision.transform.position;
        if ((collision.transform.name == "Plane" || collision.transform.name == "DOMANDA") && tocco == 0) //serve per il primo tocco della pallina sul piano 
        {
            cartaCerchio.SetActive(true);          
            domandaCerchio.SetActive(true);
            testoCerchio.SetActive(true);

            StartCoroutine(ShowText(primoText));


            Debug.Log("Toccato");
            tocco = 1;
            /*this.GetComponent<Accelerometer>().speed = 20f;
            this.GetComponent<fromKeyboard>().speed = 20f;*/
        }

        if (collision.transform.CompareTag("True"))
        {
            rispostaEsatta(collision);
            checkLevel();
        }

        if (collision.transform.CompareTag("False"))
        {
            rispostaSbagliata(collision);
            checkLevel();
        }
    }

    public void okCarta()
    {
        StartCoroutine(ShowText(secondoText));
        cartaCerchio.SetActive(false);
        domandaCerchio.SetActive(false);
        timeScoreCerchio.SetActive(true);
        play1.SetActive(false);
        
    }

    public void okTimeScore()
    {
        StartCoroutine(ShowText(terzoText));
        timeScoreCerchio.SetActive(false);
        risposteCerchio.SetActive(true);
        play2.SetActive(false);
        //cambia testo
        
    }

    public void muoviPallina()
    {
        risposteCerchio.SetActive(false);

        //cambia testo
        this.GetComponent<Accelerometer>().speed = 20f;
        this.GetComponent<fromKeyboard>().speed = 20f;
    }

    IEnumerator ShowText(string textDaScrivere)
    {
        for (int i = 0; i < textDaScrivere.Length; i++)
        {
            currentText = textDaScrivere.Substring(0, i);
            Debug.Log(testoCerchio.transform.GetChild(0).transform.GetChild(1).name);
            testoCerchio.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }




    IEnumerator TransitionToNextQuestion()
    {
        PlayerPrefs.SetInt("scoreLevel", scoreValue);
        yield return new WaitForSeconds(timeBetweenQuestion);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (tocco > 0)
        {
            timeStart += Time.deltaTime;
            timeText.text = Mathf.Round(timeStart).ToString();
        }
    }

    private void checkLevel()
    {
        int level = PlayerPrefs.GetInt("Level", 0);
        level++;
        PlayerPrefs.SetInt("Level", level);

        if (level == 4)
        {
            fineLivello();
            StartCoroutine(attendiAnimazione());
        }
        else
        {
            StartCoroutine(TransitionToNextQuestion());
        }
    }


    private void rispostaEsatta(Collision collision)
    {
        Debug.Log(timeStart);
        right.SetActive(true);
        this.transform.position = collision.transform.position;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<SphereCollider>().enabled = false;
        scoreAnswer = Mathf.RoundToInt(10 * 5 / timeStart);
        scoreValue += scoreAnswer;
        this.GetComponent<Score>().scoreText.text = scoreValue.ToString();
        this.GetComponent<Accelerometer>().speed = 0f;
        Player.GetComponent<Animator>().Play("correctAnswerAnimation 0");
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    }

    private void rispostaSbagliata(Collision collision)
    {
        wrong.SetActive(true);
        this.transform.position = collision.transform.position;
        this.GetComponent<Accelerometer>().speed = 0f;
    }

    private void fineLivello()
    {
        menuTutorial.SetActive(true);
        tutorialLevelText.text = PlayerPrefs.GetString("LetteraLivello", "C");
        tutorialScoreText.text = "SCORE: " + scoreValue.ToString();
    }


    IEnumerator attendiAnimazione()
    {
        yield return new WaitForSeconds(1f);
        menuTutorial.GetComponent<Animator>().Play("bounce 0");
        carta.SetActive(false);
        cartasonoro.SetActive(false);
        right.SetActive(false);
        wrong.SetActive(false);
        scoreObject.SetActive(false);
        timeObject.SetActive(false);
    }

    IEnumerator playAudio()
    {
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(audioParola);
    }




}
