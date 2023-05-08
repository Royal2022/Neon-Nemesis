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


    public float distanceRayCast = 0.1f;
    public LayerMask whatIsSolid;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

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

    private void Update()
    {
        transform.right = rb.velocity;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distanceRayCast, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
                if (!enemy.Death)
                {
                    enemy.DamagSound.Play();
                    enemy.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
            if (hitInfo.collider.CompareTag("EnemyHead"))
            {
                Enemy enemy = hitInfo.collider.GetComponent<head>().enemy.gameObject.GetComponent<Enemy>();
                if (!enemy.Death)
                {
                    enemy.DamagSound.Play();
                    enemy.TakeDamage(damage * 2);
                    Destroy(gameObject);
                }
            }
            if (hitInfo.collider.CompareTag("Drones"))
            {
                hitInfo.collider.GetComponent<Drones>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("StreetLamp"))
            {
                if (hitInfo.collider.GetComponent<Light2D>().enabled)
                {
                    hitInfo.collider.GetComponent<AudioSource>().Play();
                    Destroy(gameObject);
                }

                hitInfo.collider.GetComponent<Light2D>().enabled = false;
            }
            if (hitInfo.collider.CompareTag("GrenadeItem"))
            {
                hitInfo.collider.GetComponent<grenade>().enabled = true;
                Destroy(gameObject);
            }
        }

        if (Player.facingRight == false)
            sr.flipX = true;
    }


}
