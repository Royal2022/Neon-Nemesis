using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class hands : MonoBehaviour
{
    [SerializeField] private Player player;
    public float offset;

    public int sensitivity = 20;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
    
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float r = difference.y * sensitivity;

        /*
        transform.Rotate(0f, 0f, 0f);
        if (Player.facingRight == true)
        {
            offset = -2f;
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
        }*/




        if (difference.y < 1 && difference.y > -2)
        {
            if (Player.facingRight)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, r + offset);
            }
            else if (!Player.facingRight)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -r + offset);
            }
        }
    }
    public void res()
    {
        GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
    }


}
