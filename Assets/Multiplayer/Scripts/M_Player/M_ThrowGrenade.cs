using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class M_ThrowGrenade : NetworkBehaviour
{
    public int NumberOfGrenades = 3;

    /*=========Grenade==========*/
    public GameObject Grenade;
    [SyncVar]
    private float PowerThrow = 5;
    public Transform SpawnGrenade;
    public GameObject WeaponHands;
    public GameObject HandPoint;

    private float timeDropGranade;
    public float startTimeDropGranade = 2f;

    //public Image TimerImage;
    /*==========================*/


    private M_Player player;

    private OutPlayerInfo OutGranadeInfo;

    void Start()
    {
        player = GetComponent<M_Player>();
        OutGranadeInfo = FindObjectOfType<OutPlayerInfo>();
        OutGranadeInfo.GranadeText.text = NumberOfGrenades.ToString();
    }

    void Update()
    {
        /*=========Grenade==========*/
        if (!isLocalPlayer) return;
        if (timeDropGranade <= 0 && NumberOfGrenades > 0)
        {
            if (Input.GetKeyDown(KeyCode.G) && player.isGround && !PlayingOrNotAnim("dropGrenade") && !player.anim.GetBool("throwGrenade")
            && !PlayingOrNotAnim("Run")
            && !PlayingOrNotAnim("jump") && !PlayingOrNotAnim("sault")
            && !PlayingOrNotAnim("run_attack") && !PlayingOrNotAnim("attack") && !player.anim.GetBool("player_jump"))
            {
                PreparationThrowGrenade(false);
            }
            if (Input.GetKeyUp(KeyCode.G) && PlayingOrNotAnim("idle_dropGrenade")
                && !PlayingOrNotAnim("Run")
                && !PlayingOrNotAnim("jump") && !PlayingOrNotAnim("sault")
                && !PlayingOrNotAnim("run_attack") && !PlayingOrNotAnim("attack") && !player.anim.GetBool("player_jump"))
            {
                PreparationThrowGrenade(true);
                timeDropGranade = startTimeDropGranade;
            }

            if (Input.GetKey(KeyCode.G) && PlayingOrNotAnim("idle_dropGrenade"))
            {
                PowerThroW();
            }
        }
        else
        {
            timeDropGranade -= Time.deltaTime;
            var normalizedValue = Mathf.Clamp(timeDropGranade / startTimeDropGranade, 0.0f, 1.0f);
            OutGranadeInfo.GranadeTimerImage.fillAmount = normalizedValue;
        }
        /*==========================*/
    }


    [ClientRpc]
    private void RpcPowerThrow()
    {
        PowerThrow += 2 * Time.deltaTime;
        if (PowerThrow > 10)
            PowerThrow = 10;
    }
    [Command]
    private void CmdPowerThrow()
    {
        RpcPowerThrow();
    }
    private void PowerThroW()
    {
        if (isServer)
            RpcPowerThrow();
        else if (isClient)
            CmdPowerThrow();
    }


    [ClientRpc]
    private void RpcPreparationThrowGrenade(bool T_F)
    {
        if (!T_F)
        {
            player.anim.SetTrigger("throwGrenadeTrigger");
            player.anim.Play("idle_dropGrenade");
            WeaponHands.SetActive(false);
            HandPoint.transform.GetChild(0).gameObject.SetActive(true);
            player.rb.velocity = new Vector2(0, 0);
        }
        else
            player.anim.SetBool("throwGrenade", true);

    }
    [Command]
    private void CmdPreparationThrowGrenade(bool T_F)
    {
        RpcPreparationThrowGrenade(T_F);
    }
    public void PreparationThrowGrenade(bool T_F)
    {
        if (isServer)
            RpcPreparationThrowGrenade(T_F);
        else if (isClient)
            CmdPreparationThrowGrenade(T_F);
    }


    public bool PlayingOrNotAnim(string name)
    {
        return player.anim.GetCurrentAnimatorStateInfo(0).IsName($"{name}");
    }

    [ClientRpc]
    private void RpcWeaponHandsDisabled()
    {
        WeaponHands.SetActive(false);
        HandPoint.transform.GetChild(0).gameObject.SetActive(false);
        //HandPoint.SetActive(false);
    }
    [Command]
    private void CmdWeaponHandsDisable()
    {
        RpcWeaponHandsDisabled();
    }
    public void WeaponHandsDisable()
    {
        if (isServer)
            RpcWeaponHandsDisabled();
        else if (isClient)
            CmdWeaponHandsDisable();
    }


    /*=========Grenade==========*/
    [Server]
    private void SpawnGranade()
    {
        GameObject granade = Instantiate(Grenade, SpawnGrenade.position, transform.rotation);
        NetworkServer.Spawn(granade);
        granade.GetComponent<M_grenade>().powerThrow = PowerThrow;
        granade.GetComponent<M_grenade>().MyStart(GetComponent<M_Player>().facingRight);
    }
    [Command]
    private void CmdSpawnGranade()
    {
        SpawnGranade();
    }
    private void SpawnGranadde()
    {
        if (isServer)
            SpawnGranade();
        else if (isClient)
            CmdSpawnGranade();
    }



    [ClientRpc]
    private void RpcThrow_Grenade()
    {
        PowerThrow = 5;
        player.anim.ResetTrigger("throwGrenadeTrigger");
        player.anim.SetBool("throwGrenade", false);
        WeaponHands.SetActive(true);
        HandPoint.transform.GetChild(0).gameObject.SetActive(false);
    }
    [Command]
    private void CmdThrow_Grenade()
    {
        RpcThrow_Grenade();
    }
    private void Throw_Grenade()
    {
        if (isServer)
            RpcThrow_Grenade();
        else if (isClient)
            CmdThrow_Grenade();
    }


    public void ThrowAndSpawnGranade()
    {
        Throw_Grenade();
        if (!isLocalPlayer) return;
        SpawnGranadde();
        NumberOfGrenades -= 1;
        OutGranadeInfo.GranadeText.text = NumberOfGrenades.ToString();
    }


    /*==========================*/
}
