using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{

    public static int pistol_ammo = 0;
    public static int automaticGun_ammo = 0;
    
    public Rigidbody2D rb;
    public float Speed;
    public float jumpForce;

    public bool isGround; 
    public Transform feetPos; 
    public float checkRaduis;
    public LayerMask whatIsGround;

    public Animator anim;
    private float moveInput;

    public static bool facingRight = true;

    public static int money;
    public Text MoneyText;

    public int health = 10;
    public int armor;

    public Slider stamine;
    public Slider healthSlider;
    public Slider armorSlider;

    [SerializeField] private WeaponSwitch WS;

    public int hand_damage = 1;

    public bool doubleJump = false;

    public Vector3 mousePos;
    public Vector3 mousePosClick;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        WS = FindObjectOfType<WeaponSwitch>();
    }


    public void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);

        StaminFunc();
       
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
            //Debug.Log("right");

        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
            //Debug.Log("left");
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }


    public void Update()
    {
        if (health <= 0)
            anim.Play("death");

        if (health >= 0)
            healthSlider.value = health;
        else if (health <= 0)
            healthSlider.value = 0;

        if (armor >= 0)
            armorSlider.value = armor;
        else if (armor <= 0)
            armorSlider.value = 0;

        MoneyText.text = "" + money;

        if (GetComponent<Animator>().runtimeAnimatorController == WS.nogunanim)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (anim.GetBool("player_run") && !anim.GetCurrentAnimatorStateInfo(0).IsName("run_attack"))
                {
                    anim.SetBool("run_attack", true);
                }
                else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                {
                    anim.SetBool("attack", true);
                }
            }
            else
            {
                anim.SetBool("attack", false);
                anim.SetBool("run_attack", false);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && stamine.value > 3 && isGround == true)
        {
            Speed = 6f;
        }
        else
        {
            Speed = 4f;
        }

        isGround = Physics2D.OverlapCircle(feetPos.position, checkRaduis, whatIsGround);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                rb.velocity = Vector2.up * jumpForce;
                anim.SetTrigger("takeOf");
                doubleJump = false;
            }
            else if (!doubleJump && GetComponent<Animator>().runtimeAnimatorController == WS.nogunanim)
            {
                doubleJump = true;
                rb.velocity = Vector2.up * jumpForce;
                anim.Play("sault");
            }
        }


        if (isGround == true)
        {
            anim.SetBool("player_jump", false);
        }
        else
        {
            anim.SetBool("player_jump", true);
        }


        /*=========== �������� ��� �����  ===========*/
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !anim.GetBool("player_run"))
        {
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x && facingRight)
            {
                Flip();
            }
            else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x && !facingRight)
            {
                Flip();
            }
        }
        /*===========================================*/
    }

    
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("pistol2"))
    //    {
    //        Destroy(collision.gameObject);
    //        automaticGun_ammo += 35;
    //        //Debug.Log("gun");   
    //    }
    //    else if (collision.CompareTag("pistol1"))
    //    {
    //        Destroy(collision.gameObject);
    //        pistol_ammo += 15;
    //        //Debug.Log("pistol");
    //    }
    //}
    

    public void StaminFunc()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamine.value > 0 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            stamine.value -= 0.6f;
        }
        if (!Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        {
            stamine.value += 0.2f;
        }
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            stamine.value += 0.5f;
        }
        if (Input.GetKey(KeyCode.LeftShift) && (!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S)))
        {
            stamine.value += 0.3f;
        }

        
        if (Input.GetKey(KeyCode.LeftShift) && stamine.value > 0 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && FindObjectOfType<Ladder>().isTrigger == true)
        {
            stamine.value -= 0.6f;
        }
    }


    public bool hold = false;

    public float distance = 0.7f;
    public LayerMask whatIsSolid;

    public void HandAttack()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(hand_damage);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        if (armor <= 0)
        {
            if (health != 0)
            {
                health -= damage;
            }
        }
        else
            armor -= damage;
    }

    public void Death()
    {
        Destroy(gameObject);
        Time.timeScale = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}



