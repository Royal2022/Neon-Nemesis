using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
//using UnityEditor.PackageManager;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health;
    public float speed;
    public float distance = 6f;
    public float distanceBack = 1.5f;
    RaycastHit2D hit, hitBack, hitattack;
    public Rigidbody2D rb;
    public bool facingRight = true;
    public bool playerNoticed = false;


    private bool hold = false;


    public bool isGround;
    public LayerMask whatIsGround;
    public float checkRaduis;
    public Transform feetPos;
    public float jumpForce;
    public LayerMask SeePlayer;
    


    public Transform target;


    public GameObject Head;
    public GameObject fPE;
    public GameObject Patrol;

    public Animator anim;


    public bool trigger = false;

    public bool triggerDeath = false;

    public int damege = 1;

    private Material matBlink;
    private Material matDefault;
    private SpriteRenderer spriteRend;


    private float timeBtwShots;
    public float startTimeBtwShots = 4;
    public bool IwasHit = false;

    public bool CanJump;

    public bool Death;

    /*=========Sound=========*/
    public AudioSource DeathSound;
    public AudioSource DamagSound;
    public AudioSource RunSound;
    /*==========================*/

    public bool ImInGrenadeRadius;

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

        if (!hold)
        {
            Physics2D.queriesStartInColliders = false;

            hit = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, distance, SeePlayer);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
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
            if (hitBack.collider != null && hitBack.collider.CompareTag("Player"))
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




        if (health <= 0 && isGround)
        {
            Death = true;
            anim.Play("death");
            triggerDeath = true;
        }


        if (transform.localScale.x > 0 && !triggerDeath)
        {
            transform.Translate(Vector2.left * -1 * speed * Time.deltaTime);
        }
        else if (transform.localScale.x < 0 && !triggerDeath)
        {
            transform.Translate(Vector2.left * 1 * speed * Time.deltaTime);
        }



        if (speed > 0 && !anim.GetBool("attack_enemy"))
        {
            anim.SetBool("run_enemy", true);
            if (!Death)
                RunSound.volume = 0.4f;          
        }
        else
        {
            anim.SetBool("run_enemy", false);
            RunSound.volume = 0;
        }

        if (target != null)
        {
            if (Mathf.Round(target.transform.position.x) == Mathf.Round(transform.position.x) && trigger)
            {
                trigger = false;
            }
        }

        if (trigger)
        {
            speed = 4;
            if (target.position.x > transform.position.x && facingRight && !IwasHit)
            {
                Flip();
                trigger = false;
                facingRight = false;
            }
            else if (target.position.x < transform.position.x && !facingRight && !IwasHit)
            {
                Flip();
                trigger = false;
                facingRight = true;
            }
            timeBtwShots = startTimeBtwShots;
            IwasHit = true;
        }
        else if (!trigger)
            timeBtwShots -= Time.deltaTime;
        

        if (timeBtwShots <= 0 && IwasHit)
        {
            IwasHit = false;
            timeBtwShots = startTimeBtwShots;
            speed = 3;
        }

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        trigger = true;
        IwasHit = false;

        spriteRend.material = matBlink;
        Invoke("ResetMaterial", 0.1f);
    }
    private void ResetMaterial()
    {
        spriteRend.material = matDefault;
    }

    public void Flip()
    {
        if (!Death)
        {
            facingRight = !facingRight;

            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }
    }

    public void JumpEnemy()
    {
        rb.velocity = Vector2.up * jumpForce;
        anim.SetBool("run_enemy", false);
    }


    public void death()
    {
        Destroy(gameObject);
    }
    public void DeathSoundPlay()
    {
        RunSound.volume = 0;
        DeathSound.Play();
    }

    public void OnEnemyAttack()
    {
        RaycastHit2D HitAttack = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, distance, SeePlayer);
        if (HitAttack.collider != null && HitAttack.collider.CompareTag("Player"))
        {
            HitAttack.collider.GetComponent<Player>().TakeDamage(damege, false, transform.position);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * transform.localScale.x * distanceBack);
    }
}
