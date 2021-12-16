using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationOnACircle : MonoBehaviour
{
    float rotationSpeed;
    public GameObject pivotObject;

    // Start is called before the first frame update
    void Start()
    {
        string diff = PlayerPrefs.GetString("difficolta", "Easy");
        switch (diff)
        {
            case "Easy":
                rotationSpeed = 25f;
                break;
            case "Medium":
                rotationSpeed = 50f;
                break;
            case "Hard":
                rotationSpeed = 75f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivotObject.transform.position, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }
}
