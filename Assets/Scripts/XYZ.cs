using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYZ : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            FindObjectOfType<Ladder>().touchedLadder = true;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                collision.gameObject.GetComponent<Player>().anim.SetBool("touched_ground", false);

                collision.gameObject.GetComponent<Player>().anim.Play("ladder_up");
                collision.gameObject.GetComponent<Player>().anim.SetBool("ladder_up", true);

                collision.gameObject.transform.Find("arm").gameObject.SetActive(false);
                collision.gameObject.transform.Find("weapon_hands").gameObject.SetActive(false);
            }
            else if ((!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S)) && collision.gameObject.GetComponent<Player>().anim.GetBool("ladder_up"))
            {
                collision.gameObject.GetComponent<Player>().anim.SetBool("ladder_up", false);
            }
        }

    }




    private void OnTriggerExit2D(Collider2D collision)
    {
        FindObjectOfType<Ladder>().touchedLadder = false;

        if (collision.gameObject != null && collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().anim.SetBool("touched_ground", true);
            collision.gameObject.GetComponent<Player>().anim.SetBool("ladder_up", false);

            collision.gameObject.transform.Find("arm").gameObject.SetActive(true);
            collision.gameObject.transform.Find("weapon_hands").gameObject.SetActive(true);
        }
    }

}
