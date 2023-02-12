using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class M_AutomaticGun : MonoBehaviourPun
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

    public int full = 35;

    [SerializeField] private WeaponSwitch ws;


    // ==============================================================

    [SerializeField] private OutPlayerInfo AmmoDisplay;
    [SerializeField] private M_Player player;


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


        if (timeBtwShots <= 0 && currentAmmo > 0)
        {
            if (Input.GetMouseButton(0) && gameObject.transform.parent != null && transform.root.gameObject.GetComponent<M_Player>().IsItYou)
            {
                OutText();
                PhotonNetwork.Instantiate(bullet.name, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
                currentAmmo -= 1;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        // ======================== Ammo ================================

        if (Input.GetKeyDown(KeyCode.R) && M_Player.automaticGun_AllAmmo > 0)
        {
            Reload();
        }
    }

    public void Reload()
    {
        int reason = 35 - currentAmmo;
        if (M_Player.automaticGun_AllAmmo >= reason)
        {
            M_Player.automaticGun_AllAmmo -= reason;
            currentAmmo = 35;
        }
        else
        {
            currentAmmo = currentAmmo + M_Player.automaticGun_AllAmmo;
            M_Player.automaticGun_AllAmmo = 0;
        }

    }

    public void OutText()
    {
        if (!photonView.IsMine) return;
            FindObjectOfType<OutPlayerInfo>().AmmoInfo(currentAmmo, M_Player.automaticGun_AllAmmo);
    }



}
