using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class privacyPolicy : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame


    public void Leggi()
    {
        Application.OpenURL("https://sites.google.com/view/rollingstone-seriousgame/home-page");
    }

    public void Accetto()
    {
        PlayerPrefs.SetInt("PrivacyPolicy", 1);
        this.gameObject.SetActive(false);
    }
}
