using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemy : MonoBehaviour
{

    public float health;
    public float speed;
    public float distance = 6f;
    public float distanceBack = 1.5f;
    RaycastHit2D hitBack;
    public Rigidbody2D rb;
    public bool facingLeft;
    public bool playerNoticed = false;
    public LayerMask SeePlayer;


    private bool hold = false;


    public bool isGround;
    public LayerMask whatIsGround;
    public float checkRaduis;
    public Transform feetPos;
    public float jumpForce;





    public Transform target;


    public GameObject Head;
    public GameObject Patrol;

    public Animator anim;


    public bool trigger = false;

    public bool triggerDeath = false;

    public int damege;

    private Material matBlink;
    private Material matDefault;
    private SpriteRenderer spriteRend;


    private float timeBtwShots;
    public float startTimeBtwShots = 4;
    public bool IwasHit = false;

    public bool CanJump;

    public NewAttackEnemey newAttackEnemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        anim = GetComponent<Animator>();

        spriteRend = GetComponent<SpriteRenderer>();

        matBlink = Resources.Load("EnemyBlink1", typeof(Material)) as Material;
        matDefault = spriteRend.material;
    }

    
    private void Update()
    {
        isGround = Physics2D.OverlapCircle(feetPos.position, checkRaduis, whatIsGround);

        Walking();

        hitBack = Physics2D.Raycast(transform.position, Vector3.left * transform.localScale.x, distanceBack, SeePlayer);
        if (hitBack.collider != null && hitBack.collider.CompareTag("Player") && newAttackEnemy.AttackHit == false)
        {
            Flip();
        }




        if (health <= 0 && isGround)
        {
            anim.Play("death");
            triggerDeath = true;
        }



        if (target != null)
        {
            if (Mathf.Round(target.transform.position.x) == Mathf.Round(transform.position.x) && trigger)
            {
                trigger = false;
            }
        }

        /*===== EnemyTrigger =====*/

        //if (trigger)
        //{
        //    speed = 4;
        //    if (target.position.x > transform.position.x && facingRight && !IwasHit)
        //    {
        //        Flip();
        //        trigger = false;
        //        facingRight = false;
        //    }
        //    else if (target.position.x < transform.position.x && !facingRight && !IwasHit)
        //    {
        //        Flip();
        //        trigger = false;
        //        facingRight = true;
        //    }
        //    timeBtwShots = startTimeBtwShots;
        //    IwasHit = true;
        //}
        //else if (!trigger)
        //    timeBtwShots -= Time.deltaTime;


        //if (timeBtwShots <= 0)
        //{
        //    speed = 3;
        //    IwasHit = false;
        //    timeBtwShots = startTimeBtwShots;
        //}

        /*=======================*/
    }

    public void Walking()
    {

        if (transform.localScale.x > 0 && !triggerDeath && speed != 0)
        {
            transform.Translate(Vector2.left * -1 * speed * Time.deltaTime);
        }
        else if (transform.localScale.x < 0 && !triggerDeath && speed != 0)
        {
            transform.Translate(Vector2.left * 1 * speed * Time.deltaTime);
        }

        if (speed > 0 && !anim.GetBool("attack_enemy"))
        {
            anim.SetBool("run_enemy", true);
        }
        else
        {
            anim.SetBool("run_enemy", false);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        trigger = true;

        spriteRend.material = matBlink;
        Invoke("ResetMaterial", 0.1f);
    }
    private void ResetMaterial()
    {
        spriteRend.material = matDefault;
    }

    public void Flip()
    {
        facingLeft = !facingLeft;

        Vector3 Scaler = gameObject.transform.localScale;
        Scaler.x *= -1;
        gameObject.transform.localScale = Scaler;
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
        RaycastHit2D HitAttack = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, distance, SeePlayer);
        if (HitAttack.collider != null && HitAttack.collider.CompareTag("Player"))
        {
            HitAttack.collider.GetComponent<Player>().health -= damege;
        }
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    //    Gizmos.DrawLine(transform.position, transform.position + Vector3.left * transform.localScale.x * distanceBack);
    //}
}
