using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackEnemyResidentLVL2 : MonoBehaviour
{

    public GameObject bullet;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject enemy;

    public float enemyDistanceAttack = 1.5f;

    void Start()
    {
    }
    void Update()
    {
        if (timeBtwShots <= 0)
        {
            enemy.gameObject.GetComponent<Enemy>().anim.SetBool("shot", true);
            timeBtwShots = startTimeBtwShots;  
        }
        else
        {
            enemy.gameObject.GetComponent<Enemy>().anim.SetBool("shot", false);
            timeBtwShots -= Time.deltaTime;
        }



        if (enemy.gameObject.GetComponent<Enemy>().playerNoticed || enemy.gameObject.GetComponent<Enemy>().trigger)
        {

            if ((Vector2.Distance(transform.position, gameObject.GetComponent<Enemy>().target.position) > enemyDistanceAttack /*&& !gameObject.GetComponent<Enemy>().Patrol.gameObject.GetComponent<Patrol>().ground*/) && !gameObject.GetComponent<Enemy>().triggerDeath)
            {
                gameObject.GetComponent<Enemy>().speed = 4;
                Vector2.Distance(transform.position, gameObject.GetComponent<Enemy>().target.transform.position);
                gameObject.GetComponent<Enemy>().anim.SetBool("attack_enemy", false);
            }
            else if (Vector2.Distance(transform.position, gameObject.GetComponent<Enemy>().target.position) <= enemyDistanceAttack && !gameObject.GetComponent<Enemy>().triggerDeath)
            {
                gameObject.GetComponent<Enemy>().speed = 0;
                gameObject.GetComponent<Enemy>().trigger = false;
                if (!gameObject.GetComponent<Enemy>().anim.GetCurrentAnimatorStateInfo(0).IsName("attack_enemy"))
                {
                    gameObject.GetComponent<Enemy>().anim.SetBool("attack_enemy", true);
                }
            }
        }

    }

    public void Shot()
    {
        bullet.gameObject.GetComponent<bullet_Enemy>().Direction = (int)enemy.gameObject.transform.localScale.x;
        Instantiate(bullet, shotPoint.position, transform.rotation);           
    }



}
