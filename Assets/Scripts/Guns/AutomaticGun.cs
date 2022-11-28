using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class AutomaticGun : MonoBehaviour
{
    public float offset;
    public GameObject bullet;
    public Transform shotPoint;



    private float timeBtwShots;
    public float startTimeBtwShots;

    public static SpriteRenderer sr;

    // ======================== Ammo ================================

    public GameObject ammo;


    public int currentAmmo = 35;
    //public static int Player.automaticGun_ammo = 0;
    public int full = 35;

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
        if (gameObject.transform.parent)
        {
            OutText();
        }
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
        /*
        if (ws.weaponSwitch == 2)
        {
            ammoCount.text = currentAmmo + "/" + Player.automaticGun_ammo;
        }*/

        if (timeBtwShots <= 0 && currentAmmo > 0)
        {
            if (Input.GetMouseButton(0) && gameObject.transform.parent != null)
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


        if (Input.GetKeyDown(KeyCode.R) && Player.automaticGun_ammo > 0)
        {
            Reload();
        }


    }

    public void Reload()
    {
        int reason = 35 - currentAmmo;
        if (Player.automaticGun_ammo >= reason)
        {
            Player.automaticGun_ammo -= reason;
            currentAmmo = 35;
        }
        else
        {
            currentAmmo = currentAmmo + Player.automaticGun_ammo;
            Player.automaticGun_ammo = 0;
        }

    }

    public void OutText()
    {
        ammoCount.text = currentAmmo + "/" + Player.automaticGun_ammo;
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pistol2"))
        {
            Player.automaticGun_ammo += 15;
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("pistol1"))
        {
            Pistol.Player.automaticGun_ammo += 15;
            Destroy(collision.gameObject);
        }

    }*/
    // ==============================================================


}
