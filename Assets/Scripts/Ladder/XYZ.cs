using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYZ : MonoBehaviour
{
    public Ladder ladder;

    private void Start()
    {
        ladder = transform.parent.GetComponent<Ladder>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ladder.touchedLadder = true;
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && !Input.GetKey(KeyCode.Space) && !LadderUP.upTriger)
            {
                collision.GetComponent<Player>().anim.SetBool("touched_ground", false);

                collision.GetComponent<Player>().anim.Play("ladder_up");
                collision.GetComponent<Player>().anim.SetBool("ladder_up", true);

                collision.gameObject.transform.Find("weapon_hands").gameObject.SetActive(false);
            }
            else if ((!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S)) && collision.GetComponent<Player>().anim.GetBool("ladder_up"))
            {
                collision.GetComponent<Player>().anim.SetBool("ladder_up", false);
            }
        }

    }




    private void OnTriggerExit2D(Collider2D collision)
    {
        ladder.touchedLadder = false;

        if (collision.gameObject != null && collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().anim.SetBool("touched_ground", true);
            collision.GetComponent<Player>().anim.SetBool("ladder_up", false);

            collision.gameObject.transform.Find("weapon_hands").gameObject.SetActive(true);
        }
    }

}
