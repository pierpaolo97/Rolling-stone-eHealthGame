using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class detectCollision : MonoBehaviour
{

    private Animation anim;
    public GameObject Player;

    [SerializeField]
    private float timeBetweenQuestion = 1.3f;

    public int tocco = 0;
    public Text timeText;
    public float timeStart = 0;
    public int scoreValue=0;
    public GameObject menuLevel;
    public Text menuScoreText;
    public Text menuLevelText;
    private Vector3 savePosCollision;


    private void Start()
    {
        scoreValue = this.GetComponent<Score>().score;
        Debug.LogWarning(PlayerPrefs.GetString("difficolta"));
    }

    public void OnCollisionEnter(Collision collision)
    {
        savePosCollision = collision.transform.position;
        if (collision.transform.name == "Plane" && tocco == 0) //serve per il primo tocco della pallina sul piano 
        {
            tocco = 1;  
            this.GetComponent<Accelerometer>().speed = 20f;
            this.GetComponent<fromKeyboard>().speed = 20f;

        }

        if (collision.transform.CompareTag("True"))
        {
            this.transform.position = collision.transform.position;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<SphereCollider>().enabled = false;
            scoreValue += 1;
            this.GetComponent<Score>().scoreText.text = "Score:" + scoreValue.ToString();
            this.GetComponent<Accelerometer>().speed = 0f;
            Player.GetComponent<Animator>().Play("correctAnswerAnimation 0");
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            checkLevel();
        }

        if (collision.transform.CompareTag("False"))
        {
            this.transform.position = collision.transform.position;
            this.GetComponent<Accelerometer>().speed = 0f;            
            Debug.Log("Hai sbagliato");
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
            timeText.text = "Time: " + Mathf.Round(timeStart).ToString();
        }
    }

    private void checkLevel()
    {
        int level = PlayerPrefs.GetInt("Level", 0);
        level++;
        PlayerPrefs.SetInt("Level", level);

        if (level == 2)
        {
            menuLevel.SetActive(true);
            menuLevelText.text = "LEVEL '" + PlayerPrefs.GetString("LetteraLivello", "C") + "' COMPLETED"; 
            menuScoreText.text = "SCORE: " + scoreValue.ToString() + "/10";
            this.GetComponent<Score>().scoreText.gameObject.SetActive(false);
            timeText.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(TransitionToNextQuestion());
        }
        

    }



}
