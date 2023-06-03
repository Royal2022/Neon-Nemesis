using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health = 10;
    public float speed;
    public float distanceBack = 1.5f;
    public Rigidbody2D rb;
    public bool facingLeft;
    public bool playerNoticed = false;

    public LayerMask SeePlayer;

    public FootEnemyPatrol footEnemyPatrol;

    /*======= Рейкаст лучи для обноружение игрока сзади ===========*/
    public RaycastHit2D BackHit2DHeader;
    public RaycastHit2D BackHit2DBody;
    public RaycastHit2D BackHit2DFoot;

    public Transform HitLineBackHeader;
    public Transform HitLineBackBody;
    public Transform HitLineBackFoot;
    /*===============================================================*/



    [HideInInspector]
    public bool isGround;
    public LayerMask whatIsGround;
    public float checkRaduis;
    public Transform feetPos;
    public float jumpForce;

    [HideInInspector]
    public Transform target;


    [HideInInspector]
    public Animator anim;


    public bool trigger = false;

    public bool triggerDeath = false;

    private Material matBlink;
    private Material matDefault;
    private SpriteRenderer spriteRend;


    private float timeBtwShots;
    public float startTimeBtwShots = 4;
    public bool IwasHit = false;

    public bool CanJump;

    [HideInInspector]
    public bool ImInGrenadeRadius = false;

    public AudioSource RunSound;
    private bool runSound = false;
    public AudioSource DamagSound;
    public AudioSource DeathSound;

    [HideInInspector]
    public ZonePatrol MyZoneControl;

    private float SaveSpeed;


    public Material DeathDissolve;

    private void Start()
    {
        SaveSpeed = speed;
        MyZoneControl = transform.parent.GetComponent<ZonePatrol>();
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
        if (!isGround)
            speed = 0;

        Walking();

        BackHit2DHeader = Physics2D.Raycast(HitLineBackHeader.position, Vector3.left * transform.localScale.x, distanceBack, SeePlayer);
        BackHit2DBody = Physics2D.Raycast(HitLineBackBody.position, Vector3.left * transform.localScale.x, distanceBack, SeePlayer);
        BackHit2DFoot = Physics2D.Raycast(HitLineBackFoot.position, Vector3.left * transform.localScale.x, distanceBack, SeePlayer);
        if ((BackHit2DHeader.collider != null && BackHit2DHeader.collider.CompareTag("Player") ||
            BackHit2DBody.collider != null && BackHit2DBody.collider.CompareTag("Player") ||
            BackHit2DFoot.collider != null && BackHit2DFoot.collider.CompareTag("Player")) && !playerNoticed && MyZoneControl.PlayerInZone)
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

        if (trigger && MyZoneControl.PlayerInZone && !playerNoticed)
        {
            speed = SaveSpeed * 1.5f;
            if (target.position.x > transform.position.x && facingLeft /*&& !IwasHit*/)
            {
                Flip();
                trigger = false;
                //facingLeft = true;
            }
            else if (target.position.x < transform.position.x && !facingLeft /*&& !IwasHit*/)
            {
                Flip();
                trigger = false;
                //facingLeft = false;
            }
            //timeBtwShots = startTimeBtwShots;
            //IwasHit = true;
        }
        else if (!MyZoneControl.PlayerInZone)
        {
            speed = SaveSpeed;
            trigger = false;
        }
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

        if (speed > 0/* && !anim.GetBool("attack_enemy")*/)
        {
            anim.SetBool("run_enemy", true);
        }
        else
        {
            anim.SetBool("run_enemy", false);
        }


        if (anim.GetBool("run_enemy") && isGround && !runSound)
        {
            RunSound.Play();
            runSound = true;
        }
        else if (!anim.GetBool("run_enemy") && runSound || !isGround)
        {
            RunSound.Stop();
            runSound = false;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!triggerDeath)
        {
            health -= damage;
            trigger = true;

            spriteRend.material = matBlink;
            Invoke("ResetMaterial", 0.1f);
        }
    }
    private void ResetMaterial()
    {
        spriteRend.material = matDefault;
    }

    public void Flip()
    {
        if (!triggerDeath)
        {
            facingLeft = !facingLeft;

            Vector3 Scaler = gameObject.transform.localScale;
            Scaler.x *= -1;
            gameObject.transform.localScale = Scaler;
        }
    }

    public void JumpEnemy()
    {
        rb.velocity = Vector2.up * jumpForce;
    }


    public void death()
    {
        //Destroy(gameObject);
        //spriteRend.material = DeathDissolve;
        StartCoroutine(Dissolve());
    }
    public float fade;
    IEnumerator Dissolve()
    {
        while (spriteRend.material.GetFloat("_Fade") > 0)
        {
            fade = spriteRend.material.GetFloat("_Fade") - .05f;
            spriteRend.material.SetFloat("_Fade", fade);
            yield return new WaitForSeconds(.05f);
        }
        Destroy(gameObject);
    }

    public void DeathSoundPlay()
    {
        DeathSound.Play();
    }

    private void OnDrawGizmos()
    {
        if (HitLineBackHeader != null && HitLineBackBody != null && HitLineBackFoot != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(HitLineBackHeader.transform.position, HitLineBackHeader.transform.position + Vector3.left * transform.localScale.x * distanceBack);
            Gizmos.DrawLine(HitLineBackBody.transform.position, HitLineBackBody.transform.position + Vector3.left * transform.localScale.x * distanceBack);
            Gizmos.DrawLine(HitLineBackFoot.transform.position, HitLineBackFoot.transform.position + Vector3.left * transform.localScale.x * distanceBack);
        }
    }
}
