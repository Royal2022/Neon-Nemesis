//using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{

    public Rigidbody2D rb;
    public float powerThrow;

    private bool isGround;
    public LayerMask whatIsGround;
    public float checkRaduis;

    public GameObject BombsEffect;

    public float explosionTime = 1f;

    void Start()
    {
        if (Player.facingRight == true)
        {
            //rb.velocity = transform.right * powerThrow;
            rb.velocity = new Vector2(transform.localScale.x, 1) * powerThrow;
        }
        else if (Player.facingRight == false)
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

    public void Explosion()
    {
        Instantiate(BombsEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
