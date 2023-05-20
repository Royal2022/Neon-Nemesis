using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_grenade : NetworkBehaviour
{

    public Rigidbody2D rb;
    public float powerThrow;

    private bool isGround;
    public LayerMask whatIsGround;
    public float checkRaduis;

    public GameObject BombsEffect;

    public float explosionTime = 1f;


    public void MyStart(bool facingRight)
    {
        if (facingRight)
        {
            //rb.velocity = transform.right * powerThrow;
            rb.velocity = new Vector2(transform.localScale.x, 1) * powerThrow;
        }
        else if (!facingRight)
        {
            //rb.velocity = (transform.right * -1) * powerThrow;
            rb.velocity = new Vector2(transform.localScale.x * -1, 1) * powerThrow;

        }
        Invoke("Explosion", explosionTime);
    }

    void Update()
    {

    }
    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(transform.position, checkRaduis, whatIsGround);


        if (!isGround)
        {
            transform.Rotate(0f, 0.0f, Time.deltaTime * 100f, Space.Self);
        }
    }

    [Server]
    public void Explosion()
    {
        GameObject obj = Instantiate(BombsEffect, transform.position, transform.rotation);
        NetworkServer.Spawn(obj);
        NetworkServer.Destroy(gameObject);
        //Destroy(gameObject);
    }
}
