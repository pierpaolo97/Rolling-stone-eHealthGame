using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class createXML : MonoBehaviour
{
    public Text provaText;

    public void addDataToXML(string q, string a, string r, string sc)
    {
        XmlDocument xmlDocument = new XmlDocument();

        #if UNITY_EDITOR
                xmlDocument.Load(Application.dataPath + "/list_XML.xml");
        #endif

        #if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
            xmlDocument.Load(Application.persistentDataPath + "/list_XML.xml");
        #endif

        XmlElement root = xmlDocument.DocumentElement;

        XmlElement question = xmlDocument.CreateElement("Question");
        question.InnerText = q;
        root.AppendChild(question);

        XmlElement answer = xmlDocument.CreateElement("Answer");
        answer.InnerText = a;
        question.AppendChild(answer);

        XmlElement output = xmlDocument.CreateElement("OkOrNot");
        output.InnerText = r;
        question.AppendChild(output);

        XmlElement score = xmlDocument.CreateElement("Score");
        score.InnerText = sc;
        question.AppendChild(score);

        xmlDocument.AppendChild(root);

        #if UNITY_EDITOR
            Debug.Log("UNITY_EDITOR");
            xmlDocument.Save(Application.dataPath + "/list_XML.xml");
        #endif

        #if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
            Debug.Log("IOS-ANDROID");
            xmlDocument.Save(Application.persistentDataPath + "/list_XML.xml");
            provaText.text = "CARICATO SU ANDROID";
        #endif

        

    }

    public void newXML(string q, string a, string r, string sc)
    {
        XmlDocument xmlDocument = new XmlDocument();
        XmlElement root = xmlDocument.CreateElement("ListOfQuestion");

        XmlElement question = xmlDocument.CreateElement("Question");
        question.InnerText = q;
        root.AppendChild(question);

        XmlElement answer = xmlDocument.CreateElement("Answer");
        answer.InnerText = a;
        question.AppendChild(answer);

        XmlElement output = xmlDocument.CreateElement("OkOrNot");
        output.InnerText = r;
        question.AppendChild(output);

        XmlElement score = xmlDocument.CreateElement("Score");
        score.InnerText = sc;
        question.AppendChild(score);

        xmlDocument.AppendChild(root);

        #if UNITY_EDITOR
            Debug.Log("UNITY_EDITOR");
            xmlDocument.Save(Application.dataPath + "/list_XML.xml");
        #endif

        #if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
            xmlDocument.Save(Application.persistentDataPath + "/list_XML.xml");
             provaText.text = "CREATO SU ANDROID";
        #endif
       
    }
    
}
