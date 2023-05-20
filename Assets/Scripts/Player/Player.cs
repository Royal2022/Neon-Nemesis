using System.Collections;
using System.Collections.Generic;
using System.Net;
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

    public static int money = 500;
    public Text MoneyText;

    public static int NumberOfGrenades = 3;
    public Text NumberOfGrenadesText;

    public float health = 10;
    public float armor;

    public Slider stamine;
    public Slider healthSlider;
    public Slider armorSlider;

    [SerializeField] private WeaponSwitch WS;

    public int hand_damage = 1;

    public bool doubleJump = false;

    public Vector3 mousePos;
    public Vector3 mousePosClick;


    /*=========Sound=========*/
    public AudioSource RunSound;
    private bool runSound = false;
    public AudioSource JumpSound;
    public AudioSource LadderUpSound;
    public AudioSource AmmoSound;
    public AudioSource ArmorSound;
    public AudioSource FirstAidSound;
    public AudioSource MoneySound;
    public AudioSource FractureSound;
    /*==========================*/

    public GameObject DeathCanvas;

    public bool ImInGrenadeRadius;

    /*============Red_Flicker============*/
    private Material matBlink;
    private Material matDefault;
    private SpriteRenderer spriteRend;
    public SpriteRenderer hold_P;
    public SpriteRenderer hold_A;
    /*===================================*/

    public bool death;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        WS = FindObjectOfType<WeaponSwitch>();

        spriteRend = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("EnemyBlink1", typeof(Material)) as Material;
        matDefault = spriteRend.material;
    }


    public void FixedUpdate()
    {

        StaminFunc();
       

        if (facingRight == false && moveInput > 0 && !PlayingOrNotAnim("ZipLine") && !PlayingOrNotAnim("idleZipLine"))
        {
            Flip();
            //Debug.Log("right");
        }
        else if (facingRight == true && moveInput < 0 && !PlayingOrNotAnim("ZipLine") && !PlayingOrNotAnim("idleZipLine"))
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

    public Animator LegsAnim;

    public void Update()
    {
        if (!PlayingOrNotAnim("dropGrenade") && !PlayingOrNotAnim("idle_dropGrenade") && health > 0 && !PlayingOrNotAnim("ZipLine") && !PlayingOrNotAnim("idleZipLine") && !death)
        {
            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);
        }

        if (moveInput == 0)
        {
            anim.SetBool("player_run", false);
        }
        else
        {
            anim.SetBool("player_run", true);
        }

        if (anim.GetBool("player_run") && isGround && !runSound)
        {
            //RunSound.volume = 0.5f;
            RunSound.Play();
            runSound = true;

            LegsAnim.gameObject.SetActive(true);
            LegsAnim.SetBool("run", true);
        }
        else if (!anim.GetBool("player_run") && runSound || !isGround)
        {
            //RunSound.volume = 0;
            RunSound.Stop();
            runSound = false;
            LegsAnim.SetBool("run", false);
        }




        if (health <= 0)
        {
            death = true;
            anim.Play("death");
            transform.Find("weapon_hands").gameObject.SetActive(false);
        }

        if (health >= 0)
            healthSlider.value = health;
        else if (health <= 0)
            healthSlider.value = 0;

        if (armor >= 0)
            armorSlider.value = armor;
        else if (armor <= 0)
            armorSlider.value = 0;

        MoneyText.text = "" + money;
        NumberOfGrenadesText.text = "" + NumberOfGrenades;

        if (anim.GetFloat("Blend") == 0/*GetComponent<Animator>().runtimeAnimatorController == WS.nogunanim*/)
        {
            if (Input.GetMouseButtonDown(0) && !PlayingOrNotAnim("dropGrenade") && !PlayingOrNotAnim("idle_dropGrenade"))
            {
                if (anim.GetBool("player_run") && !PlayingOrNotAnim("run_attack") && !PlayingOrNotAnim("jump"))
                {
                    anim.SetBool("run_attack", true);
                }
                else if (!PlayingOrNotAnim("attack"))
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


        if (Input.GetKeyDown(KeyCode.Space) && !PlayingOrNotAnim("idle_dropGrenade") && !PlayingOrNotAnim("dropGrenade") && !death)
        {
            LegsAnim.gameObject.SetActive(false);
            if (isGround)
            {
                rb.velocity = Vector2.up * jumpForce;
                anim.SetTrigger("takeOf");
                doubleJump = false;
            }
            else if (!doubleJump && anim.GetFloat("Blend") == 0 && !PlayingOrNotAnim("ZipLine"))
            {
                doubleJump = true;
                rb.velocity = Vector2.up * jumpForce;
                anim.Play("sault");
            }
        }


        if (isGround)
        {
            anim.SetBool("player_jump", false);
        }
        else
        {
            anim.SetBool("player_jump", true);
        }


        /*=========== Разворот при клике  ===========*/
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !anim.GetBool("player_run") && !PlayingOrNotAnim("ZipLine") && !PlayingOrNotAnim("idleZipLine") 
            && Time.timeScale != 0)
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



        /*=========Grenade==========*/
        //if (Input.GetKey(KeyCode.G) && !PlayingOrNotAnim("Run")
        //    && !PlayingOrNotAnim("jump") && !PlayingOrNotAnim("sault")
        //    && !PlayingOrNotAnim("run_attack") && !PlayingOrNotAnim("attack") && !anim.GetBool("player_jump") && NumberOfGrenades > 0 && !PlayingOrNotAnim("dropGrenade") && !anim.GetBool("throwGrenade"))
        //{
        //    //gameObject.GetComponent<Animator>().runtimeAnimatorController = nogunanim;
        //    //gameObject.GetComponent<Animator>().applyRootMotion = false;

        //    PowerThrow += 0.2f;
        //    if (PowerThrow > 15)
        //        PowerThrow = 15;

        //    anim.SetTrigger("throwGrenadeTrigger");
        //    if (PlayingOrNotAnim("idle_dropGrenade"))
        //    {
        //        WeaponHands.SetActive(false);
        //        HandPoint.SetActive(true);
        //    }
        //}
        //else if (PlayingOrNotAnim("idle_dropGrenade") && !anim.GetBool("throwGrenade"))
        //    anim.SetBool("throwGrenade", true);
        /*==========================*/
    }

    public bool PlayingOrNotAnim(string name)
    {
       return anim.GetCurrentAnimatorStateInfo(0).IsName($"{name}");
    }


    public void SaultEnd()
    {
        anim.Play("Jump", 0, 21);
    }


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
    public GameObject PostProcessing;
    public Blood bloodAnim;
    public void TakeDamage(float damage, Vector3 direction)
    {
        StartCoroutine(PostProcessing.GetComponent<PostProcessing>().Dawn());
        if (direction.x > transform.position.x)
        {
            if (facingRight)           
                bloodAnim.RandomBloodAnim(false);            
            else           
                bloodAnim.RandomBloodAnim(true);
        }
        else if (direction.x < transform.position.x)
        {
            if (facingRight)
                bloodAnim.RandomBloodAnim(true);
            else
                bloodAnim.RandomBloodAnim(false);
        }


        if (armor <= 0)
        {
            if (health != 0)
            {
                health -= damage;
            }
        }
        else
            armor -= damage;


        spriteRend.material = matBlink;
        hold_P.material = matBlink;
        hold_A.material = matBlink;
        Invoke("ResetMaterial", 0.1f);
    }

    private void ResetMaterial()
    {
        spriteRend.material = matDefault;
        hold_P.material = matDefault;
        hold_A.material = matDefault;
    }

    public void StartStun()
    {
        StartCoroutine(PostProcessing.GetComponent<PostProcessing>().StunDawn());
        StartCoroutine(PostProcessing.GetComponent<PostProcessing>().AberrationDawn());
    }


    /*=========Death==========*/
    public void Death()
    {
        //Destroy(gameObject);
        //Time.timeScale = 0;
        armor = 0;
        DeathCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
    /*==========================*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}



