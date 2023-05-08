using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_AutomaticGun : MonoBehaviour
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

    public Animator anim;

    private M_Player player;

    public bool SHOT;

    public ParticleSystem ParticalS;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ws = FindObjectOfType<WeaponSwitch>();
    }


    void Update()
    {
        if (gameObject.transform.parent != null)
        {
            player = transform.root.gameObject.GetComponent<M_Player>();

            anim = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.GetComponent<Animator>();

            if (gameObject.transform.parent)
            {
                OutText();
            }

            var main = gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().main;

            if (timeBtwShots <= 0 && currentAmmo > 0)
            {
                if (Input.GetMouseButton(0) && gameObject.transform.parent != null && transform.root.gameObject.GetComponent<M_Player>().IsItYou)
                {
                    SHOT = true;
                    //player.AnimTrueOrFalse("M_Fire_AutomaticGun", true);
                    //anim.SetBool("M_Fire_AutomaticGun", true);
                    //anim.SetBool("Shot", true);
                    OutText();
                    player.CallSpawnBullet(player.netId, shotPoint.position, transform.rotation);
                    timeBtwShots = startTimeBtwShots;
                    currentAmmo -= 1;
                    main.startSpeed = player.ParticalValue;
                }
                else
                {
                    SHOT = false;
                    //anim.SetBool("Shot", false);
                    //player.AnimTrueOrFalse("M_Fire_AutomaticGun", false);
                    //anim.SetBool("M_Fire_AutomaticGun", false);
                    main.startSpeed = player.ParticalValue;
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }



            if (currentAmmo == 0)
            {
                //anim.SetBool("M_Fire_AutomaticGun", false);
                main.startSpeed = player.ParticalValue;
                SHOT = false;
            }



            if (player.facingRight == true)
            {
                ParticalS.transform.localScale = new Vector3(1, 1, 1);
                ParticalS.GetComponent<ParticleSystemRenderer>().lengthScale = -2;
            }
            else if (player.facingRight == false)
            {
                ParticalS.transform.localScale = new Vector3(-1, 1, 1);
                ParticalS.GetComponent<ParticleSystemRenderer>().lengthScale = 2;
            }
        }


        // ======================== Ammo ================================

        if (Input.GetKeyDown(KeyCode.R) && M_Player.automaticGun_AllAmmo > 0)
        {
            Reload();
        }
    }



    public void Reload()
    {
        if (gameObject.transform.parent != null)
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
    }

    public void OutText()
    {
        if (player.isLocalPlayer)
            FindObjectOfType<OutPlayerInfo>().AmmoInfo(currentAmmo, M_Player.automaticGun_AllAmmo);
    }

}
