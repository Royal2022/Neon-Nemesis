using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackEnemyResidentLVL2 : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots = 4;

    private Enemy enemyGetComp;

    public float enemyDistanceAttack = 1.5f;

    public AudioSource ShotSound;

    void Start()
    {
        enemyGetComp = gameObject.GetComponent<Enemy>();
    }
    void Update()
    {
        if (timeBtwShots <= 0)
        {
            enemyGetComp.anim.SetBool("shot", true);
            timeBtwShots = startTimeBtwShots;  
        }
        else
        {
            enemyGetComp.anim.SetBool("shot", false);
            timeBtwShots -= Time.deltaTime;
        }

        if (enemyGetComp.playerNoticed || enemyGetComp.trigger)
        {

            //if ((Vector2.Distance(transform.position, enemyGetComp.target.position) > enemyDistanceAttack /*&& !enemyGetComp.Patrol.gameObject.GetComponent<Patrol>().ground*/) && !enemyGetComp.triggerDeath)
            //{
            //    Vector2.Distance(transform.position, enemyGetComp.target.transform.position);
            //    enemyGetComp.anim.SetBool("attack_enemy", false);
            //}
            if (Vector2.Distance(transform.position, enemyGetComp.target.position) <= enemyDistanceAttack && !enemyGetComp.triggerDeath && enemyGetComp.isGround)
            {
                enemyGetComp.speed = 0;
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

    public void Shot()
    {
        bullet.gameObject.GetComponent<bullet_Enemy>().Direction = (int)gameObject.transform.localScale.x;
        Instantiate(bullet, shotPoint.position, transform.rotation);
        ShotSound.Play();
    }
}
