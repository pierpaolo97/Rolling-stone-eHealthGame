using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fromKeyboard : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField] public float speed = 1f;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rigid.AddForce(0, 0, speed);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            rigid.AddForce(0, 0, -speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigid.AddForce(-speed, 0, -0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigid.AddForce(speed, 0, -0);
        }
    }
}
