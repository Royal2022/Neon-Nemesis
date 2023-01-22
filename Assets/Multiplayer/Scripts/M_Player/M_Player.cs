using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Cinemachine;
using Unity.Burst.CompilerServices;
using Photon.Realtime;
using Photon.Pun.Demo.Asteroids;

public class M_Player : MonoBehaviourPun
{

    public static int pistol_AllAmmo = 0;
    public static int automaticGun_AllAmmo = 0;
    //public static M_Player localPlayer;

    public Rigidbody2D rb;


    private float moveInput;
    public float Speed;
    public float jumpForce;


    public Transform feetPos;
    private bool isGround;
    public float checkRaduis;
    public LayerMask whatIsGround;

    //public bool doubleJump = false;

    public Animator anim;

    public static bool facingRight = true;

    [SerializeField] private OutPlayerInfo OutText;

    public bool hold;

    public int Health;



    [SerializeField] private Text TextName;
    [SerializeField] private Canvas NameCanvas;

    public GameObject CameraPosition;


    public int Player_ID;

    public PhotonView thisPhotonView;

    public bool canShot;


    private void Start()
    {
        thisPhotonView = GetComponent<PhotonView>();

        OutText = FindObjectOfType<OutPlayerInfo>();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        TextName.text = photonView.Owner.NickName;
        if (!photonView.IsMine) return;
        FindObjectOfType<CameraFollow>().m_player = CameraPosition.transform;

        thisPhotonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        Player_ID = thisPhotonView.ViewID;

        Debug.Log(Player_ID);
    }


    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject Gun2;


    private void Update()
    {

        if (Health <= 0)
        {
            //Death();
            gameObject.GetPhotonView().RPC("Death", RpcTarget.All);
        }

        if (!photonView.IsMine) return;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PhotonNetwork.Instantiate(Gun.name, new Vector2(Random.Range(-5, 5), 0), Quaternion.identity);
        }        
        if (!photonView.IsMine) return;
        if (Input.GetKeyDown(KeyCode.X))
        {
            PhotonNetwork.Instantiate(Gun2.name, new Vector2(Random.Range(-5, 5), 0), Quaternion.identity);
        }
        OutTextHealth();


        /*=============== HandAttack =================*/
        /*
        if (Input.GetMouseButtonDown(0))
        {
            if (anim.GetBool("player_run"))
            {
                anim.SetBool("run_attack", true);
            }
            else
            {
                anim.SetBool("attack", true);
            }
        }
        else
        {
            anim.SetBool("attack", false);
            anim.SetBool("run_attack", false);
        }*/
        /*=============================================*/


        if (!photonView.IsMine) return;
            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);

            isGround = Physics2D.OverlapCircle(feetPos.position, checkRaduis, whatIsGround);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if (isGround)
            //{
            //    rb.velocity = Vector2.up * jumpForce;
            //    doubleJump = false;
            //    anim.SetBool("sault", false);
            //}
            //else if (!doubleJump /*&& GetComponent<Animator>().runtimeAnimatorController == WS.nogunanim*/)
            //{
            //    doubleJump = true;
            //    rb.velocity = Vector2.up * jumpForce;
            //    anim.SetBool("sault", true);
            //}
            if (isGround)
            {
                rb.velocity = Vector2.up * jumpForce;
                anim.SetBool("player_jump", true);
            }
        }



        if (isGround)
        {
            anim.SetBool("player_jump", false);
            anim.SetBool("sault", false);
        }
        else
        {
            anim.SetBool("player_jump", true);
        }


        
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



        /*=========== Разворот при клике  ===========*/
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

        if (!photonView.IsMine) return;
        canShot = true;
        if (photonView.IsMine) return;
        canShot = false;


    }

    public void Flip()
    {
        if (!photonView.IsMine) return;
        facingRight = !facingRight;
            
        Vector3 Scaler = transform.localScale;           
        Scaler.x *= -1;           
        transform.localScale = Scaler;


        Vector3 Scaler2 = NameCanvas.transform.localScale;
        Scaler2.x *= -1;
        NameCanvas.transform.localScale = Scaler2;
        
    }

    [PunRPC]
    public void TakeDamage(int damage, int ID,  PhotonMessageInfo info)
    {
        if (!photonView.IsMine) return;
        Health -= damage;
        OutTextHealth();
        Debug.Log("Player_ID: "  + Player_ID);
        Debug.Log("Id: " + ID);
    }

    public void OutTextHealth()
    {
        if (!photonView.IsMine) return;
        OutText.HealthInfo(Health);
    }

    [PunRPC]
    public void Death()
    {
        if (!photonView.IsMine) return;
        PhotonNetwork.Destroy(gameObject);
    }

    //public float distance = 0.7f;
    //public LayerMask whatIsSolid;
    //public int hand_damage = 1;


    //public void HandAttack()
    //{      
    //    Physics2D.queriesStartInColliders = false;

    //    RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, whatIsSolid);    

    //    if (hitInfo.collider != null && hitInfo.collider.tag == "Player")    
    //    {           
    //        //if (hitInfo.collider.CompareTag("Player"))        
    //        if (photonView.IsMine) return;
    //        hitInfo.collider.gameObject.GetComponent<M_Player>().TakeDamage(hand_damage);   
    //    }
    //    Physics2D.queriesStartInColliders = true;


    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.black;
    //    Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    //}





}







