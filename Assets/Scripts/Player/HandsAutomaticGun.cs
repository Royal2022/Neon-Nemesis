using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandsAutomaticGun : MonoBehaviour
{
    public GameObject player;

    public float offset;

    public int sensitivity = 20;

    public Text AmmoCountText;

    private float r;


    void Update()
    {
        if (Time.timeScale != 0)
        {
            var difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float d = difference.y * sensitivity;

            if (Player.facingRight)
            {
                r = d + offset;
            }
            else if (!Player.facingRight)
            {
                r = -d + offset;
            }


            if (r < 25 && r > -25)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, r);
            }
        }
    }

    public GameObject Gun;

    public void res()
    {
        GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
        Gun.transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, 0f);
    }


    public void ReloadEndAutomaticGun()
    {
        GetComponent<Animator>().SetBool("reload", false);
        Transform holdPoint = transform.GetChild(0).GetChild(0).GetChild(0);
        if (holdPoint.childCount > 0)
            holdPoint.GetChild(0).GetComponent<AutomaticGun>().Reload();
    }
}
