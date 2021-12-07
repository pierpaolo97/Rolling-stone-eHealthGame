using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Accelerometer : MonoBehaviour
{
    private Rigidbody rigid;
    public bool isFlat = true;
    private Vector3 startingAcceleration;
    [SerializeField] public float speed = 1f;
    public GameObject accText;
    public GameObject accText2;
    int x = 0;
    int z = 0;

    void Start()
    {
        /*startingAcceleration = Input.acceleration;
        Debug.Log("Starting:" + startingAcceleration);

        if(startingAcceleration.y < -0.5f)
        {
            startingAcceleration.y = 0f;
        }*/

        rigid = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        /*Vector3 acc = Input.acceleration;
         rigid.AddForce(acc.x * speed, 0, acc.y * speed);*/


        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            if ((this.GetComponent<detectCollision>().tocco > 0))
            {
                if (x == 0)
                {
                    startingAcceleration = Input.acceleration;
                    //accText.GetComponent<Text>().text = "Input acc." + startingAcceleration.ToString();
                    //Debug.Log("Starting:" + startingAcceleration);

                    if (startingAcceleration.y > 0f)
                    {
                        startingAcceleration.y = 0f;
                    }
                    if (startingAcceleration.y <= -0.7f)
                    {
                        z++;
                    }

                    x++;
                }

                Vector3 theAcceleration = Input.acceleration;
                if(z>0 && theAcceleration.y <= -0.9)
                {
                    theAcceleration.y = theAcceleration.y * 1.2f;
                }
                //accText2.GetComponent<Text>().text = "Now ACC:" + theAcceleration.ToString();

                //Debug.Log(theAcceleration);
                Vector3 fixedAcceleration = theAcceleration - new Vector3(0f, startingAcceleration.y, 0f);
                rigid.velocity = new Vector3(fixedAcceleration.x * Time.fixedDeltaTime * speed, rigid.velocity.y, fixedAcceleration.y * Time.fixedDeltaTime * speed);               
            }
        }

        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (this.GetComponent<Tutorial_Collision>().tocco > 0)
            {
                if (x == 0)
                {
                    startingAcceleration = Input.acceleration;
                    //accText.GetComponent<Text>().text = "Input acc." + startingAcceleration.ToString();
                    Debug.Log("Starting:" + startingAcceleration);

                    if (startingAcceleration.y > 0f)
                    {
                        startingAcceleration.y = 0f;
                    }
                    if (startingAcceleration.y <= -0.7f)
                    {
                        z++;
                    }

                    x++;
                }

                Vector3 theAcceleration = Input.acceleration;
                if (z > 0 && theAcceleration.y <= -0.9)
                {
                    theAcceleration.y = theAcceleration.y * 1.2f;
                }
                //accText2.GetComponent<Text>().text = "Now ACC:" + theAcceleration.ToString();

                //Debug.Log(theAcceleration);
                Vector3 fixedAcceleration = theAcceleration - new Vector3(0f, startingAcceleration.y, 0f);
                rigid.velocity = new Vector3(fixedAcceleration.x * Time.fixedDeltaTime * speed, rigid.velocity.y, fixedAcceleration.y * Time.fixedDeltaTime * speed);
            }
        }





    }


}













/*public class Accelerometer : MonoBehaviour
{
    private Rigidbody rigid;
    public bool isFlat = true;

    [SerializeField] public float speed=1f;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 acc = Input.acceleration;
        rigid.AddForce(acc.x * speed, 0, acc.y * speed);
    }

   
}*/
