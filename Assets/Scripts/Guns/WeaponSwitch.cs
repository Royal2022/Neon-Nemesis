using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private hands Hands;
    [SerializeField] private Player player;

    public int weaponSwitch = 0;

    
    public int weaponOpened = 1;
    //public bool akPickeUp = false;

    public RuntimeAnimatorController nogunanim;
    public RuntimeAnimatorController gunanim;

    public Text ammoCount;


    void Start()
    {
        Hands = FindObjectOfType<hands>();

        SelectWeapon();
    }

    public void Update()
    {
        if (weaponSwitch == 1)
        {
            gameObject.transform.parent.gameObject.transform.Find("arm").gameObject.SetActive(true);
        }
        if (weaponSwitch != 1)
        {
            gameObject.transform.parent.gameObject.transform.Find("arm").gameObject.SetActive(false);
        }
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
            Hands.res();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            weaponSwitch = 1;
            Hands.res();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) /*&& akPickeUp == true*/)
        {
            weaponSwitch = 2;
            Hands.res();
        }


        if (currentWeapon != weaponSwitch)
        {
            SelectWeapon();
        }



        //FindObjectOfType<Player>().GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Player");



        if (weaponSwitch == 0)
        {
            FindObjectOfType<Player>().GetComponent<Animator>().runtimeAnimatorController = nogunanim;
            FindObjectOfType<Player>().GetComponent<Animator>().applyRootMotion = false;
        }
        else if(weaponSwitch != 0)
        {
            FindObjectOfType<Player>().GetComponent<Animator>().runtimeAnimatorController = gunanim;
            FindObjectOfType<Player>().GetComponent<Animator>().applyRootMotion = true;
        }


        //Debug.Log(weaponSwitch);
    }



    public void SelectWeapon()
    {
        Hands.res();
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

        
    }


        /*
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "AK")
            {
                weaponOpened -= 1;
                akPickeUp = true;
                Destroy(collision.gameObject);
            }
        }*/
    }
