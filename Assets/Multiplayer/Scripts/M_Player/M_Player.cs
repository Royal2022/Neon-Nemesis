using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Cinemachine;



public class M_Player : NetworkBehaviour
{
    public Rigidbody2D rb;
    private float moveInput;
    public float Speed = 4;
    public float jumpForce;

    [SyncVar(hook = nameof(OnHealth))]
    public int Health;
    [SyncVar(hook = nameof(OnArmor))]
    public int Armor;

    public Transform feetPos;
    public bool isGround;
    public float checkRaduis;
    public LayerMask whatIsGround;

    public Animator anim;

    [SerializeField] public OutPlayerInfo OutText;

    public Text TextName;
    [SerializeField] private Canvas NameCanvas;




    public GameObject CameraPosition;

    public static int pistol_AllAmmo = 0;
    public static int automaticGun_AllAmmo = 0;

    public bool IsItYou;
    public GameObject BulletPrefab;

    /*======================= ������������� ����������� ������ ================================*/
    [SyncVar(hook = nameof(OnBoolValueChanged))]
    public bool facingRight = true;
    /*=========================================================================================*/

    public GameObject holdpointAutomaticGun;


    public GameObject PlayerListingPrefab;
    public GameObject PLayerListingMenuContent;


    private LobbyManager lobbymanager;

    [SyncVar]
    public int NumberOfRoundsWins;

    private void Start()
    {
        lobbymanager = FindObjectOfType<LobbyManager>();
        transform.GetChild(3).gameObject.SetActive(false);
        GetComponent<Rigidbody2D>().simulated = false;

        OutText = FindObjectOfType<OutPlayerInfo>();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


        DisabledAllChildrenAndParent();

        if (!isLocalPlayer) return;
        {
            lobbymanager.CmdSavePlayerName(M_PlayerInfoSave.PlayerNickName); 
        }

        if (!isLocalPlayer) return;
        {
            FindObjectOfType<CameraFollow>().GetComponent<CinemachineVirtualCamera>().Follow = CameraPosition.transform;            
        }
            





    }


    /*======================== ���������� ������� � �������� �������� ====================*/

    [ClientRpc]
    public void RpcDisabledAllChildrenAndParent()
    {
        //GetComponent<M_Player>().enabled = false;
        GetComponent<M_WeaponSwitch>().enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

    }
    [Command(requiresAuthority = false)]
    public void CmdDisabledAllChildrenAndParent()
    {
        RpcDisabledAllChildrenAndParent();
    }
    public void DisabledAllChildrenAndParent()
    {
        if (isServer)
            RpcDisabledAllChildrenAndParent();
        else if (isClient)
            CmdDisabledAllChildrenAndParent();
    }
    /*====================================================================================*/



    public Animator animHold;


    private bool BoolName;
    [ClientRpc]
    public void RpcSetName(string newText)
    {
        if (!BoolName)
        {
            TextName.text = newText;
            BoolName = true;
        }
    }
    [Command(requiresAuthority = false)]
    public void CmdSetName(string newText)
    {
        RpcSetName(newText);
    }
    public void SetName(string newText)
    {
        if (isServer)       
            RpcSetName(newText);       
        else if (isClient)
            CmdSetName(newText);
    }


    //private bool BoolPlayerListingName;
    //[ClientRpc]
    //private void RpcPlayerListingSetName()
    //{
    //    if (!BoolPlayerListingName && TextName.text != "Nick")
    //    {
    //        //FindObjectOfType<LobbyManager>().CreatePlayerListing(TextName.text);
    //        BoolPlayerListingName = true;
    //    }
    //}
    //[Command]
    //public void CmdPlayerListingSetName()
    //{
    //    RpcPlayerListingSetName();
    //}
    //public void PlayerListingSetName()
    //{
    //    if (isServer)
    //        RpcPlayerListingSetName();
    //    else if (isClient)
    //        CmdPlayerListingSetName();
    //}

    private void Update()
    {
        //ActiveiteOrDisabledHold();


        if (!isLocalPlayer) return;
        SetName(M_PlayerInfoSave.PlayerNickName);
        //PlayerListingSetName();


        if (!isLocalPlayer) return;
        if (holdpointAutomaticGun.transform.childCount > 0)
        {
            if (holdpointAutomaticGun.transform.GetChild(0).GetComponent<M_AutomaticGun>().SHOT)
            {
                AnimTrueOrFalse("M_Fire_AutomaticGun", true);
                animHold.SetBool("M_Fire_AutomaticGun", true);
                CheckParticalValue(1);
            }
            else
            {
                AnimTrueOrFalse("M_Fire_AutomaticGun", false);
                animHold.SetBool("M_Fire_AutomaticGun", false);
                CheckParticalValue(0);
            }

        }



        if (!isLocalPlayer) return;
        if (Input.GetKeyDown(KeyCode.X))
            Health = 0;

        if (!isLocalPlayer) return;
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);

        isGround = Physics2D.OverlapCircle(feetPos.position, checkRaduis, whatIsGround);


        if (Input.GetKeyDown(KeyCode.Space))
        {
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


        if (transform.localScale.x <= -1 && moveInput > 0)
        {
            Flip();
        }
        else if (transform.localScale.x >= 1 && moveInput < 0)

        {
            Flip();
        }


        /*=========== �������� ��� �����  ===========*/
        if (!isLocalPlayer) return;
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
        OutTextHealth();

        /*================================== ������ ������ ========================================*/

        if (Health <= 0)
        {
            SetActiveOrDisabled(3, false);
            //SetActiveOrDisabled(4, false);

            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            anim.SetBool("death", true);
            //anim.Play("death");
        }
        /*=========================================================================================*/



        if (!isLocalPlayer) return;
            IsItYou = true;
        if (isLocalPlayer) return;
            IsItYou = false;

    }

    public void Flip()
    {
        //facingRight = !facingRight;
        SetBoolValueOnServer(!facingRight);

        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

        Vector3 Scaler2 = NameCanvas.transform.localScale;
        Scaler2.x *= -1;
        NameCanvas.transform.localScale = Scaler2;
    }

    private void FixedUpdate()
    {
        StaminFunc();
        if (Input.GetKey(KeyCode.LeftShift) && OutText.stamine.value > 3 && isGround == true)
        {
            Speed = 6f;
        }
        else
        {
            Speed = 4f;
        }

        OutInfoArmor();
    }


    public void StaminFunc()
    {
        if (Input.GetKey(KeyCode.LeftShift) && OutText.stamine.value > 0 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            OutText.stamine.value -= 0.6f;
        }
        if (!Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        {
            OutText.stamine.value += 0.2f;
        }
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            OutText.stamine.value += 0.5f;
        }
        if (Input.GetKey(KeyCode.LeftShift) && (!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S)))
        {
            OutText.stamine.value += 0.3f;
        }
    }


    /*======================= ���� ������ ================================*/
    public void CheckTakeDamage(int damage)
    {
        if (isServer)
        {
            TakeDamage(damage);
        }
        else
        {
            CmdTakeDamage(damage);
        }
    }


    private void OnHealth(int oldhealth, int health)
    {
        Health = health;
    }
    private void OnArmor(int oldarmor, int armor)
    {
        Armor = armor;
    }

    [Server]
    public void TakeDamage(int damage)
    {
        if (Armor <= 0)
        {
            if (Health != 0)
            {
                Health -= damage;
            }
        }
        else
            Armor -= damage;
    }

    [Command]
    public void CmdTakeDamage(int damage)
    {
        TakeDamage(damage);
    }
    /*=========================================================================================*/



    public void OutTextHealth()
    {
        if (!isLocalPlayer) return;
        OutText.HealthInfo(Health);
    }
    public void OutInfoArmor()
    {
        if (!isLocalPlayer) return;
        OutText.ArmorInfo(Armor);
    }





    [Server]
    public void SpawnBullet(uint owner, Vector3 target, Quaternion transf)
    {
        GameObject bulletGo = Instantiate(BulletPrefab, target, transf);

        NetworkServer.Spawn(bulletGo);

        //if (!isLocalPlayer) return;
        bulletGo.GetComponent<M_Bullet>().Init(owner, facingRight);
    }


    [Command]
    public void CmdSpawnBullet(uint owner, Vector3 target, Quaternion transf)
    {
        SpawnBullet(owner, target, transf);
    }

    public void CallSpawnBullet(uint owner, Vector3 target, Quaternion transf)
    {
        if (isServer)
        {
            SpawnBullet(owner, target, transf);
        }
        else if (isClient)
        {
            CmdSpawnBullet(owner, target, transf);
        }
    }



    /*======================= ������������� ����������� ������ ================================*/
    private void OnBoolValueChanged(bool oldValue, bool newValue)
    {
        facingRight = newValue;
    }

    private void SetBoolValueOnServer(bool newValue)
    {
        if (isServer)
            facingRight = newValue;
        else       
            CmdSetBoolValueOnServer(newValue);
    }

    [Command]
    private void CmdSetBoolValueOnServer(bool newValue)
    {
        facingRight = newValue;
    }
    /*=========================================================================================*/

    /*================================== ������ ������ ========================================*/
    public void DeathAnimEnd()
    {
        ReSpawnManager reSpawnManager = FindAnyObjectByType<ReSpawnManager>();
        reSpawnManager.ResSpawnInfo(gameObject);
        reSpawnManager.Win();
        gameObject.SetActive(false);
        anim.SetBool("death", false);
    }
    /*=========================================================================================*/


    /*=== ��������� ��������� �������� �������� �� �������������� ��� ���������������� ========*/
    [ClientRpc]
    public void RpcSetActive(int num, bool tf)
    {
        transform.GetChild(num).gameObject.SetActive(tf);
    }

    public void SetActiveOrDisabled(int num, bool tf)
    {
        if (isServer)
            RpcSetActive(num, tf);  
        else if (isClient)        
            CmdSetActive(num, tf);
    }

    [Command]
    public void CmdSetActive(int num, bool tf)
    {
        RpcSetActive(num, tf);
    }
    /*=========================================================================================*/



    [Command]
    void CmdAnimationBool(string animationName, bool value)
    {
        //animHold.SetBool(animationName, value);
        RpcSetAnimBool(animationName, value);
    }

    [ClientRpc]
    public void RpcSetAnimBool(string paramName, bool value)
    {
        animHold.SetBool(paramName, value);
    }

    public void AnimTrueOrFalse(string animationName, bool value)
    {
        if (isServer)
        {
            RpcSetAnimBool(animationName, value);
        }
        else if (isClient)
        {
            CmdAnimationBool(animationName, value);
        }
    }





    /*======================= Partical System================================*/
    [SyncVar(hook = nameof(OnParticalValue))]
    public int ParticalValue;

    private void OnParticalValue(int oldParticalValue, int particalValue)
    {
        ParticalValue = particalValue;
    }

    public void CheckParticalValue(int damage)
    {
        if (isServer)
        {
            ServParticalValue(damage);
        }
        else
        {
            CmdServParticalValue(damage);
        }
    }


    [Server]
    public void ServParticalValue(int damage)
    {
        ParticalValue = damage;
    }

    [Command]
    public void CmdServParticalValue(int damage)
    {
        ServParticalValue(damage);
    }
    /*=========================================================================================*/


    bool AcitveHoldStart;
    public void ActiveiteOrDisabledHold()
    {
        if (!AcitveHoldStart && !lobbymanager.gameObject.activeSelf)
        {
            AcitveHoldStart = true;
            transform.GetChild(3).gameObject.SetActive(true);
            GetComponent<Rigidbody2D>().simulated = true;
        }
    }

}


