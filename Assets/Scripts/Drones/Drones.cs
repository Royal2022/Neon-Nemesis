using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Drones : MonoBehaviour
{
    public int healthDrons = 10;
    public float SpeedDrons;
    private float speedDrons;


    public Animator anim;

    public float DronsDistanceStop;
    private bool facingRight = true;

    public Transform playerPosition;

    Vector3 OldPosition;

    private bool delivered;

    bool ThrowOne;


    void Start()
    {
        speedDrons = SpeedDrons;
        playerPosition = FindObjectOfType<Player>().GetComponent<Transform>();

        OldPosition = playerPosition.position;
    }

    void Update()
    {
        if (playerPosition.position.x > transform.position.x && facingRight && !delivered)
        {
            Flip();
            facingRight = false;
        }
        else if (playerPosition.position.x < transform.position.x && !facingRight && !delivered)
        {
            Flip();
            facingRight = true;
        }


        if (!delivered)
        {
            if ((Vector2.Distance(transform.position, playerPosition.position) > DronsDistanceStop))
            {
                speedDrons = SpeedDrons;
                Vector2.Distance(transform.position, playerPosition.position);
            }
            else if (Vector2.Distance(transform.position, playerPosition.position) <= DronsDistanceStop)
            {
                speedDrons = 0;

                if (OldPosition == playerPosition.position)
                {
                    if (!ThrowOne)
                    {
                        Invoke("Throw", 1);
                        ThrowOne = true;
                    }
                }
                else
                {
                    CancelInvoke("Throw");
                    ThrowOne = false;
                }
                
                OldPosition = playerPosition.position;
            }
        }

        if (speedDrons != 0)
        {
            anim.SetBool("run", true);
        }
        else
            anim.SetBool("run", false);


    }

    private void FixedUpdate()
    {
        if (!delivered)
        {
            Vector2 playerXposition = new Vector2(playerPosition.position.x, 0);
            transform.position = Vector2.MoveTowards(transform.position, playerXposition, speedDrons * Time.fixedDeltaTime);
        }
        else if (delivered)
        {
            transform.Translate(Vector2.left * -1 * SpeedDrons * Time.deltaTime);

            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }


    bool death;

    private void Throw()
    {
        Debug.Log("Бросить");
        delivered = true;
        speedDrons = 4;

        death = true;
        //Invoke("Death", 2);
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 Scaler = gameObject.transform.localScale;
        Scaler.x *= -1;
        gameObject.transform.localScale = Scaler;
    }



    public void Death()
    {
        Destroy(gameObject);
    }
    void OnBecameInvisible()
    {
        if (death)
            Death();
    }
}
