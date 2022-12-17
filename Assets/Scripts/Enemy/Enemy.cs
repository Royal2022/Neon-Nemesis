using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;
    public float speed;
    public float distance = 6f;
    public float distanceBack = 1.5f;
    RaycastHit2D hit, hitBack, hitattack;
    public Rigidbody2D rb;
    bool facingRight = true;
    public bool playerNoticed = false;


    public bool hold = false;


    public bool isGrounded;
    public LayerMask whatIsGround;
    public float checkRaduis;
    public Transform feetPos;
    public float jumpForce;
    public LayerMask SeePlayer;
    


    private Transform target;


    //[SerializeField] private head Head;
    public GameObject Head;
    public GameObject fPE;
    public GameObject Patrol;

    public Animator anim;

    //[SerializeField] private feetPosEnemy fPE;

    public bool trigger = false;

    public bool triggerDeath = false;

    public int damege = 1;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        anim = GetComponent<Animator>();

        //Head = FindObjectOfType<head>();

        //fPE = FindObjectOfType<feetPosEnemy>();

    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            JumpEnemy();
        }

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRaduis, whatIsGround);


        if (!hold)
        {
            Physics2D.queriesStartInColliders = false;

            hit = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, distance, SeePlayer);
            if (hit.collider != null && hit.collider.gameObject.tag == "Player")
            {
                playerNoticed = true;
                hold =true;
                trigger = false;
            }
            else if (hit.collider == null && (!Head.gameObject.GetComponent<head>().playerNoticedHead && !fPE.gameObject.GetComponent<feetPosEnemy>().playerNoticedLegs))
            {
                playerNoticed = false;
            }           


            hitBack = Physics2D.Raycast(transform.position, Vector3.left * transform.localScale.x, distanceBack, SeePlayer);
            if (hitBack.collider != null && hitBack.collider.gameObject.tag == "Player")
            {
                Flip();
                hold = true;
            }

            /*
            hitattack = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, 2f, SeePlayer);
            if (hitattack.collider != null && hitattack.collider.gameObject.tag == "Player")
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                {
                    anim.SetBool("attack_enemy", true);
                    Debug.Log("Udar!");
                }
            }
            else if (hitattack.collider == null)
            {
                anim.SetBool("attack_enemy", false);
            }*/


        }
        else
        {
            hold = false;
        }
        Physics2D.queriesStartInColliders = true;




        if (health <= 0 && isGrounded)
        {
            anim.Play("death");
            triggerDeath = true;

            /*if (!anim.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                Destroy(gameObject);
            }*/
        }


        if (transform.localScale.x > 0 && !triggerDeath)
        {
            transform.Translate(Vector2.left * -1 * speed * Time.deltaTime);
        }
        else if (transform.localScale.x < 0 && !triggerDeath)
        {
            transform.Translate(Vector2.left * 1 * speed * Time.deltaTime);
        }



        if (speed > 0)
        {
            anim.SetBool("run_enemy", true);
        }
        else
        {
            anim.SetBool("run_enemy", false);
        }

        
        if (!playerNoticed && !Head.gameObject.GetComponent<head>().playerNoticedHead && !fPE.gameObject.GetComponent<feetPosEnemy>().playerNoticedLegs)
        {
            //trigger = false;
        }


        if (Mathf.Round(target.transform.position.x)  == Mathf.Round(transform.position.x) && trigger)
        {
            trigger = false;
        }

    }
    private void FixedUpdate()
    {
        
        if (playerNoticed || trigger)
        {
            if (target.position.x > transform.position.x && facingRight && trigger)
            {
                Flip();
                facingRight = false;
                //trigger = false;
            }
            else if (target.position.x < transform.position.x && !facingRight && trigger)
            {
                Flip();
                facingRight = true;
                //trigger = false;
            }


            
            if ((Vector2.Distance(transform.position, target.position) > 1.5f && !Patrol.gameObject.GetComponent<Patrol>().ground) && !triggerDeath)
            {
                //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime * 2);

                //Vector3 newPos = transform.position;
                //newPos.x = Mathf.MoveTowards(newPos.x, target.position.x, speed * Time.fixedDeltaTime);
                //transform.position = newPos;
                speed = 4;
                Vector2.Distance(transform.position, target.transform.position);
                anim.SetBool("attack_enemy", false);
            }
            else if(Vector2.Distance(transform.position, target.position) <= 1.5f && !triggerDeath) {
                speed = 0;
                trigger = false;
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attack_enemy"))
                {
                    anim.SetBool("attack_enemy", true);
                }
            }
            
        }
        else if (!trigger)
        {
            speed = 3;
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * transform.localScale.x * distanceBack);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        trigger = true;
        
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 Scaler = gameObject.transform.localScale;
        Scaler.x *= -1;
        gameObject.transform.localScale = Scaler;
        //transform.Rotate(0f, 180f, 0f);

    }

    public void JumpEnemy()
    {
        rb.velocity = Vector2.up * jumpForce;
    }


    public void death()
    {
        Destroy(gameObject);
    }

    public void OnEnemyAttack()
    {
        FindObjectOfType<Player>().health -= damege;
    }
}
