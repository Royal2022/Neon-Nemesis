using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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

    public AudioSource GetGunSound;
    public AudioSource ReloadSound;
    public AudioSource ShotSound;

    private void Start()
    {      
        sr = GetComponent<SpriteRenderer>();
        ws = FindObjectOfType<WeaponSwitch>();
    }

    void Update()
    {
        var main = gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().main;

        if (gameObject.transform.parent != null)
        {
            GetGunSound.enabled = true;
            GameObject ParentHands = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
            anim = ParentHands.GetComponent<Animator>();
            ammoCount = ParentHands.GetComponent<HandsAutomaticGun>().AmmoCountText;

            if (gameObject.transform.parent)
            {
                OutText();
            }



            if (timeBtwShots <= 0 && currentAmmo > 0)
            {

                if (Input.GetMouseButton(0) && gameObject.transform.parent != null && !anim.GetBool("reload"))
                {
                    ShotSound.Play();
                    main.startSpeed = 1;
                    //gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().startSpeed = 1;
                    Instantiate(bullet, shotPoint.position, transform.rotation);
                    timeBtwShots = startTimeBtwShots;
                    currentAmmo -= 1;
                    anim.SetBool("Shot", true);
                    shotPoint.GetComponent<Light2D>().intensity = 2.5f;
                    Invoke("offLight", 0.05f);
                }
                else
                {
                    main.startSpeed = 0;
                    //gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().startSpeed = 0;
                    anim.SetBool("Shot", false);
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }

            if (currentAmmo == 0)
            {
                main.startSpeed = 0;
                //gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().startSpeed = 0;
                anim.SetBool("Shot", false);
            }



            if (Input.GetKeyDown(KeyCode.R) && Player.automaticGun_ammo > 0 && currentAmmo < 35)
            {
                anim.SetBool("reload", true);
                ReloadSound.Play();
            }
        }
        else
        {
            GetGunSound.enabled = false;
            main.startSpeed = 0;
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


    public void offLight()
    {
        shotPoint.GetComponent<Light2D>().intensity = 0;
    }
}
