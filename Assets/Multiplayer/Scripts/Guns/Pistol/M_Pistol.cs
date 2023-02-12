using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_Pistol : MonoBehaviourPun
{
    public float offset;
    [SerializeField] private GameObject bullet;
    public Transform shotPoint;


    private float timeBtwShots;
    public float startTimeBtwShots;

    public static SpriteRenderer sr;


    // ======================== Ammo ================================

    public GameObject ammo;

    public int currentAmmo = 15;
    //public static int AllAmmo = 15;
    //public static int Player.pistol_ammo = 0;
    public int full = 45;

    //public Text ammoCount;
    [SerializeField] private WeaponSwitch ws;

    // ==============================================================

    [SerializeField] private OutPlayerInfo AmmoDisplay;
    [SerializeField] private M_Player player;


    private void Start()
    {        
        sr = GetComponent<SpriteRenderer>();
        ws = FindObjectOfType<WeaponSwitch>();
        //player = transform.root.gameObject;
    }

    void Update()
    {
        if (gameObject.transform.parent)
        {
            OutText();
        }

        if (timeBtwShots <= 0 && currentAmmo > 0)
        {
            if (Input.GetMouseButtonDown(0) && gameObject.transform.parent != null && transform.root.gameObject.GetComponent<M_Player>().IsItYou)
            {

                OutText();
                PhotonNetwork.Instantiate(bullet.name, shotPoint.position, transform.rotation);
                //Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
                currentAmmo -= 1;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    // ======================== Ammo ================================

        if (Input.GetKeyDown(KeyCode.R) && M_Player.pistol_AllAmmo > 0)
        {
            Reload();
        }

    
    }

    public void Reload()
    {
        int reason = 15 - currentAmmo;
        if (M_Player.pistol_AllAmmo >= reason)
        {
            M_Player.pistol_AllAmmo -= reason;
            currentAmmo = 15;
        }
        else
        {
            currentAmmo = currentAmmo + M_Player.pistol_AllAmmo;
            M_Player.pistol_AllAmmo = 0;
        }

    }




    public void OutText()
    {
        if (!photonView.IsMine) return;
        FindObjectOfType<OutPlayerInfo>().AmmoInfo(currentAmmo, M_Player.pistol_AllAmmo);
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
