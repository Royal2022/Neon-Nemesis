using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class M_Pistol : NetworkBehaviour
{
    public float offset;
    [SerializeField] private GameObject bullet;
    public Transform shotPoint;


    private float timeBtwShots;
    public float startTimeBtwShots;

    public static SpriteRenderer sr;


    // ======================== Ammo ================================

    public GameObject ammo;

    [SyncVar]
    public int currentAmmo = 15;

    public int full = 45;

    [SerializeField] private WeaponSwitch ws;

    // ==============================================================

    [SerializeField] private OutPlayerInfo AmmoDisplay;
    private M_Player player;
    public Animator anim;


    public AudioSource[] AllSound;

    public AudioSource GetGunSound;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ws = FindObjectOfType<WeaponSwitch>();
    }


    private void Update()
    {
        if (gameObject.transform.parent != null)
        {
            SoundPlaySetActive(true);

            player = transform.root.gameObject.GetComponent<M_Player>();

            anim = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.GetComponent<Animator>();


            if (gameObject.transform.parent)
            {
                OutText();
            }

            if (timeBtwShots <= 0 && currentAmmo > 0 && !anim.GetBool("reloadPistol"))
            {
                if (Input.GetMouseButtonDown(0) && gameObject.transform.parent != null && player.isLocalPlayer/*&& transform.root.gameObject.GetComponent<M_Player>().IsItYou*/)
                {
                    JointSoundPlay(1);
                    anim.Play("PistolShot");
                    player.CallSpawnBullet(player.netId, shotPoint.position, transform.rotation);
                    
                    OutText();
                    timeBtwShots = startTimeBtwShots;
                    //currentAmmo -= 1;
                    ChangeTheValueAmmo(currentAmmo -= 1);
                    FlashShoot();
                }
                else
                {
                    //anim.SetBool("M_fire", false);
                    //if (player.isLocalPlayer)
                    //    hands.AnimOnOff("M_fire", false);

                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }


            // ======================== Ammo ================================
            if (!player.isLocalPlayer) return;
            if (Input.GetKeyDown(KeyCode.R) && M_Player.pistol_AllAmmo > 0 && currentAmmo < 15)
            {
                anim.SetBool("reloadPistol", true);
                JointSoundPlay(0);
            }
        }
        else
        {
            SoundPlaySetActive(false);
        }
    }



    /*=================== Звуки ====================*/
    [ClientRpc]
    private void RpcJointSoundPlay(int index)
    {
        AllSound[index].Play();
    }
    [Command(requiresAuthority = false)]
    void CmdJointSoundPlay(int index)
    {
        RpcJointSoundPlay(index);
    }
    public void JointSoundPlay(int index)
    {
        if (isServer)
            RpcJointSoundPlay(index);
        else if (isClient)
            CmdJointSoundPlay(index);
    }

    [ClientRpc]
    private void RpcSoundPlaySetActive(bool index)
    {
        GetGunSound.enabled = index;
    }
    [Command(requiresAuthority = false)]
    void CmdSoundPlaySetActive(bool index)
    {
        RpcSoundPlaySetActive(index);
    }
    public void SoundPlaySetActive(bool index)
    {
        if (isServer)
            RpcSoundPlaySetActive(index);
        else if (isClient)
            CmdSoundPlaySetActive(index);
    }
    /*==============================================*/



    public void Reload()
    {
        if (gameObject.transform.parent != null)
        {
            int reason = 15 - currentAmmo;
            if (M_Player.pistol_AllAmmo >= reason)
            {
                M_Player.pistol_AllAmmo -= reason;
                //currentAmmo = 15;
                ChangeTheValueAmmo(15);
            }
            else
            {
                //currentAmmo = currentAmmo + M_Player.pistol_AllAmmo;
                ChangeTheValueAmmo(currentAmmo + M_Player.pistol_AllAmmo);
                M_Player.pistol_AllAmmo = 0;
            }
        }
    }


    public void OutText()
    {
        if (player.isLocalPlayer)
          FindObjectOfType<OutPlayerInfo>().AmmoInfo(currentAmmo, M_Player.pistol_AllAmmo);
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
