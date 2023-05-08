using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandsAutomaticGun : MonoBehaviour
{
    public GameObject player;

    public float offset;

    public int sensitivity = 20;

    public Sprite[] AllHandPosition;
    public Transform[] AllHoldPointPosition;

    public Transform HoldPoint;

    public Text AmmoCountText;

    private float r;


    void Update()
    {
        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //float r = difference.y * sensitivity;

        //if (difference.y < 1 && difference.y > -2)
        //{
        //    if (Player.facingRight)
        //    {
        //        transform.rotation = Quaternion.Euler(0f, 0f, r + offset);
        //    }
        //    else if (!Player.facingRight)
        //    {
        //        transform.rotation = Quaternion.Euler(0f, 0f, -r + offset);
        //    }
        //}


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


        //if (r < 90 && r > -90)
        //{
        //    if (r > -20 && r < -19)
        //    {
        //        if (Player.facingRight)
        //        {
        //            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[0];
        //            SearchGun(0);
        //        }
        //        else if (!Player.facingRight)
        //        {
        //            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[2];
        //            SearchGun(2);
        //        }
        //        transform.rotation = Quaternion.Euler(0f, 0f, r);
        //    }
        //    else if (r > -19 && r < 19)
        //    {
        //        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[1];
        //        SearchGun(1);
        //        transform.rotation = Quaternion.Euler(0f, 0f, r);
        //    }
        //    else if (r > 19 && r < 20)
        //    {
        //        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[2];
        //        if (Player.facingRight)
        //        {
        //            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[2];
        //            SearchGun(2);
        //        }
        //        else if (!Player.facingRight)
        //        {
        //            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[0];
        //            SearchGun(0);
        //        }
        //        transform.rotation = Quaternion.Euler(0f, 0f, r);
        //    }
        //}
    }


    private void FixedUpdate()
    {

        //var difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float d = difference.y * sensitivity;

        //if (Player.facingRight)
        //{
        //    r = d + offset;
        //}
        //else if (!Player.facingRight)
        //{
        //    r = -d + offset;
        //}


        //if (difference.y < 1 && difference.y > 0)
        //{
        //    transform.rotation = Quaternion.Euler(0f, 0f, r);
        //}


        //if (r < 90 && r > -90)
        //{
        //    //if (r > -90 && r < -60)
        //    //{
        //    //    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[0];
        //    //    SearchGun(0);
        //    //    transform.rotation = Quaternion.Euler(0f, 0f, r);
        //    //}
        //    //else if (r > -60 && r < -30)
        //    //{
        //    //    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[1];
        //    //    SearchGun(1);
        //    //    transform.rotation = Quaternion.Euler(0f, 0f, r);
        //    //}
        //    //else if (r > -30 && r < 0)
        //    //{
        //    //    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[2];
        //    //    SearchGun(2);
        //    //    transform.rotation = Quaternion.Euler(0f, 0f, r);
        //    //}
        //    //else if (r > 0 && r < 30)
        //    //{
        //    //    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[3];
        //    //    SearchGun(3);
        //    //    transform.rotation = Quaternion.Euler(0f, 0f, r);
        //    //}


        //    if (r > -20 && r < -15)
        //    {
        //        if (Player.facingRight)
        //        {
        //            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[0];
        //            SearchGun(0);
        //        }
        //        else if (!Player.facingRight)
        //        {
        //            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[2];
        //            SearchGun(2);
        //        }
        //        transform.rotation = Quaternion.Euler(0f, 0f, r);
        //    }
        //    else if (r > -15 && r < 15)
        //    {
        //        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[1];
        //        SearchGun(1);
        //        transform.rotation = Quaternion.Euler(0f, 0f, r);
        //    }
        //    else if (r > 15 && r < 20)
        //    {
        //        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[2];
        //        if (Player.facingRight)
        //        {
        //            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[2];
        //            SearchGun(2);
        //        }
        //        else if (!Player.facingRight)
        //        {
        //            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[0];
        //            SearchGun(0);
        //        }
        //        transform.rotation = Quaternion.Euler(0f, 0f, r);
        //    }
        //    //else if (r > 15 && r < 20)
        //    //{
        //    //    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[3];
        //    //    SearchGun(3);
        //    //    transform.rotation = Quaternion.Euler(0f, 0f, r);
        //    //}




        //    //else if (r > 30 & r < 60)
        //    //{
        //    //    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[4];
        //    //    SearchGun(4);
        //    //}
        //    //else if (r > 60 && r < 90)
        //    //{
        //    //    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[5];
        //    //    SearchGun(5);
        //    //}
        //}
    }

    //private void SearchGun(int num)
    //{
    //    for (int i = 0; i <= transform.GetChild(0).childCount - 1; i++)
    //    {
    //        if (transform.GetChild(0).GetChild(i).childCount > 0)
    //        {
    //            HoldPoint.position = AllHoldPointPosition[num].position;
    //            HoldPoint.rotation = AllHoldPointPosition[num].rotation;
    //            HoldPoint.GetChild(0).transform.rotation = AllHoldPointPosition[num].rotation;
    //        }
    //    }
    //}

    public void res()
    {
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = AllHandPosition[1];
        //SearchGun(1);
        GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
    }


    public void ReloadEndAutomaticGun()
    {
        GetComponent<Animator>().SetBool("reload", false);
        Transform holdPoint = transform.GetChild(0).GetChild(0).GetChild(0);
        if (holdPoint.childCount > 0)
            holdPoint.GetChild(0).GetComponent<AutomaticGun>().Reload();
    }
}
