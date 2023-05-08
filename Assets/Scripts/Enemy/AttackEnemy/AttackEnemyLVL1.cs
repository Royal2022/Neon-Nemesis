using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AttackEnemyLVL1 : MonoBehaviour
{
    private Enemy enemyGetComp;
    public float enemyDistanceAttack = 1.5f;
    public AudioSource AttackSound;


    public bool EffectAttack;
    public GameObject PrfabSparks;
    public Transform PosSpawnSparks;

    private void Start()
    {
        enemyGetComp = gameObject.GetComponent<Enemy>();
    }

    private void Update()
    {
        if (enemyGetComp.playerNoticed || enemyGetComp.trigger)
        {
            if ((Vector2.Distance(transform.position, enemyGetComp.target.position) > enemyDistanceAttack /*&& !enemyGetComp.Patrol.gameObject.GetComponent<Patrol>().ground*/) && !enemyGetComp.triggerDeath)
            {
                enemyGetComp.speed = 4;
                Vector2.Distance(transform.position, enemyGetComp.target.transform.position);
                enemyGetComp.anim.SetBool("attack_enemy", false);
            }
            else if (Vector2.Distance(transform.position, enemyGetComp.target.position) <= enemyDistanceAttack && !enemyGetComp.triggerDeath && enemyGetComp.isGround)
            {
                enemyGetComp.speed = 0;
                enemyGetComp.trigger = false;
                if (!enemyGetComp.anim.GetCurrentAnimatorStateInfo(0).IsName("attack_enemy"))
                {
                    enemyGetComp.anim.SetBool("attack_enemy", true);
                }
            }
        }
    }
    public void AttackSoundPlay()
    {
        if (EffectAttack)
        {
            GameObject obj = Instantiate(PrfabSparks);
            if (enemyGetComp.facingRight)
                obj.transform.localScale = new Vector3(1, 1, 1);
            else
                obj.transform.localScale = new Vector3(-1, 1, 1);

            obj.transform.position = PosSpawnSparks.position;
        }
        AttackSound.Play();
    }
}
