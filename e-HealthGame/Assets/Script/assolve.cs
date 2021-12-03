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
    /*public GameObject risposta1;
    public GameObject risposta2;
    public GameObject fuoco1;
    public GameObject fuoco2;*/
    private Color colMat;



    public void FixedUpdate()
    {
        if (x > 0)
        {
            for (int i = 0; i < allMaterials.Count; i++)
            {
                //dissolveMat[i].SetFloat("Dissolve_Value", Mathf.Lerp(0f, 1f, t));
                allMaterials[i].SetFloat("Dissolve_Value", Mathf.Lerp(1f, 0f, t));
                t += 0.008f * Time.fixedDeltaTime;
            }
        }

    }


    public void changeMaterial()
    {

        GameObject[] allObjects = Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
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
                            colMat = mat.color;
                            mat.shader = shaderDissolve;
                            mat.SetColor("_Albedo", colMat);
                        }
                        else
                        {
                            //Debug.LogWarning("NO COLOR" + mat.name);
                            mat.shader = shaderDissolve;
                            mat.SetColor("_Albedo", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
                        }

                    }
                }
            }
        }
        x++;
    }


}
