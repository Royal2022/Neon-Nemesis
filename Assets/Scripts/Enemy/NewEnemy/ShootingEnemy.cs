using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public float HitAttackDistance = 1;
    private Enemy enemy;


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
    public float enemyDistanceAttack = 1.5f;
    /*===============================================================*/
    public LayerMask SeePlayer;

    /*========================= «вуки ===============================*/

    public AudioSource ShotSound;
    /*===============================================================*/

    /*===================== Ёффекты от атаки ========================*/
    public bool EffectAttack;
    public GameObject PrfabSparks;
    public Transform PosSpawnSparks;
    /*===============================================================*/

    public GameObject bullet;
    public Transform shotPoint;

    private float SaveSpeed;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        SaveSpeed = enemy.speed;
    }


    void Update()
    {
        AttackHit2DHeader = Physics2D.Raycast(HitLineAttackHeader.position, Vector3.right * transform.localScale.x, HitAttackDistance, SeePlayer);
        AttackHit2DBody = Physics2D.Raycast(HitLineAttackBody.position, Vector3.right * transform.localScale.x, HitAttackDistance, SeePlayer);
        AttackHit2DFoot = Physics2D.Raycast(HitLineAttackFoot.position, Vector3.right * transform.localScale.x, HitAttackDistance, SeePlayer);

        if ((AttackHit2DHeader.collider != null && AttackHit2DHeader.collider.CompareTag("Player") ||
            AttackHit2DBody.collider != null && AttackHit2DBody.collider.CompareTag("Player") ||
            AttackHit2DFoot.collider != null && AttackHit2DFoot.collider.CompareTag("Player")) && enemy.MyZoneControl.PlayerInZone)
        {
            enemy.playerNoticed = true;
            if ((Vector2.Distance(transform.position, enemy.target.position) <= enemyDistanceAttack) && !enemy.triggerDeath && Vector2.Distance(transform.position, enemy.target.position) > 0.6f)
            {
                enemy.speed = 0;

                enemy.anim.SetBool("attack_enemy", true);
                if (TimeAttack <= 0 && enemy.speed == 0)
                {
                    enemy.anim.SetBool("shot", true);
                    TimeAttack = StartTimeAttack;
                }
                else
                {
                    TimeAttack -= Time.deltaTime;
                }
            }
            else if (Vector2.Distance(transform.position, enemy.target.position) <= 0.6f)
            {
                enemy.speed = SaveSpeed * 1.5f;
            }
            else
            {
                enemy.speed = SaveSpeed;
                TimeAttack = 0;
                enemy.anim.SetBool("attack_enemy", false);
            }

        }
        else
        {
            enemy.anim.SetBool("attack_enemy", false);
            TimeAttack = 0;
            if (Vector2.Distance(transform.position, enemy.target.position) >= 1.5f)
            {
                enemy.playerNoticed = false;
                enemy.speed = SaveSpeed;
            }
        }
    }

    public void Shot()
    {
        bullet.gameObject.GetComponent<bullet_Enemy>().Direction = (int)gameObject.transform.localScale.x;
        Instantiate(bullet, shotPoint.position, transform.rotation);
        ShotSound.Play();
    }

    public void ShotAnimEnd()
    {
        enemy.anim.SetBool("shot", false);
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
        Gizmos.color = Color.red;
    }
}
