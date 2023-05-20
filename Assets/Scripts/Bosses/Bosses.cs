using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bosses : MonoBehaviour
{
    public float health;
    public float speed;
    
    public bool triggerDeath;

    public Rigidbody2D rb;
    public Animator anim;

    public LayerMask SeePlayer;

    /*======= Рейкаст лучи для обноружение игрока сзади ===========*/
    public float distanceBack;

    public RaycastHit2D BackHit2DHeader;
    public RaycastHit2D BackHit2DBody;
    public RaycastHit2D BackHit2DFoot;

    public Transform HitLineBackHeader;
    public Transform HitLineBackBody;
    public Transform HitLineBackFoot;
    /*===============================================================*/


    private Material matBlink;
    private Material matDefault;
    private SpriteRenderer spriteRend;

    public bool facingLeft = false;


    public bool playerNoticed = false;

    public bool ImInGrenadeRadius = false;

    [HideInInspector]
    public Transform target;

    public bool isGround;
    public Transform feetPos;
    public float checkRaduis;
    public LayerMask whatIsGround;
    public float jumpForce;
    public bool IwasHit;
    public bool CanJump;
    public bool trigger;

    [HideInInspector]
    public ZonePatrol MyZoneControl;


    public AudioSource DamagSound;

    private float SaveSpeed;

    void Start()
    {
        SaveSpeed = speed;
        MyZoneControl = transform.parent.GetComponent<ZonePatrol>();

        whatIsGround = LayerMask.GetMask("Ground");

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


        spriteRend = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("EnemyBlink1", typeof(Material)) as Material;
        matDefault = spriteRend.material;
    }

    void Update()
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
    }

    private void Walking()
    {
        if (transform.localScale.x > 0 && !triggerDeath && speed > 0)
        {
            transform.Translate(Vector2.left * -1 * speed * Time.deltaTime);
        }
        else if (transform.localScale.x < 0 && !triggerDeath && speed > 0)
        {
            transform.Translate(Vector2.left * 1 * speed * Time.deltaTime);
        }

        if (speed > 0)
            anim.SetBool("run", true);
        else anim.SetBool("run", false);
    }
    public void JumpBosses()
    {
        rb.velocity = Vector2.up * jumpForce;
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

    public void death()
    {
        Destroy(gameObject);
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
