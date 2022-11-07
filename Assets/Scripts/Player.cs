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

    private bool isGrounded; 
    public Transform feetPos; 
    public float checkRaduis;
    public LayerMask whatIsGround;

    SpriteRenderer sr;

    public static Animator anim;
    private float moveInput;

    public static bool facingRight = true;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }




    public void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);

        /*
        if (Input.GetKey("left shift"))
        {
            rb.velocity = new Vector2(moveInput * Speed * 2, rb.velocity.y);
        }*/
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = 8f;
        }
        else
        {
            Speed = 4f;
        }

        //sr.flipX = moveInput < 0 ? true : false;


        moveInput = Input.GetAxis("Horizontal");

        if (moveInput == 0)
        {
            anim.SetBool("player_run", false);
        }
        else
        {
            anim.SetBool("player_run", true);
        }


        if (facingRight == false && moveInput > 0)
        {
            Flip();        
        }
        else if (facingRight == true && moveInput < 0)
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
        /*transform.Rotate(0f, 180f, 0f);*/
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

            /*==== Ladder =====*/
            //rb.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            //Player.anim.SetBool("touched_ground", true);
            /*=================*/

        }
        else
        {
            anim.SetBool("player_jump", true);
            //Player.anim.SetBool("touched_ground", false);

        }
    }
    

                    
    
    
}
