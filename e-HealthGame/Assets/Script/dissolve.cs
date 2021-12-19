using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissolve : MonoBehaviour
{
    public Material[] dissolveMat;
    public float t = 0.0f;
    int x = 0;
    public GameObject player;
    public Shader shaderDissolve;
    public List<Material> allMaterials;
    public GameObject risposta1;
    public GameObject risposta2;
    public GameObject risposte;
    GameObject fuoco1;
    GameObject GrassBlade;
    GameObject GrassBlade2;
    private Color colMat;
    int fatto = 0;

    public void Start()
    {
        /*risposta1 = risposte.transform.Find("R1").transform.Find("Risposta").gameObject;
        risposta2 = risposte.transform.Find("R2").transform.Find("Risposta").gameObject;*/

        risposta1 = risposte.transform.Find("R1").gameObject;
        risposta2 = risposte.transform.Find("R2").gameObject;
    }

    public void FixedUpdate()
    {      
        if (x > 0 && fatto==0)
        {    
            for (int i = 0; i < allMaterials.Count; i++)
            {
                //dissolveMat[i].SetFloat("Dissolve_Value", Mathf.Lerp(0f, 1f, t));
                allMaterials[i].SetFloat("Dissolve_Value", Mathf.Lerp(0f, 1f, t));
            }
            t += 0.5f * Time.fixedDeltaTime;


            if (GrassBlade != null)
            {
                int n_child = GrassBlade.transform.childCount;
                for (int i = 0; i < n_child; i++)
                {
                    GameObject child = GrassBlade.transform.GetChild(i).gameObject;
                    child.GetComponent<ProceduralGrassRenderer>().instantiatedGrassComputeShader.SetFloat("_BladeWidth", Mathf.Lerp(0.5f, 0f, t));
                    child.GetComponent<ProceduralGrassRenderer>().instantiatedGrassComputeShader.SetFloat("_BladeHeight", Mathf.Lerp(2f, 0f, t));
                }
            }

            if (GrassBlade2 != null)
            {
                int n_child = GrassBlade2.transform.childCount;
                for (int i = 0; i < n_child; i++)
                {
                    GameObject child = GrassBlade2.transform.GetChild(i).gameObject;
                    child.GetComponent<ProceduralGrassRenderer>().instantiatedGrassComputeShader.SetFloat("_BladeWidth", Mathf.Lerp(0.5f, 0f, t));
                    child.GetComponent<ProceduralGrassRenderer>().instantiatedGrassComputeShader.SetFloat("_BladeHeight", Mathf.Lerp(2f, 0f, t));
                }
            }
        }

        if (t > 1 && fatto==0)
        {
            fatto = 1;

            if (GrassBlade != null)
            {
                GrassBlade.SetActive(false);
            }
            if (GrassBlade2 != null)
            {
                GrassBlade2.SetActive(false);
            }
        }

    }


    public void changeMaterial()
    {
        Destroy(this.GetComponent<cameraFollow>());
        Vector3 cameraTargetPosition = new Vector3(0f, 15f, -10.84f);
        iTween.MoveTo(this.gameObject, cameraTargetPosition, 2f);
        if (this.gameObject.transform.position.x > 0)
        {
            iTween.RotateTo(this.gameObject, iTween.Hash(
                "rotation", new Vector3(0, -180, 0),
                "time", 2f,
                "easetype", iTween.EaseType.linear));
        }
        if (this.gameObject.transform.position.x < 0)
        {
            iTween.RotateTo(this.gameObject, iTween.Hash(
                "rotation", new Vector3(0, 180, 0),
                "time", 2f,
                "easetype", iTween.EaseType.linear));
        }


        risposta1.SetActive(false);
        risposta2.SetActive(false);

        fuoco1 = GameObject.Find("Fire");

        if(fuoco1 != null)
        {
            fuoco1.SetActive(false);
        }

        GrassBlade = GameObject.Find("Grassblade");
        /*if (GrassBlade != null)
        {
            GrassBlade.SetActive(false);
        }*/

        GrassBlade2 = GameObject.Find("Grassblade Giostre");
        /*if (GrassBlade2 != null)
        {
            GrassBlade2.SetActive(false);
        }*/


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
                            if (go.name == "Plane")
                            {
                                mat.shader = shaderDissolve;
                                mat.SetColor("_Albedo", new Color(1f, 1f, 1f));
                            }
                            if (go.name == "Plane_sabbia")
                            {
                                mat.shader = shaderDissolve;
                                //Debug.Log("caoooooooooooooooooo");
                                mat.SetColor("_Albedo", new Color(0.6415094f, 0.5206518f, 0.3056248f));
                            }
                        }


                        
                    }
                }
            }
        }
        x++;
    }

}
