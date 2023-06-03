using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttackEnemy : MonoBehaviour
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


    public float enemyDistanceExplosiv = 1f;
    public float enemyDistanceAttack = 3f;
    public LayerMask SeePlayer;

    /*========================= «вуки ===============================*/

    public AudioSource AttackSound;
    /*===============================================================*/

    /*===================== Ёффекты от атаки ========================*/
    public bool EffectAttack;
    public GameObject PrfabSparks;
    public Transform PosSpawnSparks;
    /*===============================================================*/

    public GameObject BoomEffect;

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
            enemy.speed = SaveSpeed * 2;

            if ((Vector2.Distance(transform.position, enemy.target.position) <= enemyDistanceAttack) && !enemy.triggerDeath && Mathf.Round(enemy.target.transform.position.x) != Mathf.Round(transform.position.x))
                enemy.anim.SetBool("attack_enemy", true);


            if (((Vector2.Distance(transform.position, enemy.target.position) <= enemyDistanceExplosiv) || 
                Mathf.Round(enemy.target.transform.position.x) == Mathf.Round(transform.position.x)) && !enemy.triggerDeath)
            {
                enemy.speed = 0;
                Instantiate(BoomEffect, gameObject.transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        else
        {
            enemy.anim.SetBool("attack_enemy", false);
            enemy.speed = SaveSpeed;
            if (!enemy.anim.GetBool("attack_enemy"))
                enemy.playerNoticed = false;
        }
    }

    public void AttackAnimEnd()
    {
        enemy.anim.SetBool("attack_enemy", false);
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
