using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neverSleep : MonoBehaviour
{
    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
