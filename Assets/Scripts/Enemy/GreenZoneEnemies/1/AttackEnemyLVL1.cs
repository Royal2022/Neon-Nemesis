using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyLVL1 : MonoBehaviour
{

    public GameObject gm;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (gm.gameObject.GetComponent<Enemy>().playerNoticed || gm.gameObject.GetComponent<Enemy>().trigger)
        {
            if (gm.gameObject.GetComponent<Enemy>().target.position.x > transform.position.x && gm.gameObject.GetComponent<Enemy>().facingRight && gm.gameObject.GetComponent<Enemy>().trigger)
            {
                gm.gameObject.GetComponent<Enemy>().Flip();
                gm.gameObject.GetComponent<Enemy>().facingRight = false;
                //trigger = false;
            }
            else if (gameObject.GetComponent<Enemy>().target.position.x < transform.position.x && !gameObject.GetComponent<Enemy>().facingRight && gameObject.GetComponent<Enemy>().trigger)
            {
                gameObject.GetComponent<Enemy>().Flip();
                gameObject.GetComponent<Enemy>().facingRight = true;
                //trigger = false;
            }



            if ((Vector2.Distance(transform.position, gameObject.GetComponent<Enemy>().target.position) > 1.5f && !gameObject.GetComponent<Enemy>().Patrol.gameObject.GetComponent<Patrol>().ground) && !gameObject.GetComponent<Enemy>().triggerDeath)
            {
                //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime * 2);

                //Vector3 newPos = transform.position;
                //newPos.x = Mathf.MoveTowards(newPos.x, target.position.x, speed * Time.fixedDeltaTime);
                //transform.position = newPos;
                gameObject.GetComponent<Enemy>().speed = 4;
                Vector2.Distance(transform.position, gameObject.GetComponent<Enemy>().target.transform.position);
                gameObject.GetComponent<Enemy>().anim.SetBool("attack_enemy", false);
            }
            else if (Vector2.Distance(transform.position, gameObject.GetComponent<Enemy>().target.position) <= 1.5f && !gameObject.GetComponent<Enemy>().triggerDeath)
            {
                gameObject.GetComponent<Enemy>().speed = 0;
                gameObject.GetComponent<Enemy>().trigger = false;
                if (!gameObject.GetComponent<Enemy>().anim.GetCurrentAnimatorStateInfo(0).IsName("attack_enemy"))
                {
                    gameObject.GetComponent<Enemy>().anim.SetBool("attack_enemy", true);
                }
            }

        }
        else if (!gameObject.GetComponent<Enemy>().trigger)
        {
            gameObject.GetComponent<Enemy>().speed = 3;
        }
    }
}
