using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feetPosEnemy : MonoBehaviour
{
    RaycastHit2D hit, hitBack;
    public LayerMask SeePlayer;
    public float distance = 6f;
    public float distanceBack = 1.5f;
    
    public bool playerNoticedLegs = false;

    public GameObject enemy;

    public bool hold;

    private void Update()
    {
        if (!hold)
        {
            Physics2D.queriesStartInColliders = false;

            hit = Physics2D.Raycast(transform.position, Vector3.right * enemy.gameObject.GetComponent<Transform>().localScale.x, distance, SeePlayer);
            if (hit.collider != null && hit.collider.gameObject.tag == "Player")
            {
                playerNoticedLegs = true;
                hold = true;
                if (!enemy.gameObject.GetComponent<Enemy>().playerNoticed)
                {
                    enemy.gameObject.GetComponent<Enemy>().playerNoticed = true;
                }
            }
            else if (hit.collider == null)
            {
                playerNoticedLegs = false;
            }

            hitBack = Physics2D.Raycast(transform.position, Vector3.left * enemy.gameObject.GetComponent<Transform>().localScale.x, distanceBack, SeePlayer);
            if (hitBack.collider != null && hitBack.collider.gameObject.tag == "Player")
            {
                enemy.gameObject.GetComponent<Enemy>().Flip();
            }
        }
        else
        {
            hold = false;
        }
        Physics2D.queriesStartInColliders = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * enemy.gameObject.GetComponent<Transform>().localScale.x * distance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * enemy.gameObject.GetComponent<Transform>().localScale.x * distanceBack);
    }
}
