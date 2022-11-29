using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feetPosEnemy : MonoBehaviour
{
    RaycastHit2D hit, hitBack;
    public LayerMask SeePlayer;
    public float distance = 6f;
    public float distanceBack = 1.5f;

    //[SerializeField] private Enemy enemy; 
    
    public bool playerNoticedLegs = false;

    public GameObject enemy;

    private void Start()
    {
        //enemy = FindObjectOfType<Enemy>();
    }

    private void Update()
    {



        Physics2D.queriesStartInColliders = false;


        hit = Physics2D.Raycast(transform.position, Vector3.right * enemy.gameObject.GetComponent<Transform>().localScale.x, distance, SeePlayer);
        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            playerNoticedLegs = true;
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

    
    private void OnDrawGizmos()
    {
        //enemy = FindObjectOfType<Enemy>();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * enemy.gameObject.GetComponent<Transform>().localScale.x * distance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * enemy.gameObject.GetComponent<Transform>().localScale.x * distanceBack);

    }
}
