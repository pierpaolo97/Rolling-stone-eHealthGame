using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakBird : MonoBehaviour
{
    public string TextStart;
    private Animator BirdAnimator;
    public GameObject Bird;
    private string currentText = "";
    private bool check_text;
    public float delay = 0.1f;
    public GameObject TextComic;

    private void Start()
    {

        StartCoroutine(ShowText());
        //Bird.GetComponent<Animator>().Play("IdleSpeak");

    }

    private void Update()
    {
        //SpeakTree();
    }

    public IEnumerator ShowText()
    {
        for (int i = 0; i < TextStart.Length; i++)
        {
            currentText = TextStart.Substring(0, i);
            //Debug.Log(Bird.transform.GetChild(0).transform.GetChild(1).name);
            TextComic.transform.GetChild(0).GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        //Bird.GetComponent<Animator>().Play("idleMute");
        //Bird.GetComponent<Animator>().enabled = false;
    }

    
 

}
