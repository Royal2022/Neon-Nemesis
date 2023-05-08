using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Drones : MonoBehaviour
{
    public int healthDrons = 10;
    public float SpeedDrons;
    private float speedDrons;


    public Animator anim;

    public float DronsDistanceStop;
    private bool facingRight = true;

    private Transform playerPosition;

    Vector3 OldPosition;

    private bool delivered;

    private bool ThrowOne;



    private Material matBlink;
    private Material matDefault;
    private SpriteRenderer spriteRend;

    void Start()
    {
        speedDrons = SpeedDrons;
        playerPosition = FindObjectOfType<Player>().GetComponent<Transform>();

        OldPosition = playerPosition.position;

        spriteRend = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("EnemyBlink1", typeof(Material)) as Material;
        matDefault = spriteRend.material;
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
                        Invoke("Throw", 0.6f);
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


        if (healthDrons <= 0)
        {
            anim.Play("death");
            speedDrons = 0;
            if (gameObject.transform.GetChild(0).childCount >= 1)
            {
                gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Rigidbody2D>().simulated = true;
                gameObject.transform.GetChild(0).transform.GetChild(0).parent = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!delivered)
        {
            Vector2 playerXposition = new Vector2(playerPosition.position.x, playerPosition.position.y + 2);
            transform.position = Vector2.MoveTowards(transform.position, playerXposition, speedDrons * Time.fixedDeltaTime);
        }
        else if (delivered)
        {
            transform.Translate(Vector2.left * -1 * SpeedDrons * Time.deltaTime);

            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }


    public void Product(GameObject product)
    {
        Instantiate(product, gameObject.transform.GetChild(0)).GetComponent<Rigidbody2D>().simulated = false;

        if (gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>())
            gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>().enabled = false;
    }

    private bool death;

    private void Throw()
    {
        if (gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>())
            gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>().enabled = true;

        gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Rigidbody2D>().simulated = true;
        gameObject.transform.GetChild(0).transform.GetChild(0).parent = null;
        delivered = true;
        speedDrons = 4;

        death = true;
        Invoke("OnBecameInvisible", 10);
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 Scaler = gameObject.transform.localScale;
        Scaler.x *= -1;
        gameObject.transform.localScale = Scaler;
    }

    public void TakeDamage(int damage)
    {
        healthDrons -= damage;

        spriteRend.material = matBlink;
        Invoke("ResetMaterial", 0.1f);
    }
    private void ResetMaterial()
    {
        spriteRend.material = matDefault;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
    void OnBecameInvisible()
    {
        if (death)
            anim.Play("death");
    }
}
