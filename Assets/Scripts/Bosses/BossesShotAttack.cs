using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BossesShotAttack : MonoBehaviour
{
    public float HitAttackDistance = 1;
    private Bosses bosses;


    /*======= –ейкаст лучи дл€ обноружение игрока спереди ===========*/
    public RaycastHit2D AttackHit2DHeader;
    public RaycastHit2D AttackHit2DBody;
    public RaycastHit2D AttackHit2DFoot;

    public Transform HitLineAttackHeader;
    public Transform HitLineAttackBody;
    public Transform HitLineAttackFoot;
    /*===============================================================*/


    /*============ ¬рем€ между атаками и дистанци€ атаки ============*/
    private float TimeAttack;
    public float StartTimeAttack = 1;
    public float DistanceAttack = 1.5f;
    /*===============================================================*/
    public LayerMask SeePlayer;

    /*========================= «вуки ===============================*/

    public AudioSource AttackSound;
    /*===============================================================*/

    /*===================== Ёффекты от атаки ========================*/
    public bool EffectAttack;
    public GameObject PrfabSparks;
    public Transform PosSpawnSparks;
    /*===============================================================*/

    private float SaveSpeed;

    private float TimeAttackLongChase;
    public float StartTimeAttackLongChase = 1;

    void Start()
    {
        bosses = GetComponent<Bosses>();
        SaveSpeed = bosses.speed;
    }


    void Update()
    {
        AttackHit2DHeader = Physics2D.Raycast(HitLineAttackHeader.position, Vector3.right * transform.localScale.x, HitAttackDistance, SeePlayer);
        AttackHit2DBody = Physics2D.Raycast(HitLineAttackBody.position, Vector3.right * transform.localScale.x, HitAttackDistance, SeePlayer);
        AttackHit2DFoot = Physics2D.Raycast(HitLineAttackFoot.position, Vector3.right * transform.localScale.x, HitAttackDistance, SeePlayer);

        if ((AttackHit2DHeader.collider != null && AttackHit2DHeader.collider.CompareTag("Player") ||
            AttackHit2DBody.collider != null && AttackHit2DBody.collider.CompareTag("Player") ||
            AttackHit2DFoot.collider != null && AttackHit2DFoot.collider.CompareTag("Player")) && bosses.MyZoneControl.PlayerInZone)
        {
            bosses.playerNoticed = true;

            if ((Vector2.Distance(transform.position, bosses.target.position) <=  DistanceAttack) && !bosses.triggerDeath)
            {
                bosses.speed = 0;

                if ((Vector2.Distance(transform.position, bosses.target.position) <= DistanceAttack) && !bosses.triggerDeath && Vector2.Distance(transform.position, bosses.target.position) > 3f)
                {
                    bosses.speed = 0;

                    if (TimeAttack <= 0 && bosses.speed == 0)
                    {
                        bosses.anim.SetInteger("attack", Random.Range(2, 4));
                        TimeAttack = StartTimeAttack;
                    }
                    else
                    {
                        TimeAttack -= Time.deltaTime;
                    }
                }
                else if (Vector2.Distance(transform.position, bosses.target.position) <= 3f)
                {
                    bosses.speed = SaveSpeed * 2f;
                }
                else
                {
                    bosses.speed = SaveSpeed;
                    TimeAttack = 0;
                    bosses.anim.SetInteger("attack", 0);
                }

            }
            else if ((Vector2.Distance(transform.position, bosses.target.position) > DistanceAttack) && !bosses.triggerDeath)
            {
                if (TimeAttackLongChase <= 0)
                {
                    bosses.speed = 0;
                    bosses.anim.SetInteger("attack", SelectAttack());
                    TimeAttackLongChase = StartTimeAttackLongChase;
                }
                else
                {
                    TimeAttackLongChase -= Time.deltaTime;
                }
            }
            else
            {             
                bosses.speed = SaveSpeed;                
                TimeAttack = 0;
                bosses.anim.SetInteger("attack", 0);
                selectattack = false;
            }
        }
        else
        {
            bosses.anim.SetInteger("attack", 0);
            selectattack = false;
            if (Vector2.Distance(transform.position, bosses.target.position) >= 6f)
            {
                bosses.playerNoticed = false;
                bosses.speed = SaveSpeed;
            }

            TimeAttack = 0;
        }
    }


    private bool selectattack;
    private int SaveSelectAttack;
    private int SelectAttack()
    {
        if (!selectattack)
        {
            selectattack = true;
            SaveSelectAttack = Random.Range(1, 5);
            return SaveSelectAttack;
        }
        else
            return SaveSelectAttack;
    }
    public GameObject TNTPrefab;
    public Transform TNTSpawnPosition;
    public float PowerThrow = 5f;

    public GameObject BulletPrefab;
    public Transform BulletSpawnPosition;
    public void ThrowTNT()
    {
        SaveSelectAttack = Random.Range(2, 4);
        GameObject TNT = Instantiate(TNTPrefab, TNTSpawnPosition.position, transform.rotation);
        TNT.GetComponent<TNT>().WhereToFly = bosses.facingLeft;
        TNT.GetComponent<TNT>().powerThrow = PowerThrow;
        PowerThrow = 5;
    }
    public void Sneer()
    {
        SaveSelectAttack = Random.Range(1, 4);
    }
    public void Shot()
    {
        BulletPrefab.GetComponent<bullet_Enemy>().Direction = (int)gameObject.transform.localScale.x;
        Instantiate(BulletPrefab, BulletSpawnPosition.position, transform.rotation);
    }



    public void AttackAnimEnd(int index)
    {
        bosses.anim.SetInteger("attack", 0);
        selectattack = false;
        if (index == 1)
        {
            bosses.Flip();
            bosses.speed = SaveSpeed;
            bosses.playerNoticed = true;
            Invoke("FlipBack", 1f);
        }
    }
    private void FlipBack()
    {
        bosses.Flip();
        bosses.speed = SaveSpeed;
    }


    public void AttackSoundPlay()
    {
        if (EffectAttack)
        {
            GameObject obj = Instantiate(PrfabSparks);
            if (bosses.facingLeft)
                obj.transform.localScale = new Vector3(1, 1, 1);
            else
                obj.transform.localScale = new Vector3(-1, 1, 1);

            obj.transform.position = PosSpawnSparks.position;
        }
        AttackSound.Play();
    }


    private void OnDrawGizmos()
    {
        if (HitLineAttackHeader != null && HitLineAttackBody != null && HitLineAttackFoot != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(HitLineAttackHeader.position, HitLineAttackHeader.transform.position + Vector3.right * transform.localScale.x * HitAttackDistance);
            Gizmos.DrawLine(HitLineAttackBody.position, HitLineAttackBody.transform.position + Vector3.right * transform.localScale.x * HitAttackDistance);
            Gizmos.DrawLine(HitLineAttackFoot.position, HitLineAttackFoot.transform.position + Vector3.right * transform.localScale.x * HitAttackDistance);
        }
    }
}
