using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Accelerometer : MonoBehaviour
{
    private Rigidbody rigid;
    public bool isFlat = true;
    private Vector3 startingAcceleration;
    [SerializeField] public float speed = 1f;

    void Start()
    {
        startingAcceleration = Input.acceleration;
        Debug.Log("Starting:" + startingAcceleration);
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
                Vector3 theAcceleration = Input.acceleration;
                //Debug.Log(theAcceleration);
                Vector3 fixedAcceleration = theAcceleration - startingAcceleration;

                rigid.velocity = new Vector3(fixedAcceleration.x * Time.fixedDeltaTime * speed, rigid.velocity.y, fixedAcceleration.y * Time.fixedDeltaTime * speed);
                //Debug.Log(rigid.velocity);
                //rigid.AddForce(fixedAcceleration.x * speed, 0, fixedAcceleration.y * speed);
            }
        }

        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (this.GetComponent<Tutorial_Collision>().tocco > 0)
            {
                Vector3 theAcceleration = Input.acceleration;
                Debug.Log(theAcceleration);
                Vector3 fixedAcceleration = theAcceleration - startingAcceleration;

                rigid.velocity = new Vector3(fixedAcceleration.x * Time.fixedDeltaTime * speed, rigid.velocity.y, fixedAcceleration.y * Time.fixedDeltaTime * speed);
                Debug.Log(rigid.velocity);
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
