using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class unamed : MonoBehaviour
{

    public Text ammoCount;
    [SerializeField] private WeaponSwitch ws;

    private void Start()
    {
        ws = FindObjectOfType<WeaponSwitch>();
    }
    void Update()
    {
        /*
        if (ws.weaponSwitch == 0)
        {
            ammoCount.text = 0 + "/" + 0;
        }*/
        ammoCount.text = 0 + "/" + 0;
    }
}
