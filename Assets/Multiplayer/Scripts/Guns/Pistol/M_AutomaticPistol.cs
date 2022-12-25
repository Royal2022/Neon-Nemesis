using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_AutomaticPistol : MonoBehaviourPunCallbacks
{
    public float offset;
    public GameObject bullet;
    public Transform shotPoint;



    private float timeBtwShots;
    public float startTimeBtwShots;

    public static SpriteRenderer sr;

    // ======================== Ammo ================================

    public GameObject ammo;


    public int currentAmmo = 25;
    //public static int Player.pistol_ammo = 0;
    public int full = 25;

    public Text ammoCount;
    [SerializeField] private WeaponSwitch ws;


    // ==============================================================



    [SerializeField] private OutPlayerInfo AmmoDisplay;
    [SerializeField] private M_Player player;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ws = FindObjectOfType<WeaponSwitch>();
        player = GetComponent<M_Player>();

    }

    void Update()
    {
        if (gameObject.transform.parent)
        {
            OutText();
        }

        //OutText();

        if (timeBtwShots <= 0 && currentAmmo > 0)
        {
            if (Input.GetMouseButton(0) && gameObject.transform.parent != null)
            {
                OutText();
                if (!photonView.IsMine) return;
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


        if (Input.GetKeyDown(KeyCode.R) && M_Player.pistol_AllAmmo > 0)
        {
            Reload();
        }


    }

    public void Reload()
    {
        int reason = 25 - currentAmmo;
        if (M_Player.pistol_AllAmmo >= reason)
        {
            M_Player.pistol_AllAmmo -= reason;
            currentAmmo = 25;
        }
        else
        {
            currentAmmo = currentAmmo + M_Player.pistol_AllAmmo;
            M_Player.pistol_AllAmmo = 0;
        }

    }



    public void OutText()
    {
        //ammoCount.text = currentAmmo + "/" + Player.pistol_ammo;
        //AmmoDisplay.AmmoInfo(currentAmmo, Player.pistol_ammo);
        FindObjectOfType<OutPlayerInfo>().AmmoInfo(currentAmmo, M_Player.pistol_AllAmmo);
    }


}
