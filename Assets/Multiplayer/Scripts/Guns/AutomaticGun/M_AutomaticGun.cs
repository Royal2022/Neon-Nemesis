using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class M_AutomaticGun : NetworkBehaviour
{
    public float offset;
    public GameObject bullet;
    public Transform shotPoint;



    private float timeBtwShots;
    public float startTimeBtwShots;

    public static SpriteRenderer sr;

    // ======================== Ammo ================================

    public GameObject ammo;

    [SyncVar]
    public int currentAmmo = 35;

    public int full = 35;

    [SerializeField] private WeaponSwitch ws;


    // ==============================================================

    [SerializeField] private OutPlayerInfo AmmoDisplay;

    public Animator anim;

    private M_Player player;

    public bool SHOT;

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


            if (timeBtwShots <= 0 && currentAmmo > 0 )
            {
                if (Input.GetMouseButton(0) && gameObject.transform.parent != null && transform.root.gameObject.GetComponent<M_Player>().IsItYou && !anim.GetBool("reloadAutoGun"))
                {
                    SHOT = true;
                    OutText();
                    player.CallSpawnBullet(player.netId, shotPoint.position, transform.rotation);
                    timeBtwShots = startTimeBtwShots;
                    //currentAmmo -= 1;
                    ChangeTheValueAmmo(currentAmmo -= 1);
                    FlashShoot();
                }
                else
                {
                    SHOT = false;
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }



            if (currentAmmo == 0)
            {
                SHOT = false;
            }

            // ======================== Ammo ================================
            if (!player.isLocalPlayer) return;
            if (Input.GetKeyDown(KeyCode.R) && M_Player.automaticGun_AllAmmo > 0 && currentAmmo < 35)
            {
                anim.SetBool("reloadAutoGun", true);
            }
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
                //currentAmmo = 35;
                ChangeTheValueAmmo(35);
            }
            else
            {
                //currentAmmo = currentAmmo + M_Player.automaticGun_AllAmmo;
                ChangeTheValueAmmo(currentAmmo + M_Player.automaticGun_AllAmmo);
                M_Player.automaticGun_AllAmmo = 0;
            }
        }
    }

    public void OutText()
    {
        if (player.isLocalPlayer)
            FindObjectOfType<OutPlayerInfo>().AmmoInfo(currentAmmo, M_Player.automaticGun_AllAmmo);
    }

    [ClientRpc]
    private void RpcFlashShoot()
    {
        if (gameObject.activeSelf)
        {
            shotPoint.GetComponent<Light2D>().intensity = 10;
            Invoke("offLight", 0.05f);
        }
    }
    [Command(requiresAuthority = false)]
    private void CmdFlashShoot()
    {
        RpcFlashShoot();
    }
    public void FlashShoot()
    {
        if (player.isServer)
            RpcFlashShoot();
        else if (player.isClient)
            CmdFlashShoot();
    }
    public void offLight()
    {
        shotPoint.GetComponent<Light2D>().intensity = 0;
    }






    [ClientRpc]
    private void RpcChangeTheValueAmmo(int value)
    {
        currentAmmo = value;
    }
    [Command(requiresAuthority = false)]
    private void CmdChangeTheValueAmmo(int value)
    {
        RpcChangeTheValueAmmo(value);
    }
    private void ChangeTheValueAmmo(int value)
    {
        if (player.isServer)
            RpcChangeTheValueAmmo(value);
        else if (player.isClient)
            CmdChangeTheValueAmmo(value);
    }

}
