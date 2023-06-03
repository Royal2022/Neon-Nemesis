using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using Unity.VisualScripting;

public class M_WeaponSwitch : NetworkBehaviour
{
    public GameObject WSwitch;

    [SyncVar(hook = nameof(SyncWeaponSwitch))]
    public int weaponSwitch = 0;

    public int weaponOpened = 1;

    public M_Player player;
    public Animator playerAnim;

    public bool[] handIsNotEmpty = new bool[] { true, false, false};

    public void Start()
    {
        SetActive(weaponSwitch);
    }

    public void Update()
    {
        int currentWeapon = weaponSwitch;
        if (!isLocalPlayer) return;
        if (!player.PlayingOrNotAnim("sault"))
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                SetActive(SelectWeaponNext((weaponSwitch + 1 == 3) ? 0 : weaponSwitch + 1));
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                SetActive(SelectWeaponBack((weaponSwitch - 1 == -1) ? 2 : weaponSwitch - 1));
            }

            if (!isLocalPlayer) return;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetActive(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && WSwitch.transform.childCount >= 2)
            {
                SetActive(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetActive(2);
            }
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



    private int SelectWeaponNext(int a)
    {
        if (handIsNotEmpty.Count(b => b) > 1)
        {
            for (int i = 0; i <= transform.childCount - 1; i++)
            {
                if (i == a && handIsNotEmpty[a])
                {
                    weaponSwitch = i;
                    return weaponSwitch;
                }
                else if (i == a && !handIsNotEmpty[a])
                {
                    weaponSwitch = (a + 1 == 3) ? 0 : a + 1;
                    return weaponSwitch;
                }
            }
            return 0;
        }
        else return 0;
    }
    private int SelectWeaponBack(int a)
    {
        if (handIsNotEmpty.Count(b => b) > 1)
        {
            for (int i = 0; i <= transform.childCount - 1; i++)
            {
                if (i == a && handIsNotEmpty[a])
                {
                    weaponSwitch = i;
                    return weaponSwitch;
                }
                else if (i == a && !handIsNotEmpty[a])
                {
                    weaponSwitch = (a - 1 == -1) ? 2 : a - 1;
                    return weaponSwitch;
                }
            }
            return 0;
        }
        else return 0;
    }


    [ClientRpc]
    public void RpcSetActive(int a)
    {
        int k = (a + 1 == 3) ? 0 : a + 1;

        int d = (a - 1 == -1) ? 2 : a - 1;

        if (((handIsNotEmpty[k] || handIsNotEmpty[d]) && handIsNotEmpty[a]) || (!handIsNotEmpty[k] && !handIsNotEmpty[d] && handIsNotEmpty[a]))
        {
            for (int i = 0; i <= WSwitch.transform.childCount - 1; i++)
            {
                WSwitch.transform.GetChild(i).gameObject.SetActive(false);
                if (i == a)
                {
                    WSwitch.transform.GetChild(i).gameObject.SetActive(true);
                    playerAnim.SetFloat("Blend", i);
                    weaponSwitch = i;
                }
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
