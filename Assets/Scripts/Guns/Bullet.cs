using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public float flightRange = 1f;
    public float speed = 25f;
    public int damage = 1;
    public Rigidbody2D rb;


    public float RadiusRayCast = 0.05f;
    public LayerMask whatIsSolid;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (Player.facingRight == false)
            sr.flipX = true;

        if (Player.facingRight == true)
        {
            rb.velocity = transform.right * speed;
        }
        else if (Player.facingRight == false)
        {
            rb.velocity = (transform.right * -1) * speed;
        }
        Invoke("DestroyBullet", flightRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            rb.velocity = Vector2.Reflect(-collision.relativeVelocity.normalized, collision.contacts[0].normal) * speed;
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.right = rb.velocity;

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, RadiusRayCast, transform.position, 1, whatIsSolid);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (!enemy.triggerDeath)
                {
                    enemy.DamagSound.Play();
                    enemy.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
            else if (hit.collider.CompareTag("EnemyHead"))
            {
                Enemy enemy = hit.collider.transform.parent.GetComponent<Enemy>();
                if (!enemy.triggerDeath)
                {
                    enemy.DamagSound.Play();
                    enemy.TakeDamage(damage * 2);
                    Destroy(gameObject);
                }
            }
            else if (hit.collider.CompareTag("Bosses"))
            {
                Bosses bosses = hit.collider.GetComponent<Bosses>();
                if (!bosses.triggerDeath)
                {
                    bosses.DamagSound.Play();
                    bosses.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
            else if (hit.collider.CompareTag("Drones"))
            {
                hit.collider.GetComponent<Drones>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (hit.collider.CompareTag("StreetLamp"))
            {
                if (hit.collider.GetComponent<Light2D>().enabled)
                {
                    hit.collider.GetComponent<AudioSource>().Play();
                    Destroy(gameObject);
                }
                hit.collider.GetComponent<Light2D>().enabled = false;
            }
            else if (hit.collider.CompareTag("GrenadeItem"))
            {
                hit.collider.GetComponent<grenade>().enabled = true;
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, 0.05f);
    }
}
