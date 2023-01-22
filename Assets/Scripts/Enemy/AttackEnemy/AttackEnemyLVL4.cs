using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyLVL4 : MonoBehaviour
{
    public GameObject enemy;
    public float enemyDistanceAttack = 2.5f;



    void Update()
    {
        if (enemy.gameObject.GetComponent<Enemy>().playerNoticed || enemy.gameObject.GetComponent<Enemy>().trigger)
        {

            if ((Vector2.Distance(transform.position, gameObject.GetComponent<Enemy>().target.position) > enemyDistanceAttack /*&& !gameObject.GetComponent<Enemy>().Patrol.gameObject.GetComponent<Patrol>().ground*/) && !gameObject.GetComponent<Enemy>().triggerDeath)
            {
                gameObject.GetComponent<Enemy>().speed = 5;
                Vector2.Distance(transform.position, gameObject.GetComponent<Enemy>().target.transform.position);
                gameObject.GetComponent<Enemy>().anim.SetBool("attack_enemy", false);
            }
            else if (Vector2.Distance(transform.position, gameObject.GetComponent<Enemy>().target.position) <= enemyDistanceAttack && !gameObject.GetComponent<Enemy>().triggerDeath)
            {
                gameObject.GetComponent<Enemy>().speed = 5;
                gameObject.GetComponent<Enemy>().trigger = false;
                if (!gameObject.GetComponent<Enemy>().anim.GetCurrentAnimatorStateInfo(0).IsName("attack_enemy"))
                {
                    gameObject.GetComponent<Enemy>().anim.SetBool("attack_enemy", true);
                }
            }
        }
    }

    public void attack_off()
    {
        gameObject.GetComponent<Enemy>().anim.SetBool("attack_enemy", false);
    }
}
