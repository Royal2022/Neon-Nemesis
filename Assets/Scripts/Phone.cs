using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public Animator anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !anim.GetBool("up_or_down"))
        {
            anim.SetBool("up_or_down", true);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && anim.GetBool("up_or_down"))
        {
            anim.SetBool("up_or_down", false);
        }
    }



    public void OnClick_BuyAmmoPistol()
    {

    }
}
