using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYZ : MonoBehaviour
{
    public GameObject weaponswitch;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player.anim.SetBool("touched_ground", false);
        

        if (collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                Player.anim.Play("ladder_up");
                Player.anim.SetBool("ladder_up", false);
                weaponswitch.SetActive(false);
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

        if (weaponswitch.gameObject.activeSelf == false)
        {
            weaponswitch.SetActive(true);
        }
    }
}
