using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Cinemachine;
using UnityEngine.Rendering.Universal;



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

    /*======================= Синхронизация направление игрока ================================*/
    [SyncVar(hook = nameof(OnBoolValueChanged))]
    public bool facingRight = true;
    /*=========================================================================================*/

    public GameObject holdpointAutomaticGun;


    public GameObject PlayerListingPrefab;
    public GameObject PLayerListingMenuContent;


    private LobbyManager lobbymanager;

    [SyncVar]
    public int NumberOfRoundsWins;

    public bool ImInGrenadeRadius;

    public bool doubleJump = false;

    public uint MyId;

    public Slider stamine;
    public Slider healthSlider;
    public Slider armorSlider;


    public M_PostProcessing PostProcessing;

    private void Start()
    {
        PostProcessing = FindObjectOfType<M_PostProcessing>();
        MyId = netId;
        lobbymanager = FindObjectOfType<LobbyManager>();
        transform.GetChild(3).gameObject.SetActive(false);
        GetComponent<Rigidbody2D>().simulated = false;

        OutText = FindObjectOfType<OutPlayerInfo>();
        stamine = OutText.stamine;
        healthSlider = OutText.healthSlider;
        armorSlider = OutText.armorSlider;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


        DisabledAllChildrenAndParent();

        spriteRend = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("EnemyBlink1", typeof(Material)) as Material;
        matDefault = spriteRend.material;

        if (!isLocalPlayer) return;
        {
            lobbymanager.CmdSavePlayerName(M_PlayerInfoSave.PlayerNickName); 
        }

        if (!isLocalPlayer) return;
        {
            FindObjectOfType<CameraFollow>().GetComponent<CinemachineVirtualCamera>().Follow = CameraPosition.transform;            
        }
    }


    /*======================== Отключение скрипта и дочерних объектов ====================*/

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

        isGround = Physics2D.OverlapCircle(feetPos.position, checkRaduis, whatIsGround);

        if (!isLocalPlayer) return;
        if (!PlayingOrNotAnim("dropGrenade") && !PlayingOrNotAnim("idle_dropGrenade") && Health > 0)
        {
            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGround)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    doubleJump = false;
                }
                else if (!doubleJump && anim.GetFloat("Blend") == 0 /*GetComponent<Animator>().runtimeAnimatorController == WS.nogunanim*/)
                {
                    doubleJump = true;
                    rb.velocity = Vector2.up * jumpForce;
                    anim.Play("sault");
                }
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


        /*=========== Разворот при клике  ===========*/
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

        /*================================== Смерть игрока ========================================*/

        if (Health <= 0)
        {
            SetActiveOrDisabled(3, false);
            //SetActiveOrDisabled(4, false);

            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            anim.SetBool("death", true);
            //anim.Play("death");
        }
        /*=========================================================================================*/



        /*=================================== Ближняя Атака =======================================*/
        if (!isLocalPlayer) return;
        if (anim.GetFloat("Blend") == 0/*GetComponent<Animator>().runtimeAnimatorController == WS.nogunanim*/)
        {
            if (Input.GetMouseButtonDown(0) && !PlayingOrNotAnim("dropGrenade") && !PlayingOrNotAnim("idle_dropGrenade"))
            {
                if (anim.GetBool("player_run") && !PlayingOrNotAnim("AttackHandRun") && !PlayingOrNotAnim("jump"))
                {
                    anim.SetBool("AttackHandRun", true);
                }
                else if (!PlayingOrNotAnim("attack"))
                {
                    anim.SetBool("AttackHand", true);
                }
            }
            else
            {
                anim.SetBool("AttackHand", false);
                anim.SetBool("AttackHandRun", false);
            }
        }
        /*=========================================================================================*/




        if (!isLocalPlayer) return;
            IsItYou = true;
        if (isLocalPlayer) return;
            IsItYou = false;
    }


    public bool PlayingOrNotAnim(string name)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName($"{name}");
    }


    public void SaultEnd()
    {
        anim.Play("Jump", 0, 21);
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


    /*======================= Урон играку ================================*/
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

    /*============Red_Flicker============*/
    private Material matBlink;
    private Material matDefault;
    private SpriteRenderer spriteRend;
    public SpriteRenderer hold_P;
    public SpriteRenderer hold_A;
    /*===================================*/
    [ClientRpc]
    private void RpcTakeDamage(int damage)
    {
        if (!isLocalPlayer) return;
        if (Armor <= 0)
        {
            if (Health != 0)
            {
                Health -= damage;
            }
        }
        else
            Armor -= damage;

        Blink();
        StartCoroutine(PostProcessing.Dawn());
    }
    [Command]
    private void CmdTakeDamage(int damage)
    {
        if (!isLocalPlayer) return;
        RpcTakeDamage(damage);
    }
    public void TakeDamage(int damage)
    {
        if (isServer)
            RpcTakeDamage(damage);
        else if (isClient)
            CmdTakeDamage(damage);
    }


    public void StartStun()
    {
        if (!isLocalPlayer) return;
        StartCoroutine(PostProcessing.StunDawn());
        StartCoroutine(PostProcessing.AberrationDawn());
    }

    /*================ Эффект получение урона ======================*/
    [ClientRpc]
    private void RpcBlink()
    {
        spriteRend.material = matBlink;
        hold_P.material = matBlink;
        hold_A.material = matBlink;
        Invoke("ResetMaterial", 0.1f);
    }
    [Command]
    private void CmdBlink()
    {
        RpcBlink();
    }
    private void Blink()
    {
        if (isServer)
            RpcBlink();
        else if (isClient)
            CmdBlink();
    }

    [ClientRpc]
    private void RpcResetMaterial()
    { 
        spriteRend.material = matDefault;
        hold_P.material = matDefault;
        hold_A.material = matDefault;
    }
    [Command(requiresAuthority = false)]
    private void CmdResetMaterial()
    {
        RpcResetMaterial();
    }
    private void ResetMaterial()
    {
        if (isServer)
            RpcResetMaterial();
        else if (isClient)
            CmdResetMaterial();
    }
    /*=============================================================*/



    /*====================================== Ближняя Атака ========================================*/
    public float distance = 1;
    public LayerMask whatIsSolid;
    public int HandDamage = 1;

    [Command(requiresAuthority = false)]
    private void CmdHandAttack()
    {
        RaycastHit2D[] hitInfos = Physics2D.RaycastAll(transform.position, Vector2.right * transform.localScale.x, distance, whatIsSolid);

        foreach (var hitInfo in hitInfos)
        {
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    if (MyId != hitInfo.collider.GetComponent<M_Player>().netId)
                    {
                        hitInfo.collider.GetComponent<M_Player>().TakeDamage(HandDamage);
                        Debug.Log(hitInfo.collider);
                    }
                }
            }
        }
    }
    private void HandAttack()
    {
        if (!isLocalPlayer) return;
            CmdHandAttack();
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
    private void SpawnBullet(uint owner, Vector3 target, Quaternion transf)
    {
        GameObject bulletGo = Instantiate(BulletPrefab, target, transf);

        NetworkServer.Spawn(bulletGo);

        //if (!isLocalPlayer) return;
        bulletGo.GetComponent<M_Bullet>().Init(owner, facingRight);
    }


    [Command]
    private void CmdSpawnBullet(uint owner, Vector3 target, Quaternion transf)
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



    /*======================= Синхронизация направление игрока ================================*/
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

    /*================================== Смерть игрока ========================================*/
    public void DeathAnimEnd()
    {
        ReSpawnManager reSpawnManager = FindAnyObjectByType<ReSpawnManager>();
        reSpawnManager.ResSpawnInfo(gameObject);
        reSpawnManager.Win();
        gameObject.SetActive(false);
        anim.SetBool("death", false);
    }
    /*=========================================================================================*/


    /*=== Изменение состояния дочерних объектов на активированный или деактивированный ========*/
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


    /*====================== Спавн партикал эффектов после аптечки =============================*/
    public GameObject EffectFirstAid;
    [ClientRpc]
    private void RpcCreateChildObj()
    {
        Instantiate(EffectFirstAid, transform);
    }
    [Command]
    private void CmdCreateChildObj()
    {
        RpcCreateChildObj();
    }
    public void SpawnChildEffectFirstAid()
    {
        if (isServer)
            RpcCreateChildObj();
        else if (isClient)
            CmdCreateChildObj();
    }
    /*=========================================================================================*/
}




