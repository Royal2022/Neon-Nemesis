using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroy : MonoBehaviour
{
    public float Time;

    void Start()
    {
        Invoke("ObjDestroy", Time);
    }

    private void ObjDestroy()
    {
        Destroy(gameObject);
    }

}
