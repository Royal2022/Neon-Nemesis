using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class NewAttackEnemey : MonoBehaviour
{
    public GameObject Enemy;
    public float AttackDistance = 1;
    public RaycastHit2D AttackHit;
    private NewEnemy enemy;

    private float timeBtwShots;
    public float startTimeBtwShots = 4;
    public float enemyDistanceAttack = 1.5f;

    public LayerMask SeePlayer;


    void Start()
    {
          enemy = Enemy.GetComponent<NewEnemy>();
    }

    void Update()
    {
        //if (timeBtwShots <= 0)
        //{
        //    enemy.anim.SetBool("shot", true);
        //    timeBtwShots = startTimeBtwShots;
        //}
        //else
        //{
        //    enemy.anim.SetBool("shot", false);
        //    timeBtwShots -= Time.deltaTime;
        //}

        AttackHit = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, AttackDistance, SeePlayer);
        if (AttackHit.collider != null && AttackHit.collider.CompareTag("Player"))
        {
            if ((Vector2.Distance(transform.position, enemy.target.position) > enemyDistanceAttack) && !enemy.triggerDeath)
            {
                Vector2.Distance(transform.position, enemy.target.transform.position);
            }

            enemy.anim.SetBool("attack_enemy", true);
            enemy.speed = 0;
        }
        else
        {
            enemy.anim.SetBool("attack_enemy", false);
            enemy.speed = 3;
        }

    }



}
