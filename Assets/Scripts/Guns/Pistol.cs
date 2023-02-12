using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private Animator anim;


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
        if (gameObject.transform.parent != null)
        {
            anim = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.GetComponent<Animator>();


            if (gameObject.transform.parent)
            {
                OutText();
            }

            //OutText();


            if (timeBtwShots <= 0 && currentAmmo > 0)
            {
                if (Input.GetMouseButtonDown(0) && gameObject.transform.parent != null)
                {
                    gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
                    Instantiate(bullet, shotPoint.position, transform.rotation);
                    timeBtwShots = startTimeBtwShots;
                    currentAmmo -= 1;
                    OutText();
                    anim.SetBool("fire", true);
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
                anim.SetBool("fire", false);
            }



            if (Player.facingRight == true)
            {
                gameObject.transform.GetChild(1).gameObject.transform.localScale = new Vector3(1, 1, 1);
                gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystemRenderer>().lengthScale = -2;
            }
            else if (Player.facingRight == false)
            {
                gameObject.transform.GetChild(1).gameObject.transform.localScale = new Vector3(-1, 1, 1);
                gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystemRenderer>().lengthScale = 2;
            }
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
    }

    
    // ==============================================================




}
