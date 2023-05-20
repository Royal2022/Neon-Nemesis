using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MeleeAttackEnemey : MonoBehaviour
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

    public AudioSource AttackSound;
    /*===============================================================*/

    /*===================== Ёффекты от атаки ========================*/
    public bool EffectAttack;
    public GameObject PrfabSparks;
    public Transform PosSpawnSparks;
    /*===============================================================*/

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
            if ((Vector2.Distance(transform.position, enemy.target.position) <= enemyDistanceAttack) && !enemy.triggerDeath)
            {
                enemy.speed = 0;

                if (TimeAttack <= 0 && enemy.speed == 0 )
                {
                    enemy.anim.SetBool("attack_enemy", true);
                    TimeAttack = StartTimeAttack;
                }
                else
                {
                    TimeAttack -= Time.deltaTime;
                }
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
            enemy.speed = SaveSpeed;
            enemy.playerNoticed = false;
        }
    }

    public void AttackAnimEnd()
    {
        enemy.anim.SetBool("attack_enemy", false);
    }

    public void AttackSoundPlay()
    {
        if (EffectAttack)
        {
            GameObject obj = Instantiate(PrfabSparks);
            if (enemy.facingLeft)
                obj.transform.localScale = new Vector3(1, 1, 1);
            else
                obj.transform.localScale = new Vector3(-1, 1, 1);

            obj.transform.position = PosSpawnSparks.position;
        }
        AttackSound.Play();
    }

    public float LengthAttackHit = 1.1f;
    public float damage = 2;

    public void OnEnemyAttack()
    {
        RaycastHit2D HitAttack = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, LengthAttackHit, SeePlayer);
        if (HitAttack.collider != null && HitAttack.collider.CompareTag("Player"))
        {
            //HitAttack.collider.GetComponent<Player>().health -= damage;
            HitAttack.collider.GetComponent<Player>().TakeDamage(damage, transform.position);
        }
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
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * LengthAttackHit);
    }
}
