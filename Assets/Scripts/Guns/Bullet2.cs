using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
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

    /*
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        Destroy(gameObject);
    }
    */
    void Update()
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("EnemyHead"))
            {
                hitInfo.collider.GetComponent<head>().enemy.gameObject.GetComponent<Enemy>().TakeDamage(damage*2);
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
