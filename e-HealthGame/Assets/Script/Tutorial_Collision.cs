using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
//using System.Numerics;
//using System.Diagnostics;

public class Tutorial_Collision : MonoBehaviour
{
    public Animator BirdAnimator;
    private GameObject Tree;
    public GameObject Player;
    private bool boolcheck = true;
    private bool boolcheck2 = true;
    private bool boolcheck3 = true;
    private bool boolcheck4;

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
    public GameObject pause;

    public GameObject scoreObject;
    public GameObject timeObject;

    private Vector3 savePosCollision;
    public GameObject camera;

    public GameObject TextComic;
    public GameObject phone;

    public GameObject domanda;
    public GameObject menuTutorial;
    private Renderer rend;
    private TextMeshPro textParola;
    public Texture2D audioTexture;
    public AudioClip audioParola;
    public AudioSource audioSource;
    public GameObject audioLivello;
    public GameObject QuestionArrow;
    public GameObject Bird;
    public GameObject TimeScoreArrow;
    public GameObject PortalsArrow;

    public Button play0;
    public Button play1;
    public Button play2;
    public Button play3;

    public float delay = 0.1f;
    public string benvenutoText = "";
    public string benvenutoText2 = "";
    public string primoText = "";
    public string secondoText = "";
    public string terzoText = "";
    public string quartoText = "";
    public string quintoText = "";
    public string RightText = "";
    public string WrongText = " ";
    public string TryAgain = "";
    public string currentText = "";
    public GameObject risposta1;
    public GameObject risposta2;

    void Awake()
    {
        Tree = GameObject.Find("Tree");
    }

    private void Start()
    {
        TextComic.SetActive(true);
        scoreValue = this.GetComponent<Score>().score;
        Debug.LogWarning(PlayerPrefs.GetString("difficolta"));
        textParola = domanda.transform.Find("Parola").GetComponent<TextMeshPro>();
        rend = domanda.transform.Find("Immagine").GetComponent<MeshRenderer>();
        //BirdAnimator = Bird.GetComponent<Animator>();

        int level = PlayerPrefs.GetInt("Level", 0);
        if (level == 1 )
        {
            StartCoroutine(ShowText(TryAgain));
            Bird.SetActive(true);
            risposta1.transform.position = new Vector3(41f, 0.48f, -20f);
            risposta1.transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        if (level == 3)
        {
            StartCoroutine(ShowText(TryAgain));
            Bird.SetActive(true);
            risposta2.transform.position = new Vector3(41f, 0.48f, -18.25f);
        }

        if (level > 1)
        {
            audioLivello.SetActive(true);
            cartasonoro.SetActive(true);
            rend.material.mainTexture = audioTexture;
            textParola.text = "";
            StartCoroutine(playAudio());
        }

        if (level == 2)
        {
            StartCoroutine(ShowText(quintoText));
            Bird.SetActive(true);
        }
    }

    private void Update()
    {
        if (tocco > 0)
        {
            timeStart += Time.deltaTime;
            timeText.text = Mathf.Round(timeStart).ToString();
        }

        if (BirdAnimator.GetCurrentAnimatorStateInfo(0).IsName("GoArrow")|| BirdAnimator.GetCurrentAnimatorStateInfo(0).IsName("GotoScoreTime") || BirdAnimator.GetCurrentAnimatorStateInfo(0).IsName("GotoPortals") || BirdAnimator.GetCurrentAnimatorStateInfo(0).IsName("GotoTree"))
        {
            TextComic.SetActive(false);
            play1.interactable = false;
            play2.interactable = false;
            play3.interactable = false;
        }

        CheckCaroutine();
        CheckFlip();
    }

    private void FixedUpdate()
    {
        CheckTree();
    }


    public void OnCollisionEnter(Collision collision)
    {
        savePosCollision = collision.transform.position;
        if ((collision.transform.name == "Plane" || collision.transform.name == "DOMANDA") && tocco == 0) //serve per il primo tocco della pallina sul piano 
        {
            if (collision.transform.name == "DOMANDA")
            {
                collision.collider.enabled = false;
                //Debug.Log("TOLTO");
            }
            pause.SetActive(true);
            int level = PlayerPrefs.GetInt("Level", 0);
            if (level == 0) {
                TextComic.SetActive(true);
                StartCoroutine(ShowTextbenvenuto(benvenutoText));
                StartCoroutine(ShowTextbenvenuto2(benvenutoText2));
                Bird.SetActive(true);
                play0.gameObject.SetActive(true);
            }
            else
            {
                TextComic.SetActive(true);
                muoviPallina();
            }

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

    public void okStartTutorial()
    {
        Bird.GetComponent<Animator>().Play("GoArrow");
        QuestionArrow.SetActive(true);
        play0.gameObject.SetActive(false);
        play1.gameObject.SetActive(true);
    }

    public void okCarta()
    {
        TextComic.SetActive(false);
        Bird.GetComponent<Animator>().Play("GotoScoreTime");
        QuestionArrow.SetActive(false);
        TimeScoreArrow.SetActive(true);
        play1.gameObject.SetActive(false);
        play2.gameObject.SetActive(true);
    }

    public void okTimeScore()
    {
        TextComic.SetActive(false);
        Bird.GetComponent<Animator>().Play("GotoPortals");
        TimeScoreArrow.SetActive(false);
        PortalsArrow.SetActive(true);
        play2.gameObject.SetActive(false);
        play3.gameObject.SetActive(true);
    }

    public void GoTree ()
    {
        TextComic.SetActive(false);
        Bird.GetComponent<Animator>().Play("GotoTree");
        boolcheck4 = true;
        PortalsArrow.SetActive(false);
        play3.gameObject.SetActive(false);
    }

    public void muoviPallina()
    {
        this.GetComponent<Accelerometer>().speed = 2000f;
        this.GetComponent<fromKeyboard>().speed = 500f;
    }


    IEnumerator ShowText(string textDaScrivere)
    {
        for (int i = 0; i < textDaScrivere.Length; i++)
        {
            Bird.GetComponent<Animator>().Play("IdleSpeak");
            currentText = textDaScrivere.Substring(0, i);
            //Debug.Log(Bird.transform.GetChild(0).transform.GetChild(1).name);
            TextComic.transform.GetChild(0).GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        Bird.GetComponent<Animator>().Play("idleMute");
    }

    IEnumerator ShowTextbenvenuto(string textDaScrivere)
    {
        play0.interactable = false;
        play1.interactable = false;
        play2.interactable = false;
        for (int i = 0; i < textDaScrivere.Length; i++)
        {
            Bird.GetComponent<Animator>().Play("IdleSpeak");
            currentText = textDaScrivere.Substring(0, i);
            //Debug.Log(Bird.transform.GetChild(0).transform.GetChild(1).name);
            TextComic.transform.GetChild(0).GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        Bird.GetComponent<Animator>().Play("idleMute");
        play0.interactable = false;

    }

    IEnumerator ShowTextbenvenuto2(string textDaScrivere)
    {
        yield return new WaitForSeconds(9);
        for (int i = 0; i < textDaScrivere.Length; i++)
        {
            Bird.GetComponent<Animator>().Play("IdleSpeak");
            currentText = textDaScrivere.Substring(0, i);
            //Debug.Log(Bird.transform.GetChild(0).transform.GetChild(1).name);
            TextComic.transform.GetChild(0).GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        Bird.GetComponent<Animator>().Play("idleMute");
        play0.interactable = true;

    }

    IEnumerator ShowTextCards(string textDaScrivere)
    {
        for (int i = 0; i < textDaScrivere.Length; i++)
        {
            Bird.GetComponent<Animator>().Play("FlySpeak");
            currentText = textDaScrivere.Substring(0, i);
            //Debug.Log(Bird.transform.GetChild(0).transform.GetChild(1).name);
            TextComic.transform.GetChild(0).GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        Bird.GetComponent<Animator>().Play("FlyIdle");
        play1.interactable = true;
    }

    IEnumerator ShowTextTimeScore(string textDaScrivere)
    {
        for (int i = 0; i < textDaScrivere.Length; i++)
        {
            Bird.GetComponent<Animator>().Play("FlySpeak2");
            currentText = textDaScrivere.Substring(0, i);
            //Debug.Log(Bird.transform.GetChild(0).transform.GetChild(1).name);
            TextComic.transform.GetChild(0).GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        Bird.GetComponent<Animator>().Play("FlyIdle2");
        play2.interactable = true;
    }

    IEnumerator ShowTextPortals(string textDaScrivere)
    {
        play3.interactable = false;
        for (int i = 0; i < textDaScrivere.Length; i++)
        {
            Bird.GetComponent<Animator>().Play("FlySpeak3");
            currentText = textDaScrivere.Substring(0, i);
            //Debug.Log(Bird.transform.GetChild(0).transform.GetChild(1).name);
            TextComic.transform.GetChild(0).GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        Bird.GetComponent<Animator>().Play("FlyIdle3");
        play3.interactable = true;
    }

    IEnumerator ShowTextBackToTree(string textDaScrivere)
    {
        for (int i = 0; i < textDaScrivere.Length; i++)
        {
            Bird.GetComponent<Animator>().Play("IdleSpeak");
            currentText = textDaScrivere.Substring(0, i);
            //Debug.Log(Bird.transform.GetChild(0).transform.GetChild(1).name);
            TextComic.transform.GetChild(0).GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        Bird.GetComponent<Animator>().Play("idleMute");
        muoviPallina();
        phone.SetActive(true);

    }

    IEnumerator TransitionToNextQuestion()
    {
        PlayerPrefs.SetInt("scoreLevel", scoreValue);
        yield return new WaitForSeconds(timeBetweenQuestion);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        //Debug.Log(timeStart);
        StartCoroutine(ShowText(RightText));
        Bird.SetActive(true);
        right.SetActive(true);
        this.transform.position = collision.transform.position;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<SphereCollider>().enabled = false;
        scoreAnswer = Mathf.RoundToInt(10 * 5 / timeStart);
        scoreValue += scoreAnswer;
        this.GetComponent<Score>().scoreText.text = scoreValue.ToString();
        this.GetComponent<Accelerometer>().speed = 0f;
        //Player.GetComponent<Animator>().Play("correctAnswerAnimation 0");
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    }

    private void rispostaSbagliata(Collision collision)
    {
        wrong.SetActive(true);
        StartCoroutine(ShowText(WrongText));
        Bird.SetActive(true);
        this.transform.position = collision.transform.position;
        this.GetComponent<Accelerometer>().speed = 0f;
    }

    private void fineLivello()
    {

        //tutorialLevelText.text = PlayerPrefs.GetString("LetteraLivello", "C");
        //tutorialScoreText.text = "SCORE: " + scoreValue.ToString();
    }

    IEnumerator attendiAnimazione()
    {
        yield return new WaitForSeconds(3.7f);
        menuTutorial.SetActive(true);
        menuTutorial.GetComponent<Animator>().Play("bounce");
        carta.SetActive(false);
        Tree.SetActive(false);
        Bird.SetActive(false);
        cartasonoro.SetActive(false);
        right.SetActive(false);
        wrong.SetActive(false);
        scoreObject.SetActive(false);
        timeObject.SetActive(false);
        pause.SetActive(false);
    }

    IEnumerator playAudio()
    {
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(audioParola);
    }


    /// CHECK BIRD

    public void CheckTree()
    {
        if (!BirdAnimator.GetCurrentAnimatorStateInfo(0).IsName("GotoTree") && boolcheck4 == true)
        {
            Bird.transform.localRotation = Quaternion.Euler(0, 0, 0);
            TextComic.SetActive(true);
            StartCoroutine(ShowTextBackToTree(quartoText));
            boolcheck4 = false;
        }
    }

    public void CheckFlip()
    {
        if (BirdAnimator.GetCurrentAnimatorStateInfo(0).IsName("GotoScoreTime") && !TextComic.activeInHierarchy)
        {
            Bird.transform.localRotation = Quaternion.Euler(0, 180, 0);
            TextComic.transform.localRotation = Quaternion.Euler(180, 0, 0);
            TextComic.transform.GetChild(0).localRotation = Quaternion.Euler(180, 180, 0);
            //TextComic.transform.Translate(0, 280, 0);
            TextComic.GetComponent<RectTransform>().localPosition = new Vector3(-200, -100 , 0);
        }
        if (BirdAnimator.GetCurrentAnimatorStateInfo(0).IsName("GotoPortals") && !TextComic.activeInHierarchy)
        {
            Bird.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //TextComic.transform.localRotation = Quaternion.Euler(0, 0, 0);
            TextComic.transform.GetChild(0).localRotation = Quaternion.Euler(180,0, 0);
            TextComic.transform.localRotation = Quaternion.Euler(180, 0, 0);
        }
        if (BirdAnimator.GetCurrentAnimatorStateInfo(0).IsName("GotoTree") && !TextComic.activeSelf)
        {
            Bird.transform.localRotation = Quaternion.Euler(0, 180, 0);
            //TextComic.transform.localRotation = Quaternion.Euler(180, 180, 0);
            TextComic.transform.localRotation = Quaternion.Euler(0,0, 0);
            TextComic.transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, 0);
            //TextComic.transform.Translate(0, 2.6f, 0);
            TextComic.GetComponent<RectTransform>().localPosition = new Vector3(-250, 80, 0);
        }
    }

    public void CheckCaroutine()
    {
        if (BirdAnimator.GetCurrentAnimatorStateInfo(0).IsName("FlySpeak") && boolcheck == true)
        {
            TextComic.SetActive(true);
            StartCoroutine(ShowTextCards(primoText));
            boolcheck = false;
        }

        if (BirdAnimator.GetCurrentAnimatorStateInfo(0).IsName("FlySpeak2") && boolcheck2 == true)
        {
            TextComic.SetActive(true);
            StartCoroutine(ShowTextTimeScore(secondoText));
            boolcheck2 = false;
        }

        if (BirdAnimator.GetCurrentAnimatorStateInfo(0).IsName("FlySpeak3") && boolcheck3 == true)
        {
            TextComic.SetActive(true);
            StartCoroutine(ShowTextPortals(terzoText));
            boolcheck3 = false;
        }
    }

}
