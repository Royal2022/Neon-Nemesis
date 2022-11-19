using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYZ : MonoBehaviour
{
    [SerializeField] private WeaponSwitch ws;

    //public GameObject weaponswitch;
    public GameObject arm;

    public void Start()
    {
        ws = FindObjectOfType<WeaponSwitch>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player.anim.SetBool("touched_ground", false);
        

        if (collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                Player.anim.Play("ladder_up");
                Player.anim.SetBool("ladder_up", false);
                ws.gameObject.SetActive(false);
                arm.SetActive(false);
            }
            else
            {
                Player.anim.SetBool("ladder_up", true);
            }
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {

        Player.anim.SetBool("touched_ground", true);
        Player.anim.SetBool("ladder_up", false);
        
        if (ws.gameObject.activeSelf == false)
        {
            ws.gameObject.SetActive(true);
        }
        if (ws.weaponSwitch != 3)
        {
            arm.SetActive(true);
        }




    }
}
