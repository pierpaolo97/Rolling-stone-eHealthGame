using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class detectCollision : MonoBehaviour
{

    private Animation anim;
    public GameObject Player;

    [SerializeField]
    private float timeBetweenQuestion = 1.3f;

    public int tocco = 0;
    public Text timeText;
    public float timeStart = 0;
    public int scoreValue = 0;
    public GameObject menuLevel;
    public Text menuScoreText;
    public Text menuLevelText;
    public GameObject carta;
    public GameObject cartasonoro;
    public GameObject right;
    public GameObject wrong;
    public GameObject pause;
    public int scoreAnswer = 0;

    public GameObject scoreObject;
    public GameObject timeObject;

    private Vector3 savePosCollision;
    public GameObject camera;
    public string rispostaData;

    private void Start()
    {
        scoreValue = this.GetComponent<Score>().score;
        Debug.LogWarning(PlayerPrefs.GetString("difficolta"));
    }

    public void OnCollisionEnter(Collision collision)
    {
        savePosCollision = collision.transform.position;
        if ( (collision.transform.name == "Plane" || collision.transform.name == "DOMANDA") && tocco == 0) //serve per il primo tocco della pallina sul piano 
        {
            Debug.Log("Toccato");
            tocco = 1;
            this.GetComponent<Accelerometer>().speed = 20f;
            this.GetComponent<fromKeyboard>().speed = 20f;
        }

        if (collision.transform.CompareTag("True"))
        {          
            rispostaEsatta(collision);
            prelevaInformazioni(collision, "CORRECT");
            checkLevel();
        }

        if (collision.transform.CompareTag("False"))
        {            
            rispostaSbagliata(collision);
            prelevaInformazioni(collision, "WRONG");
            checkLevel();
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
            timeText.text =  Mathf.Round(timeStart).ToString();
        }
    }

    private void checkLevel()
    {
        int level = PlayerPrefs.GetInt("Level", 0);
        level++;
        PlayerPrefs.SetInt("Level", level);

        if (level == 2)
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
        menuLevel.SetActive(true);
        menuLevelText.text = PlayerPrefs.GetString("LetteraLivello", "C");
        menuScoreText.text = "SCORE: " + scoreValue.ToString();   
    }


    IEnumerator attendiAnimazione()
    {
        yield return new WaitForSeconds(1f);
        menuLevel.GetComponent<Animator>().Play("bounce 0");
        carta.SetActive(false);
        cartasonoro.SetActive(false);
        right.SetActive(false);
        wrong.SetActive(false);
        scoreObject.SetActive(false);
        timeObject.SetActive(false);
        pause.SetActive(false);
    }


    public void prelevaInformazioni(Collision collision, string esito)
    {

        GameObject father = collision.gameObject.transform.parent.gameObject;
        if (father.name == "R1")
        {
            rispostaData = father.transform.Find("textR1").GetComponent<TextMeshPro>().text;
        }
        if (father.name == "R2")
        {
            rispostaData = father.transform.Find("textR2").GetComponent<TextMeshPro>().text;
        }

        string parola = camera.GetComponent<setQuestion>().currentQuestion.word;
        string score = scoreAnswer.ToString();

        #if UNITY_EDITOR
            if (File.Exists(Application.dataPath + "/list_XML.xml"))
            {
                    Debug.Log("Esiste");
                    camera.GetComponent<createXML>().addDataToXML(parola, rispostaData, esito, score);
            }
            else
            {
                    Debug.Log("Non esiste");
                    camera.GetComponent<createXML>().newXML(parola, rispostaData, esito, score);
            }
        #endif

        #if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
            if (File.Exists(Application.persistentDataPath + "/list_XML.xml"))
            {
                camera.GetComponent<createXML>().addDataToXML(parola, rispostaData, esito, score);
            }
            else
            {
                camera.GetComponent<createXML>().newXML(parola, rispostaData, esito, score);
            }
        #endif






    }





}
