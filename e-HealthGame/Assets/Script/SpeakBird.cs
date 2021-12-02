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

        StartCoroutine(ShowText(TextStart));

    }

    private void Update()
    {
        SpeakTree();
    }

    IEnumerator ShowText(string textDaScrivere)
    {
        check_text = true;
        for (int i = 0; i < textDaScrivere.Length; i++)
        {
            currentText = textDaScrivere.Substring(0, i);
            //Debug.Log(Bird.transform.GetChild(0).transform.GetChild(1).name);
            TextComic.transform.GetChild(0).GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }
        check_text = false;
    }

    public void SpeakTree()
    {
        if (check_text == true )
        {
            Bird.GetComponent<Animator>().Play("IdleSpeak");
        }
        else if (check_text == false ) 
        {
            Bird.GetComponent<Animator>().Play("idleMute");
        }

    }

}
