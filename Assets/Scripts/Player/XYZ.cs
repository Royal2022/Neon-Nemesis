using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYZ : MonoBehaviour
{
    [SerializeField] private WeaponSwitch ws;
    [SerializeField] private Player player;

    //public GameObject weaponswitch;
    public GameObject arm;

    public void Start()
    {
        ws = FindObjectOfType<WeaponSwitch>();
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        player.anim.SetBool("touched_ground", false);

        if (collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                player.anim.Play("ladder_up");
                player.anim.SetBool("ladder_up", false);
                ws.gameObject.SetActive(false);
                arm.SetActive(false);
            }
            else if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S))
            {
                player.anim.SetBool("ladder_up", true);
            }
        }

    }




    private void OnTriggerExit2D(Collider2D collision)
    {

        player.anim.SetBool("touched_ground", true);
        player.anim.SetBool("ladder_up", false);

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
        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("ladder_up") == false && player.anim.GetCurrentAnimatorStateInfo(0).IsName("stop_ladder") == false)
        {
            ws.gameObject.SetActive(true);
            arm.SetActive(true);

            player.anim.SetBool("touched_ground", true);
        }
    }
}
