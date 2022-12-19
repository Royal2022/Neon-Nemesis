using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletEnemyLVL2 : MonoBehaviour
{
    public float speed = 25f;
    public int damage = 2;
    public Rigidbody2D rb;


    public float distance;
    public LayerMask whatIsSolid;

    SpriteRenderer sr;
    //[SerializeField] private Enemy enemy;

    [HideInInspector] public Vector2 Direction;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        //enemy = FindObjectOfType<Enemy>();

        if (Direction.x > 0)
        {
            rb.velocity = transform.right * speed;
        }
        else if (Direction.x < 0)
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
            if (hitInfo.collider.gameObject.CompareTag("Player"))
            {
                hitInfo.collider.gameObject.GetComponent<Player>().TakeDamage(damage);
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }

    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }



}
