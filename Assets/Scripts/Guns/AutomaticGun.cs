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

    private Animator anim;


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


            if (timeBtwShots <= 0 && currentAmmo > 0)
            {

                if (Input.GetMouseButton(0) && gameObject.transform.parent != null)
                {
                    gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().startSpeed = 1;
                    Instantiate(bullet, shotPoint.position, transform.rotation);
                    timeBtwShots = startTimeBtwShots;
                    currentAmmo -= 1;
                    anim.SetBool("Shot", true);
                }
                else
                {
                    gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().startSpeed = 0;
                    anim.SetBool("Shot", false);
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }

            if (currentAmmo == 0)
            {
                gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().startSpeed = 0;
                anim.SetBool("Shot", false);
            }
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

    // ==============================================================


}
