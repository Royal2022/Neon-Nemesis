using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class M_unamed : MonoBehaviour
{

    //public Text ammoCount;
    [SerializeField] private M_WeaponSwitch ws;
    [SerializeField] private OutPlayerInfo AmmoDisplay;


    private void Start()
    {
        ws = FindObjectOfType<M_WeaponSwitch>();
    }
    void Update()
    {
        /*
        if (ws.weaponSwitch == 0)
        {
            ammoCount.text = 0 + "/" + 0;
        }*/
        //ammoCount.text = 0 + "/" + 0;
        AmmoDisplay.AmmoInfo(0, 0);

    }




}
