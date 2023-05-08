using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class M_WeaponSwitch : NetworkBehaviour
{
    public GameObject WSwitch;
    //public GameObject arm;

    [SyncVar(hook = nameof(SyncWeaponSwitch))]
    public int weaponSwitch = 0;

    public int weaponOpened = 1;

    public GameObject M_Player;


    public RuntimeAnimatorController pistolanim;
    public RuntimeAnimatorController gunanim;


    public void Start()
    {
        SetActive(weaponSwitch);
    }

    public void Update()
    {
        int currentWeapon = weaponSwitch;
        if (!isLocalPlayer) return;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (weaponSwitch >= WSwitch.transform.childCount - weaponOpened)
            {
                SetActive(0);
            }
            else
            {
                SetActive(weaponSwitch++);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (weaponSwitch <= 0)
            {
                SetActive(weaponSwitch = WSwitch.transform.childCount - weaponOpened);

            }
            else
            {
                SetActive(weaponSwitch--);
            }
        }

        if (!isLocalPlayer) return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActive(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            SetActive(1);
        }

        if (currentWeapon != weaponSwitch)
        {
            SetActive(weaponSwitch);
        }


    }



    private void SyncWeaponSwitch(int oldnum, int newnum)
    {
        weaponSwitch = newnum;
    }




    [ClientRpc]
    public void RpcSetActive(int a)
    {
        if (a == 0)
        {
            //M_Player.GetComponent<Animator>().runtimeAnimatorController = pistolanim;
            //M_Player.GetComponent<Animator>().applyRootMotion = true;
            M_Player.GetComponent<Animator>().SetInteger("PitolOrAutomaticGun", 0);
        }
        else if (a == 1)
        {
            //M_Player.GetComponent<Animator>().runtimeAnimatorController = gunanim;
            //M_Player.GetComponent<Animator>().applyRootMotion = true;
            M_Player.GetComponent<Animator>().SetInteger("PitolOrAutomaticGun", 1);
        }



        if (WSwitch != null)
        {
            int i = 0;
            foreach (Transform weapon in WSwitch.transform)
            {
                if (i == a)
                {
                    weapon.gameObject.SetActive(true);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
                i++;
            }
        }
    }

    public void SetActive(int a)
    {
        if (isServer)
        {
            RpcSetActive(a);
        }
        else if (isClient)
        {
            CmdSetActive(a);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdSetActive(int a)
    {
        RpcSetActive(a);
    }
}
