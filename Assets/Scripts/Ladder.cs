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

    //public GameObject player;

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
            if (Input.GetKey(KeyCode.W) && (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) || touchedLadder))
            {
                other.GetComponent<SpriteRenderer>().sortingOrder = 7;


                tf = true;
                other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;


                other.GetComponent<Rigidbody2D>().gravityScale = 0;
                other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
                other.GetComponent<BoxCollider2D>().isTrigger = true;
                other.GetComponent<Transform>().position = new Vector3(XYZ.position.x, other.transform.position.y, other.transform.position.z);
                isTrigger = true;
            }
            else if (Input.GetKey(KeyCode.S) && (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) || touchedLadder))
            {
                other.GetComponent<SpriteRenderer>().sortingOrder = 7;

                tf = true;
                other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;



                //Player.anim.SetBool("ladder_up", true);

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
            }


            if (Input.GetKey(KeyCode.LeftShift) && other.gameObject.GetComponent<Player>().stamine.value > 10)
            {
                speed = 3f;
            }
            else
            {
                speed = 2f;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTrigger = false;

            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;


            other.GetComponent<Rigidbody2D>().gravityScale = 1;
            other.GetComponent<BoxCollider2D>().isTrigger = false;

            other.GetComponent<SpriteRenderer>().sortingOrder = 9;
        }
    }
}
