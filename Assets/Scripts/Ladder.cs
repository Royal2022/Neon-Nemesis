using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.UIElements;

public class Ladder : MonoBehaviour
{


    float speed = 5;

    bool tf = false;

    public Transform XYZ;

    public GameObject player;

    public bool isTrigger = false;

    public bool touchedLadder = false;


    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))    
        {
            m = true;
            Debug.Log(m);
        }
        else
        {
            m = false;
            Debug.Log(m);
        }
    }*/


    public void OnTriggerStay2D(Collider2D other)
    {



        if (other.gameObject.CompareTag("Player"))
        {
            //Player.anim.SetBool("player_jump", false);

            if (Input.GetKey(KeyCode.W) && (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) || touchedLadder))
            {
                other.GetComponent<SpriteRenderer>().sortingOrder = 7;

                //FindObjectOfType<Player>().StaminFunc();
                //Player.anim.SetBool("ladder_up", true);
                tf = true;
                other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;


                other.GetComponent<Rigidbody2D>().gravityScale = 0;
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
                other.GetComponent<CapsuleCollider2D>().isTrigger = true;
                other.GetComponent<Transform>().position = new Vector3(XYZ.position.x, other.transform.position.y, other.transform.position.z);
                isTrigger = true;
            }
            else if (Input.GetKey(KeyCode.S) && (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) || touchedLadder))
            {
                other.GetComponent<SpriteRenderer>().sortingOrder = 7;

                //FindObjectOfType<Player>().StaminFunc();
                tf = true;
                other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;



                //Player.anim.SetBool("ladder_up", true);

                other.GetComponent<Rigidbody2D>().gravityScale = 0;
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
                other.GetComponent<CapsuleCollider2D>().isTrigger = true;
                other.GetComponent<Transform>().position = new Vector3(XYZ.position.x, other.transform.position.y, other.transform.position.z);
                isTrigger=true;
            }
            else if (other.tag == "Player" && tf == true)
            {

                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                //Player.anim.SetBool("ladder_up", false);
                other.GetComponent<Transform>().position = new Vector3(XYZ.position.x, other.transform.position.y, other.transform.position.z);


                tf = false;
            }


        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = false;

            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;


            //Player.anim.SetBool("ladder_up", false);

            player.GetComponent<Rigidbody2D>().gravityScale = 1;
            player.GetComponent<CapsuleCollider2D>().isTrigger = false;

            other.GetComponent<SpriteRenderer>().sortingOrder = 9;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && FindObjectOfType<Player>().stamine.value > 10)
        {
            speed = 3f;
        }
        else
        {
            speed = 2f;
        }


    }





}
