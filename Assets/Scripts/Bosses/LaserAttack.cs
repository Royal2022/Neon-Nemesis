using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserAttack : MonoBehaviour
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

    public RaycastHit2D Hit2DLaserDamage;
    public Transform Hit2DLaserDamagePos;
    private float distantLaser;
    private float TimeLaser;
    public float StartTimeLaser = 0.5f;

    private float SaveSpeed;

    private float TimeAttackLongChase;
    public float StartTimeAttackLongChase = 1;

    private void Start()
    {
        bosses = GetComponent<Bosses>();
        SaveSpeed = bosses.speed;
    }


    private void Update()
    {
        AttackHit2DHeader = Physics2D.Raycast(HitLineAttackHeader.position, Vector3.right * transform.localScale.x, HitAttackDistance, SeePlayer);
        AttackHit2DBody = Physics2D.Raycast(HitLineAttackBody.position, Vector3.right * transform.localScale.x, HitAttackDistance, SeePlayer);
        AttackHit2DFoot = Physics2D.Raycast(HitLineAttackFoot.position, Vector3.right * transform.localScale.x, HitAttackDistance, SeePlayer);

        if ((AttackHit2DHeader.collider != null && AttackHit2DHeader.collider.CompareTag("Player") ||
            AttackHit2DBody.collider != null && AttackHit2DBody.collider.CompareTag("Player") ||
            AttackHit2DFoot.collider != null && AttackHit2DFoot.collider.CompareTag("Player")) && bosses.MyZoneControl.PlayerInZone)
        {
            bosses.playerNoticed = true;

            if ((Vector2.Distance(transform.position, bosses.target.position) <= DistanceAttack) && !bosses.triggerDeath && bosses.anim.GetInteger("attack") == 0)
            {
                bosses.speed = 0;

                if ((Vector2.Distance(transform.position, bosses.target.position) <= DistanceAttack) && !bosses.triggerDeath && Vector2.Distance(transform.position, bosses.target.position) > 3f && bosses.anim.GetInteger("attack") == 0)
                {
                    bosses.speed = 0;

                    if (TimeAttack <= 0 && bosses.speed == 0)
                    {
                        bosses.anim.SetInteger("attack", Random.Range(1, 3));
                        TimeAttack = StartTimeAttack;
                    }
                    else
                    {
                        TimeAttack -= Time.deltaTime;
                    }
                }
                else if (Vector2.Distance(transform.position, bosses.target.position) <= 3f && bosses.anim.GetInteger("attack") == 0)
                {
                    bosses.speed = SaveSpeed * 2f;
                }
                else if (bosses.anim.GetInteger("attack") == 0)
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
                if (bosses.anim.GetInteger("attack") == 0)
                {
                    bosses.speed = SaveSpeed;
                    TimeAttack = 0;
                    bosses.anim.SetInteger("attack", 0);
                    selectattack = false;
                }

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
                ResetLine();
            }

            TimeAttack = 0; 
        }


        Hit2DLaserDamage = Physics2D.Raycast(Hit2DLaserDamagePos.position, Vector3.right * transform.localScale.x, distantLaser, SeePlayer);

        if (Hit2DLaserDamage.collider != null && Hit2DLaserDamage.collider.CompareTag("Player")){
            if (TimeLaser <= 0)
            {
                Hit2DLaserDamage.collider.GetComponent<Player>().TakeDamage(1, transform.position);
                TimeLaser = StartTimeLaser;
            }
            else
            {
                TimeLaser -= Time.deltaTime;
            }
        }
    }


    private bool selectattack;
    private int SaveSelectAttack;
    private int SelectAttack()
    {
        if (!selectattack)
        {
            selectattack = true;
            SaveSelectAttack = Random.Range(1, 3);
            return SaveSelectAttack;
        }
        else
            return SaveSelectAttack;
    }


    public GameObject Line;
    public float MaxLineDistant;

    public void Sneer()
    {
        SaveSelectAttack = Random.Range(1, 4);
    }
    public void Shot()
    {
        Line.SetActive(true);
        StartCoroutine(ScaleLine(10));
        AttackSound.Play();
    }
    IEnumerator ScaleLine(float duration)
    {
        float distant = 0;

        while (distant < duration)
        {
            distant += 1f;
            Line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(distant, 0, 0));
            distantLaser = distant;
            yield return new WaitForSeconds(.01f);
        }
    }
    public void ResetLine()
    {
        Line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, 0));
        Line.SetActive(false);
        distantLaser = 0;
        TimeLaser = 0;
    }


    public GameObject BulletPrefab;
    public Transform BulletSpawnPosition;
    public void Shotintermittent()
    {
        AttackSound.Play();
        BulletPrefab.GetComponent<bullet_Enemy>().Direction = (int)gameObject.transform.localScale.x;
        Instantiate(BulletPrefab, BulletSpawnPosition.position, transform.rotation);
    }

    public void AttackAnimEnd()
    {
        bosses.anim.SetInteger("attack", 0);
        selectattack = false;
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

            Gizmos.DrawLine(Hit2DLaserDamagePos.position, Hit2DLaserDamagePos.transform.position + Vector3.right * transform.localScale.x * distantLaser);

        }
    }
}
