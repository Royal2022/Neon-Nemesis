using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    public Rigidbody2D rb;
    public float powerThrow;

    private bool isGround;
    public LayerMask whatIsGround;
    public float checkRaduis;

    public GameObject BombsEffect;

    public float explosionTime = 1f;

    public bool WhereToFly;

    void Start()
    {
        if (WhereToFly)
        {
            rb.velocity = new Vector2(-transform.localScale.x, 1) * powerThrow;
        }
        else if (!WhereToFly)
        {
            rb.velocity = new Vector2(transform.localScale.x, 1) * powerThrow;
        }
        Invoke("Explosion", explosionTime);
    }


    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(transform.position, checkRaduis, whatIsGround);


        if (!isGround)
        {
            transform.Rotate(0f, 0.0f, Time.deltaTime * 100f, Space.Self);
        }
    }

    public void Explosion()
    {
        Instantiate(BombsEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
