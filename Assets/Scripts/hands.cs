using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hands : MonoBehaviour
{

    public float offset;


    void Start()
    {
        
    }
    //public static bool tf = false;

    void Update()
    {
        /*
        if (tf)
        {
            res();
            tf = false;
        }*/        

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.Rotate(0f, 0f, 0f);

        if (Player.facingRight == true)
        {
            offset = 0f;
            if (rotZ < 30 && rotZ > -45)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
            }
        }
        else if (Player.facingRight == false)
        {
            offset = 180f;
            if (rotZ < -135 || rotZ > 150)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
            }
        }
    }
    public void res()
    {
        GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
        Debug.Log("Robit");
    }


}
