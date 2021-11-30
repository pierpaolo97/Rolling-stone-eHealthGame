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
    public GameObject fuoco1;
    public GameObject fuoco2;


    public void FixedUpdate()
    {      
        if (x > 0)
        {
            
            for (int i = 0; i < allMaterials.Count; i++)
            {
                //dissolveMat[i].SetFloat("Dissolve_Value", Mathf.Lerp(0f, 1f, t));
                allMaterials[i].SetFloat("Dissolve_Value", Mathf.Lerp(0f, 1f, t));
                t += 0.008f * Time.fixedDeltaTime;
            }
        }
       
    }


    public void changeMaterial()
    {
        Destroy(this.GetComponent<cameraFollow>());
        Vector3 cameraTargetPosition = new Vector3(0f, 15f, -10.84f);
        iTween.MoveTo(this.gameObject, cameraTargetPosition, 2f);
        iTween.RotateTo(this.gameObject, iTween.Hash(
            "rotation", new Vector3(180, 180, 180),
            "time", 2f,
            "easetype", iTween.EaseType.linear));

        risposta1.SetActive(false);
        risposta2.SetActive(false);

        if (fuoco1.activeSelf)
        {
            fuoco1.SetActive(false);
        }

        if (fuoco2.activeSelf)
        {
            fuoco2.SetActive(false);
        }

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
                        mat.shader = shaderDissolve;
                        mat.SetColor("_Albedo", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
                    }
                }
            }
        }
        x++;
    }

   


}
