using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class M_WeaponSwitch : MonoBehaviourPunCallbacks
{
    [SerializeField] private M_hands Hands;
    //[SerializeField] private XYZ xyz;
    [SerializeField] private M_Player player;

    public GameObject arm;

    [PunRPC] public int weaponSwitch = 0;

    
    public int weaponOpened = 1;
    //public bool akPickeUp = false;

    //public RuntimeAnimatorController nogunanim;
    //public RuntimeAnimatorController gunanim;

    //public Text ammoCount;




    void Start()
    {
        Hands = FindObjectOfType<M_hands>();
        if (!photonView.IsMine) return;
        gameObject.GetPhotonView().RPC("SelectWeapon", RpcTarget.AllBuffered, weaponSwitch);
        //SelectWeapon();
    }

    public void Update()
    {
        int currentWeapon = weaponSwitch;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (weaponSwitch >= transform.childCount - weaponOpened)
            {
                weaponSwitch = 0;
            }
            else
            {
                weaponSwitch++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (weaponSwitch <= 0)
            {
                weaponSwitch = transform.childCount - weaponOpened;
            }
            else
            {
                weaponSwitch--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {   
            weaponSwitch = 0;
            //Hands.res();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            weaponSwitch = 1;
            //Hands.res();
        }

        if (currentWeapon != weaponSwitch)
        {
            //SelectWeapon();
            if (!photonView.IsMine) return;
            gameObject.GetPhotonView().RPC("SelectWeapon", RpcTarget.AllBuffered, weaponSwitch);
        }


        if (!photonView.IsMine) return;
        if (weaponSwitch == 0)
        {
            //xyz = FindObjectOfType<XYZ>();
            //xyz.arm.SetActive(false);
            //arm.SetActive(true);
            gameObject.GetPhotonView().RPC("armTF", RpcTarget.AllBuffered, true);
        }
        else if (weaponSwitch != 0)
        {
            //xyz = FindObjectOfType<XYZ>();
            //xyz.arm.SetActive(true);
            //arm.SetActive(false);
            gameObject.GetPhotonView().RPC("armTF", RpcTarget.AllBuffered, false);
        }

        //FindObjectOfType<Player>().GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Player");



        //if (weaponSwitch == 0)
        //{
        //    FindObjectOfType<Player>().GetComponent<Animator>().runtimeAnimatorController = nogunanim;
        //    FindObjectOfType<Player>().GetComponent<Animator>().applyRootMotion = false;
        //}
        //else if(weaponSwitch != 0)
        //{
        //    FindObjectOfType<Player>().GetComponent<Animator>().runtimeAnimatorController = gunanim;
        //    FindObjectOfType<Player>().GetComponent<Animator>().applyRootMotion = true;
        //}

    }


    [PunRPC]
    public void SelectWeapon(int a)
    {
        //Hands.res();
        int i = 0;
        foreach (Transform weapon in transform)
        {
            Debug.Log(weapon.gameObject);
            if (i == a)
            {     
             //weapon.gameObject.SetActive(true);
                PhotonView.Find(weapon.gameObject.GetPhotonView().ViewID).gameObject.SetActive(true);
            }
            else
            {
             //weapon.gameObject.SetActive(false);
                PhotonView.Find(weapon.gameObject.GetPhotonView().ViewID).gameObject.SetActive(false);
            }
            i++;
        }     
    }

    [PunRPC]
    public void armTF(bool tf)
    {
        PhotonView.Find(arm.gameObject.GetPhotonView().ViewID).gameObject.SetActive(tf);
    }

}
