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
    //public static int Player.pistol_ammo = 0;
    public int full = 45;

    public Text ammoCount;
    [SerializeField] private WeaponSwitch ws;

    // ==============================================================



    private void Start()
    {        
        sr = GetComponent<SpriteRenderer>();
        ws = FindObjectOfType<WeaponSwitch>();
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

        
        if (gameObject.transform.parent)
        {
            OutText();
        }

        //OutText();

        //Debug.Log(gameObject.transform.parent.ToString());


        if (timeBtwShots <= 0 && currentAmmo > 0)
        {
            if (Input.GetMouseButtonDown(0) && gameObject.transform.parent != null)
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
                currentAmmo -= 1;
                OutText();
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }






    // ======================== Ammo ================================

        if (Input.GetKeyDown(KeyCode.R) && Player.pistol_ammo > 0)
        {
            Reload();
        }

    
    }

    public void Reload()
    {
        int reason = 15 - currentAmmo;
        if (Player.pistol_ammo >= reason)
        {
            Player.pistol_ammo -= reason;
            currentAmmo = 15;
        }
        else
        {
            currentAmmo = currentAmmo + Player.pistol_ammo;
            Player.pistol_ammo = 0;
        }

    }

    public void OutText()
    {
        ammoCount.text = currentAmmo + "/" + Player.pistol_ammo;
        //Debug.Log("cock");

    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pistol1"))
        {
            Player.pistol_ammo += 15;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("pistol2"))
        {
            AutomaticGun.Player.pistol_ammo += 15;
            Destroy(collision.gameObject);
        }

    }*/
    // ==============================================================




}
