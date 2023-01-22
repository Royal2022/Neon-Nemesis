using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_Enemy : MonoBehaviour
{
    public float speed = 25f;
    public int damage = 2;
    public Rigidbody2D rb;


    public float distanceRayCast = 0.1f;
    public LayerMask whatIsSolid;

    SpriteRenderer sr;

    public int Direction;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (Direction > 0)
        {
            rb.velocity = transform.right * speed;
        }
        else if (Direction < 0)
        {
            rb.velocity = (transform.right * -1) * speed;
        }
        Invoke("DestroyBullet", 2f);
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distanceRayCast, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Player"))
            {
                hitInfo.collider.gameObject.GetComponent<Player>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }

    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
