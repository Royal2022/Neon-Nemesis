using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class hands : MonoBehaviour
{
    public GameObject player;

    public float offset;

    public int sensitivity = 20;

    public Text AmmoCountText;

    void Update()
    {
        if (Time.timeScale != 0)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
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
    }

    public GameObject Gun;
    public void res()
    {
        GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
        Gun.transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void ReloadEndPistol()
    {
        GetComponent<Animator>().SetBool("reload", false);
        Transform holdPoint = transform.GetChild(0).GetChild(0).GetChild(0);
        if (holdPoint.childCount > 0)
            holdPoint.GetChild(0).GetComponent<Pistol>().Reload();
    }
}
