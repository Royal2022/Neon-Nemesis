using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy : MonoBehaviour
{

    public int health;
    public float speed;
    public float distance = 6f;
    public float distanceBack = 1.5f;
    RaycastHit2D hit, hitBack;
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

    //[SerializeField] private feetPosEnemy fPE;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //Head = FindObjectOfType<head>();

        //fPE = FindObjectOfType<feetPosEnemy>();

    }


    private void Update()
    {

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRaduis, whatIsGround);



        if (!hold)
        {
            Physics2D.queriesStartInColliders = false;

            hit = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, distance, SeePlayer);
            if (hit.collider != null && hit.collider.gameObject.tag == "Player")
            {
                playerNoticed = true;
                hold =true;
            }
            else if (hit.collider == null && (!Head.gameObject.GetComponent<head>().playerNoticedHead /*|| !fPE.playerNoticedLegs*/))
            {
                playerNoticed = false;
            }

            if (playerNoticed)
            {
                if (Vector2.Distance(transform.position, target.position) > 1.5f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime * 2);
                }
            }


            hitBack = Physics2D.Raycast(transform.position, Vector3.left * transform.localScale.x, distanceBack, SeePlayer);
            if (hitBack.collider != null && hitBack.collider.gameObject.tag == "Player")
            {
                Flip();
                hold = true;
            }



        }
        else
        {
            hold = false;
        }
        Physics2D.queriesStartInColliders = true;
        

        





        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
        if (!playerNoticed)
        {
            if (transform.localScale.x > 0)
            {
                transform.Translate(Vector2.left * -1 * speed * Time.deltaTime);
            }
            else if (transform.localScale.x < 0)
            {
                transform.Translate(Vector2.left * 1 * speed * Time.deltaTime);
            }
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
        //Debug.Log(damage);
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

}
