using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour

{
    /*
    public float speed = 1f;
    public float jumpForce = 5f;
    Rigidbody2D rb;
    SpriteRenderer sr;

    private Animator anim;
    private float moveInput;
    private float moveInput2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");

        if(moveInput == 0)
        {
            anim.SetBool("player_run", false);
        }
        else
        {
            anim.SetBool("player_run", true);
        }

        moveInput2 = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            anim.SetBool("player_jump", true);
        }
        else
        {
            anim.SetBool("player_jump", false);
        }
    }



    void Update()
    {
        float movement = Input.GetAxis("Horizontal");

        transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.05f)
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        sr.flipX = movement < 0 ? true : false;



    }
    */

    
    private Rigidbody2D rb;
    public float Speed;
    public float jumpForce;
    public float moveImput;

    private bool isGrounded; 
    public Transform feetPos; 
    public float checkRaduis;
    public LayerMask whatIsGround;

    SpriteRenderer sr;

    private Animator anim;
    private float moveInput;

    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }



    public void FixedUpdate()
    {
        moveImput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveImput * Speed, rb.velocity.y);

        if (Input.GetKey("left shift"))
        {
            rb.velocity = new Vector2(moveImput * Speed * 2, rb.velocity.y);
        }

        //sr.flipX = moveImput < 0 ? true : false;


        moveInput = Input.GetAxis("Horizontal");

        if (moveInput == 0)
        {
            anim.SetBool("player_run", false);
        }
        else
        {
            anim.SetBool("player_run", true);
        }


        if (facingRight == false && moveImput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveImput < 0)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRaduis, whatIsGround);

        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("takeOf");
        }



        if (isGrounded == true)
        {
            anim.SetBool("player_jump", false);
        }
        else
        {
            anim.SetBool("player_jump", true);

        }
    }
    

                    
    
    
}
