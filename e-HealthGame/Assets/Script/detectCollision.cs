using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.VFX;
using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class detectCollision : MonoBehaviour
{

    //public GameObject animCam;
    public GameObject Player;
    public VisualEffect Vfx;
    public GameObject risposte;
    [SerializeField]
    private float timeBetweenQuestion = 0.4f;
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

    AsyncOperation async;
    public GameObject portalAnimation;

    public string xmlTime;

    public List<float> X;
    public List<float> Y;
    public List<float> Z;

    public int risposto = 0;

    public GameObject risposta1;
    public GameObject risposta2;
    private Vector3 targetPosition;
    private GameObject AtomBall;


    private void Start()
    {
        AtomBall = Player.transform.GetChild(0).gameObject;
        StartCoroutine(WaitCamera());
        scoreValue = this.GetComponent<Score>().score;
        //Debug.LogWarning(PlayerPrefs.GetString("difficolta"));

        /*if (PlayerPrefs.GetInt("Level", -1) >0)
        {
            async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            async.allowSceneActivation = false;
        }*/

        risposta1 = risposte.transform.Find("R1").transform.Find("Risposta").gameObject;
        risposta2 = risposte.transform.Find("R2").transform.Find("Risposta").gameObject;

        if (risposta1.CompareTag("True"))
        {
            targetPosition = risposta1.transform.position;
        }
        else
        {
            targetPosition = risposta2.transform.position;
        }
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        savePosCollision = collision.transform.position;
        //Debug.Log(collision.transform.name);
        if ( (collision.transform.name == "Plane" || collision.transform.name == "DOMANDA") && tocco == 0) //serve per il primo tocco della pallina sul piano 
        {
            pause.SetActive(true);
            //Debug.Log(collision.transform.name);
            //Debug.Log("Toccato");           
            this.GetComponent<Accelerometer>().speed = 2000f;
            this.GetComponent<fromKeyboard>().speed = 500f;
            async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            async.allowSceneActivation = false;
            camera.GetComponent<cameraFollow>().enabled = true;
            camera.transform.rotation = Quaternion.Euler(40f, 0, 0);
            tocco = 1;
            if (collision.transform.name == "DOMANDA")
            {
                collision.collider.enabled = false;
                //Debug.Log("TOLTO");
            }

        }

        if (collision.transform.CompareTag("True"))
        {
            risposto++;
            //animCam.GetComponent<Animator>().Play("camera jump");
            camera.GetComponent<dissolve>().changeMaterial();
            rispostaEsatta(collision);
            prelevaInformazioni(collision, "CORRECT");
            checkLevel();
        }

        if (collision.transform.CompareTag("False"))
        {
            risposto++;
            camera.GetComponent<dissolve>().changeMaterial();
            rispostaSbagliata(collision);
            prelevaInformazioni(collision, "WRONG");
            checkLevel();
        }
    }

  
    IEnumerator TransitionToNextQuestion()
    {
        //portalAnimation.SetActive(true);
        PlayerPrefs.SetInt("scoreLevel", scoreValue);        
        yield return new WaitForSeconds(timeBetweenQuestion);
        /*if (PlayerPrefs.GetInt("Level", -1) > 1)
        {*/
            async.allowSceneActivation = true;
            /*Debug.Log("precaricato");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("carica ora");
        }*/
    }

    private void Update()
    {
        if (tocco > 0 && risposto==0)
        {
            timeStart += Time.deltaTime;
            timeText.text =  Mathf.Round(timeStart).ToString();
            X.Add(targetPosition.x - this.transform.position.x);
            Y.Add(targetPosition.y - this.transform.position.y);
            Z.Add(targetPosition.z - this.transform.position.z);
        }

        if (AtomBall.transform.position.y > 3)
        {
            Vfx.Stop();
        }
        else
        {
            Vfx.Play();
        }


    }



    private void checkLevel()
    {
        int level = PlayerPrefs.GetInt("Level", 0);
        level++;
        PlayerPrefs.SetInt("Level", level);

        if (level == 10)
        {
            StartCoroutine(attendiAnimazione());
            fineLivello();
        }
        else
        {
            StartCoroutine(TransitionToNextQuestion());         
        }
        PlayerPrefs.SetInt("scoreLevel", scoreValue);
        this.GetComponent<HighScore>().checkHighScore(PlayerPrefs.GetInt("scoreLevel"));

    }


    private void rispostaEsatta(Collision collision)
    {
        //Debug.Log(timeStart);
        xmlTime = Mathf.RoundToInt(timeStart).ToString();
        right.SetActive(true);
        this.transform.position = collision.transform.position;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<SphereCollider>().enabled = false;
        scoreAnswer = Mathf.RoundToInt(10 * 10 / timeStart);
        scoreValue += scoreAnswer;
        this.GetComponent<Score>().scoreText.text = scoreValue.ToString();
        this.GetComponent<Accelerometer>().speed = 0f;
        //Player.GetComponent<Animator>().Play("correctAnswerAnimation 0");
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    }

    private void rispostaSbagliata(Collision collision)
    {
        xmlTime = Mathf.RoundToInt(timeStart).ToString();
        wrong.SetActive(true);
        this.transform.position = collision.transform.position;
        this.GetComponent<Accelerometer>().speed = 0f;
    }

    private void fineLivello()
    {
        menuLevelText.text = PlayerPrefs.GetString("LetteraLivello", "C");
        menuScoreText.text = "SCORE: " + scoreValue.ToString();   
    }


    IEnumerator attendiAnimazione()
    {
        yield return new WaitForSeconds(1.5f);
        menuLevel.SetActive(true);
        menuLevel.GetComponent<Animator>().Play("bounce");
        carta.SetActive(false);
        cartasonoro.SetActive(false);
        right.SetActive(false);
        wrong.SetActive(false);
        scoreObject.SetActive(false);
        timeObject.SetActive(false);
        pause.SetActive(false);
    }

    IEnumerator WaitCamera()
    {
        yield return new WaitForSeconds(1f);
        AtomBall.GetComponent<Rigidbody>().useGravity = true;

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
        bool audio = camera.GetComponent<setQuestion>().audioObject.activeSelf;

        string xmlAudio = "";

        if (audio)
        {
            xmlAudio = "AUDIO";
        }
        else
        {
            xmlAudio = "PAROLA";
        }

        float[] arrayX = X.ToArray();
        float[] arrayY = Y.ToArray();
        float[] arrayZ = Z.ToArray();

        string X_string = string.Join(" ", arrayX);
        string Y_string = string.Join(" ", arrayY);
        string Z_string = string.Join(" ", arrayZ);


        #if UNITY_EDITOR
            if (File.Exists(Application.dataPath + "/list_XML.xml"))
            {
                    Debug.Log("Esiste");
                    camera.GetComponent<createXML>().addDataToXML(parola, rispostaData, esito, score, xmlAudio, xmlTime, X_string, Y_string, Z_string);
            }
            else
            {
                    Debug.Log("Non esiste");
                    camera.GetComponent<createXML>().newXML(parola, rispostaData, esito, score, xmlAudio, xmlTime, X_string, Y_string, Z_string);
            }
#endif

#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
            if (File.Exists(Application.persistentDataPath + "/list_XML.xml"))
            {
                camera.GetComponent<createXML>().addDataToXML(parola, rispostaData, esito, score, xmlAudio, xmlTime, X_string, Y_string, Z_string);
            }
            else
            {
                camera.GetComponent<createXML>().newXML(parola, rispostaData, esito, score, xmlAudio, xmlTime, X_string, Y_string, Z_string);
            }
#endif






    }





}
