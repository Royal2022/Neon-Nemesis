using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyLVL4 : MonoBehaviour
{
    private Enemy enemyGetComp;
    public float enemyDistanceAttack = 2.5f;

    private void Start()
    {
        enemyGetComp = gameObject.GetComponent<Enemy>();
    }

    void Update()
    {
        if (enemyGetComp.playerNoticed || enemyGetComp.trigger)
        {
            //if ((Vector2.Distance(transform.position, enemyGetComp.target.position) > enemyDistanceAttack /*&& !enemyGetComp.Patrol.gameObject.GetComponent<Patrol>().ground*/) && !enemyGetComp.triggerDeath)
            //{
            //    enemyGetComp.speed = 4;
            //    Vector2.Distance(transform.position, enemyGetComp.target.transform.position);
            //    enemyGetComp.anim.SetBool("attack_enemy", false);
            //}
            if (Vector2.Distance(transform.position, enemyGetComp.target.position) <= enemyDistanceAttack && !enemyGetComp.triggerDeath)
            {
                enemyGetComp.speed = 5;
                enemyGetComp.trigger = false;
                if (!enemyGetComp.anim.GetCurrentAnimatorStateInfo(0).IsName("attack_enemy"))
                {
                    enemyGetComp.anim.SetBool("attack_enemy", true);
                }
            }
        }
        else if (Vector2.Distance(transform.position, enemyGetComp.target.position) > enemyDistanceAttack)
        {
            enemyGetComp.anim.SetBool("attack_enemy", false);
            enemyGetComp.speed = 3;

        }
    }

    public void attack_off()
    {
        enemyGetComp.anim.SetBool("attack_enemy", false);
    }
}
