using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    private Rigidbody rigid;
    public bool isFlat = true;

    [SerializeField] public float speed=1f;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 acc = Input.acceleration;
        rigid.AddForce(acc.x * speed, 0, acc.y * speed);
    }
}
