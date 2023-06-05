using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private Player player;

    public int weaponSwitch = 0;

    public Animator PlayerAnim;

    public Text ammoCount;

    public bool[] handIsNotEmpty = new bool[] { true, true, true };

    void Start()
    {
        SelectWeapon(0);
    }

    public void Update()
    {
        if (Time.timeScale != 0)
        {
            if (!player.PlayingOrNotAnim("sault"))
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    SelectWeapon(SelectWeaponNext((weaponSwitch + 1 == 3) ? 0 : weaponSwitch + 1));
                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    SelectWeapon(SelectWeaponBack((weaponSwitch - 1 == -1) ? 2 : weaponSwitch - 1));
                }


                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    SelectWeapon(0);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    SelectWeapon(1);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    SelectWeapon(2);
                }

            }
        }
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


    public void SelectWeapon(int a)
    {
        int k = (a + 1 == 3) ? 0 : a + 1;

        int d = (a - 1 == -1) ? 2 : a - 1;

        if (((handIsNotEmpty[k] || handIsNotEmpty[d]) && handIsNotEmpty[a]) || (!handIsNotEmpty[k] && !handIsNotEmpty[d] && handIsNotEmpty[a]))
        {
            for (int i = 0; i <= transform.childCount - 1; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                if (i == a)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                    PlayerAnim.SetFloat("Blend", i);
                    weaponSwitch = i;
                }
            }
        }
    }

    public void OutText()
    {
        ammoCount.text = 0 + "/" + 0;
    }
}
