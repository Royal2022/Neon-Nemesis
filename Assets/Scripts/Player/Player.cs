using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;



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

    
    public Rigidbody2D rb;
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

    public int health = 10;
    public Text healthDisplay;

    public Slider stamine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        healthDisplay.text = "" + health;
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
        if (Input.GetKey(KeyCode.LeftShift) && stamine.value > 0 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            stamine.value -= 0.8f;
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            stamine.value += 0.2f;
        }
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            stamine.value += 0.5f;
        }
        if (Input.GetKey(KeyCode.LeftShift) && (!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D)))
        {
            stamine.value += 0.5f;
        }

           
        /*
        else    
        {    
        Speed = 4f;           
        }*/

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
            Debug.Log("right");

        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
            Debug.Log("left");
        }
    }
    public void Flip()
    {
        facingRight = !facingRight;
        
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        /*transform.Rotate(0f, 180f, 0f);*/
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamine.value > 10)
        {
            Speed = 6f;
        }
        else
        {
            Speed = 4f;
        }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pistol2"))
        {
            Destroy(collision.gameObject);
            AutomaticGun.allAmmo += 35;
        }
        else if (collision.CompareTag("pistol1"))
        {
            Destroy(collision.gameObject);
            Pistol.allAmmo += 15;
        }

    }





}
