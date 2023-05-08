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
    public RuntimeAnimatorController pistolanim;

    public Animator PlayerAnim;

    public Text ammoCount;

    void Start()
    {
        Hands = FindObjectOfType<hands>();

        SelectWeapon();
    }

    public void Update()
    {
        int currentWeapon = weaponSwitch;

        
        if (!PlayerAnim.GetCurrentAnimatorStateInfo(0).IsName("sault") || !PlayerAnim.GetBool("Sault"))
        {
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
        }

        


        if (currentWeapon != weaponSwitch)
        {
            SelectWeapon();
        }



        //FindObjectOfType<Player>().GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Player");


        if (weaponSwitch == 0)
        {
            PlayerAnim.SetFloat("Blend", 0);
            OutText();
            //player.GetComponent<Animator>().runtimeAnimatorController = nogunanim;
            //player.GetComponent<Animator>().applyRootMotion = false;
        }
        else if (weaponSwitch == 1)
        {
            PlayerAnim.SetFloat("Blend", 1);
            //player.GetComponent<Animator>().runtimeAnimatorController = pistolanim;
            //player.GetComponent<Animator>().applyRootMotion = true;
        }
        else if(weaponSwitch == 2)
        {
            PlayerAnim.SetFloat("Blend", 2);
            //player.GetComponent<Animator>().runtimeAnimatorController = gunanim;
            //player.GetComponent<Animator>().applyRootMotion = true;
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

    public void OutText()
    {
        ammoCount.text = 0 + "/" + 0;
    }
}
