using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pistol : MonoBehaviour
{
    public float offset;
    public GameObject bullet;
    public Transform shotPoint;


    private float timeBtwShots;
    public float startTimeBtwShots;

    public static SpriteRenderer sr;


    // ======================== Ammo ================================

    public GameObject ammo;

    public int currentAmmo = 15;
    public static int allAmmo = 0;
    public int full = 45;

    [SerializeField]
    private Text ammoCount;


    // ==============================================================



    private void Start()
    {
            sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        /*
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.Rotate(0f, 0f, 0f);

        if (Player.facingRight == true)
        {
            offset = 0f;
            if (rotZ < 90 && rotZ > -90)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
            }
        }
        else if (Player.facingRight == false)
        {
            offset = 180f;            
            if (rotZ < -90 || rotZ > 90)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
            }
        }
        */


        if (timeBtwShots <= 0 && currentAmmo > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
                currentAmmo -= 1;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }






    // ======================== Ammo ================================

        ammoCount.text = currentAmmo + "/" + allAmmo;

        if (Input.GetKeyDown(KeyCode.R) && allAmmo > 0)
        {
            Reload();
        }

    
    }

    public void Reload()
    {
        int reason = 15 - currentAmmo;
        if (allAmmo >= reason)
        {
            allAmmo -= reason;
            currentAmmo = 15;
        }
        else
        {
            currentAmmo = currentAmmo + allAmmo;
            allAmmo = 0;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pistol1"))
        {
            allAmmo += 15;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("pistol2"))
        {
            Gun2.allAmmo += 15;
            Destroy(collision.gameObject);
        }

    }
    // ==============================================================



}
