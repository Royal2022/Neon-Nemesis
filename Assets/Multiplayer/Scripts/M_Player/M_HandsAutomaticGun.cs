using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_HandsAutomaticGun : MonoBehaviour
{
    public GameObject player;

    public float offset;

    public int sensitivity = 20;

    public Sprite[] AllHandPosition;
    public Transform[] AllHoldPointPosition;

    public Transform HoldPoint;


    private float r;

    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
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



        if (r < 90 && r > -90)
        {
            if (r > -20 && r < -15)
            {
                if (Player.facingRight)
                {
                    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[0];
                    SearchGun(0);
                }
                else if (!Player.facingRight)
                {
                    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[2];
                    SearchGun(2);
                }
                transform.rotation = Quaternion.Euler(0f, 0f, r);
            }
            else if (r > -15 && r < 15)
            {
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[1];
                SearchGun(1);
                transform.rotation = Quaternion.Euler(0f, 0f, r);
            }
            else if (r > 15 && r < 20)
            {
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[2];
                if (Player.facingRight)
                {
                    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[2];
                    SearchGun(2);
                }
                else if (!Player.facingRight)
                {
                    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[0];
                    SearchGun(0);
                }
                transform.rotation = Quaternion.Euler(0f, 0f, r);
            }
        }
    }

    

    private void SearchGun(int num)
    {
        for (int i = 0; i <= transform.GetChild(0).childCount - 1; i++)
        {
            if (transform.GetChild(0).GetChild(i).childCount > 0)
            {
                HoldPoint.position = AllHoldPointPosition[num].position;
                HoldPoint.rotation = AllHoldPointPosition[num].rotation;
                HoldPoint.GetChild(0).transform.rotation = AllHoldPointPosition[num].rotation;
            }
        }
    }

    public void AnimOnOff(string name, bool OnOff)
    {
        anim.SetBool(name, OnOff);
    }

    //public void res()
    //{
    //    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[1];
    //    SearchGun(1);
    //    GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
    //}


    //public void ReloadEndAutomaticGun()
    //{
    //    GetComponent<Animator>().SetBool("reload", false);
    //    Transform holdPoint = transform.GetChild(0).GetChild(0).GetChild(0);
    //    if (holdPoint.childCount > 0)
    //        holdPoint.GetChild(0).GetComponent<AutomaticGun>().Reload();
    //}
}
