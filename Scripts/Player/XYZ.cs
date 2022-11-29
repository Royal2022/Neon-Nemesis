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
            else if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S))
            {
                Player.anim.SetBool("ladder_up", true);
            }
        }

    }




    private void OnTriggerExit2D(Collider2D collision)
    {

        Player.anim.SetBool("touched_ground", true);
        Player.anim.SetBool("ladder_up", false);

        /*
        if (ws.gameObject.activeSelf == false)
        {
            //ws.gameObject.SetActive(true);
        }*/
        /*
        if (ws.weaponSwitch != 3)
        {
            arm.SetActive(true);
        }*/
        /*
        if (Player.anim.GetBool("touched_ground") == true && Player.anim.GetCurrentAnimatorStateInfo(0).IsName("ladder_up") == false)
        {
            ws.gameObject.SetActive(true);
            Debug.Log(1);
        }*/

    }
    private void Update()
    {
        if (Player.anim.GetCurrentAnimatorStateInfo(0).IsName("ladder_up") == false && Player.anim.GetCurrentAnimatorStateInfo(0).IsName("stop_ladder") == false)
        {
            ws.gameObject.SetActive(true);
            arm.SetActive(true);
            
            Player.anim.SetBool("touched_ground", true);
        }
    }
}
