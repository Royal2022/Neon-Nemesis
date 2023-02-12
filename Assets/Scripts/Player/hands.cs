using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class hands : MonoBehaviour
{
    public GameObject player;

    public float offset;

    public int sensitivity = 20;

    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float r = difference.y * sensitivity;

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
