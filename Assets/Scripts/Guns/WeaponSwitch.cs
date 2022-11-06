using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int weaponSwitch = 0;

    
    public int weaponOpened = 2;
    public bool akPickeUp = false;


    void Start()
    {
        SelectWeapon();

    }

    void Update()
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
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            weaponSwitch = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && akPickeUp == true)
        {
            weaponSwitch = 2;
        }

        if (currentWeapon != weaponSwitch)
        {
            SelectWeapon();
        }
    }

    [SerializeField] private hands Hands;

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == weaponSwitch)
            {            
             weapon.gameObject.SetActive(true); 
            }
            else
            {
             weapon.gameObject.SetActive(false);
            }
            i++;
        }
        //hands.tf = true;

        Hands = FindObjectOfType<hands>();
        Hands.res();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AK")
        {
            weaponOpened -= 1;
            akPickeUp = true;
            Destroy(collision.gameObject);
        }
    }
}
