using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /*
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    public static Vector2 movement;

    */

    /* !!!!
    public float startTime;
    public float endTime;*/

    /*
    void Start()
    {
        Invoke("life", lifetime);

    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }

        if (Player.facingRight == true)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (Player.facingRight == false)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

    }


    void life()
    {
        Destroy(gameObject);
    }
    */
    /* !!!!
    void FixedUpdate()
    {
        startTime += 0.1f;

        if (startTime >= endTime)
        {
            Destroy(gameObject);
        }
    }*/


    public float speed = 25f;
    public int damage = 1;
    public Rigidbody2D rb;


    public float distance;
    public LayerMask whatIsSolid;

    SpriteRenderer sr;
    //[SerializeField] private Enemy enemy;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        //enemy = FindObjectOfType<Enemy>();

        if (Player.facingRight == true)
        {
            rb.velocity = transform.right * speed;
        }
        else if (Player.facingRight == false)
        {
            rb.velocity = (transform.right * -1) * speed;
        }
        Invoke("DestroyBullet", 2f);

    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Enemy"))
            {
                hitInfo.collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.gameObject.CompareTag("EnemyHead"))
            {
                hitInfo.collider.gameObject.GetComponent<head>().enemy.gameObject.GetComponent<Enemy>().TakeDamage(damage * 2);
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }

    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        if (Player.facingRight == false)
        {
            sr.flipX = true;
        }
    }


}
