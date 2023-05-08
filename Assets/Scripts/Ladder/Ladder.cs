using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ladder : MonoBehaviour
{


    float speed = 5;

    bool tf = false;

    public Transform XYZ;


    public bool isTrigger = false;

    public bool touchedLadder = false;

    public void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.W) && (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) || touchedLadder) && !Input.GetKey(KeyCode.Space))
            {
                //other.GetComponent<Player>().anim.SetBool("StairsOn", true);
                other.GetComponent<Player>().LadderUpSound.volume = 1.1f;
                other.GetComponent<SpriteRenderer>().sortingOrder = 7;


                tf = true;
                other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;


                other.GetComponent<Rigidbody2D>().gravityScale = 0;
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
                other.GetComponent<BoxCollider2D>().isTrigger = true;
                other.GetComponent<Transform>().position = new Vector3(XYZ.position.x, other.transform.position.y, other.transform.position.z);
                isTrigger = true;
            }
            else if (Input.GetKey(KeyCode.S) && (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) || touchedLadder) && !Input.GetKey(KeyCode.Space))
            {
                //ther.GetComponent<Player>().anim.SetBool("StairsOn", true);
                other.GetComponent<Player>().LadderUpSound.volume = 1.1f;

                other.GetComponent<SpriteRenderer>().sortingOrder = 7;

                tf = true;
                other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

                other.GetComponent<Rigidbody2D>().gravityScale = 0;
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
                other.GetComponent<BoxCollider2D>().isTrigger = true;
                other.GetComponent<Transform>().position = new Vector3(XYZ.position.x, other.transform.position.y, other.transform.position.z);
                isTrigger = true;
            }
            else if (other.tag == "Player" && tf == true)
            {
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                tf = false;
                other.GetComponent<Player>().LadderUpSound.volume = 0;
            }


            if (Input.GetKey(KeyCode.LeftShift) && other.gameObject.GetComponent<Player>().stamine.value > 10)
                speed = 3f;
            else        
                speed = 2f;
            
            if (other.GetComponent<Player>().anim.GetBool("touched_ground") && !LadderUP.upTriger)
            {
                isTrigger = false;

                other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;


                other.GetComponent<Rigidbody2D>().gravityScale = 1;
                other.GetComponent<BoxCollider2D>().isTrigger = false;

                other.GetComponent<SpriteRenderer>().sortingOrder = 9;
            }


            if (Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && isTrigger)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    //other.GetComponent<Player>().anim.SetBool("StairsOn", false);
                    other.GetComponent<Player>().anim.Play("Jump");
                    other.gameObject.transform.Find("weapon_hands").gameObject.SetActive(true);

                    isTrigger = false;

                    other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;


                    other.GetComponent<Rigidbody2D>().gravityScale = 1;
                    other.GetComponent<BoxCollider2D>().isTrigger = false;

                    other.GetComponent<SpriteRenderer>().sortingOrder = 9;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.GetComponent<Player>().anim.SetBool("StairsOn", false);

            isTrigger = false;

            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;


            other.GetComponent<Rigidbody2D>().gravityScale = 1;
            other.GetComponent<BoxCollider2D>().isTrigger = false;

            other.GetComponent<SpriteRenderer>().sortingOrder = 9;

            other.GetComponent<Player>().LadderUpSound.volume = 0;
        }
    }
}
