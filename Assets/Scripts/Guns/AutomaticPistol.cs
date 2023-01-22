using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutomaticPistol : MonoBehaviour
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

        //OutText();

        if (timeBtwShots <= 0 && currentAmmo > 0)
        {
            if (Input.GetMouseButton(0) && gameObject.transform.parent != null)
            {
                gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().startSpeed = 0.5f;
                Instantiate(bullet, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
                currentAmmo -= 1;
                OutText();
            }
            else
            {
                gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().startSpeed = 0;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (currentAmmo == 0)
        {
            gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().startSpeed = 0;
        }

        // ======================== Ammo ================================


        if (Input.GetKeyDown(KeyCode.R) && Player.pistol_ammo > 0)
        {
            Reload();
        }


    }

    public void Reload()
    {
        int reason = 25 - currentAmmo;
        if (Player.pistol_ammo >= reason)
        {
            Player.pistol_ammo -= reason;
            currentAmmo = 25;
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


}
