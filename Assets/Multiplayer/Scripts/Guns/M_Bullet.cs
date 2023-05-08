using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class M_Bullet : NetworkBehaviour
{
    public float flightRange = 1f;
    public float speed = 25f;
    public int damage = 1;
    public Rigidbody2D rb;


    public float distanceRayCast = 0.1f;
    public LayerMask whatIsSolid;

    public GameObject sr;

    [SyncVar(hook = nameof(OnOvnerid))]
    private uint ownerId;
    private void OnOvnerid(uint oldownerId, uint OwnerId)
    {
        ownerId = OwnerId;
    }

    [Server]
    public void Init(uint owner, bool tf)
    {
        ownerId = owner;
        if (tf)
        {
            rb.velocity = transform.right * speed;
        }
        else if (!tf)
        {
            rb.velocity = (transform.right * -1) * speed;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void Start()
    {
        Invoke("DestroyBullet", flightRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))        
            rb.velocity = Vector2.Reflect(-collision.relativeVelocity.normalized, collision.contacts[0].normal) * speed;        
        else
        {
            collision.collider.GetComponent<M_Player>().TakeDamage(damage);
            NetworkServer.Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.right = rb.velocity;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distanceRayCast, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Player"))
            {
                if (hitInfo.collider.gameObject.GetComponent<M_Player>().netId != ownerId)
                {
                    hitInfo.collider.gameObject.GetComponent<M_Player>().TakeDamage(damage);
                    NetworkServer.Destroy(gameObject);
                }
            }
        }
    }


    void DestroyBullet()
    {
        NetworkServer.Destroy(gameObject);
    }
}
