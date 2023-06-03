using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhoneSystem : MonoBehaviour
{
    public GameObject[] WindowPhone;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !anim.GetBool("up_or_down"))
            anim.SetBool("up_or_down", true);
        else if (Input.GetKeyDown(KeyCode.Tab) && anim.GetBool("up_or_down"))
            anim.SetBool("up_or_down", false);
    }

    public void ClickOpenWinodow(int id)
    {
        for (int i = 0; i < WindowPhone.Length; i++)
        {
            if (WindowPhone[i].activeSelf)
                WindowPhone[i].SetActive(false);
        }
        WindowPhone[id].SetActive(true);
    }

    public void ButtonHome()
    {
        if (!WindowPhone[0].activeSelf)
        {
            for (int i = 0; i < WindowPhone.Length; i++)
            {
                if (WindowPhone[i].activeSelf)
                    WindowPhone[i].SetActive(false);
            }
            WindowPhone[0].SetActive(true);
        }
    }
}
