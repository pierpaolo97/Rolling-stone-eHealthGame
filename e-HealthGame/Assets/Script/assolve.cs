using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assolve : MonoBehaviour
{
    public Material[] dissolveMat;
    public float t = 0.0f;
    int x = 0;
    public GameObject player;
    public Shader shaderDissolve;
    public List<Material> allMaterials;

    public List<Shader> oldShadersList;
    /*public GameObject risposta1;
    public GameObject risposta2;
    public GameObject fuoco1;
    public GameObject fuoco2;*/
    private Color colMat;
    int fatto = 0;



    public void FixedUpdate()
    {
        if (x > 0 && fatto == 0)
        {
            for (int i = 0; i < allMaterials.Count; i++)
            {
                //dissolveMat[i].SetFloat("Dissolve_Value", Mathf.Lerp(0f, 1f, t));
                allMaterials[i].SetFloat("Dissolve_Value", Mathf.Lerp(1f, 0f, t));
                t += 0.008f * Time.fixedDeltaTime;
                //Debug.Log("Nel fixed update");
            }

        }

        if (t >= 1f && fatto == 0)
        {
            fatto = 1;
            reChange();

        }


    }

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Level", 0) > 0)
        {
            Debug.Log((PlayerPrefs.GetInt("Level", 0)));
            changeMaterial();
        }
    }


    public void changeMaterial()
    {

        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.transform.name != "DOMANDA" && go.name != "Effetto" && go.name != "Effetto (1)" && go.name != "Parola" && go.name != "Immagine" && go.name != "textR1" && go.name != "textR2" && go.name != "AtomBallLines" && go.name != "AtomBallSpheres")
            {
                if (go.GetComponent<MeshRenderer>() != null)
                {
                    if (go.GetComponent<MeshRenderer>().materials != null)
                    {
                        Material[] materials = go.GetComponent<MeshRenderer>().materials;
                        foreach (Material mat in materials)
                        {
                            allMaterials.Add(mat);
                            if (mat.HasProperty("_Color"))
                            {
                                oldShadersList.Add(mat.shader);
                                colMat = mat.color;
                                mat.shader = shaderDissolve;
                                mat.SetColor("_Albedo", colMat);
                            }
                            else
                            {
                                //Debug.LogWarning("NO COLOR" + mat.name);
                                oldShadersList.Add(mat.shader);
                                mat.shader = shaderDissolve;
                                mat.SetColor("_Albedo", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
                            }

                        }
                    }
                }
            }
        }
        x++;
    }

    public void reChange()
    {
        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
        int i = 0;
        foreach (GameObject go in allObjects)
        {
            if (go.transform.name != "DOMANDA" && go.name != "Effetto" && go.name != "Effetto (1)" && go.name != "Parola" && go.name != "Immagine" && go.name != "textR1" && go.name != "textR2" && go.name != "AtomBallLines" && go.name != "AtomBallSpheres")
            {
                if (go.GetComponent<MeshRenderer>() != null)
                {
                    if (go.GetComponent<MeshRenderer>().materials != null)
                    {
                        Material[] materials = go.GetComponent<MeshRenderer>().materials;
                        foreach (Material mat in materials)
                        {
                            mat.shader = oldShadersList[i];
                            i++;

                        }
                    }
                }
            }
        }
        x++;
    }


}
