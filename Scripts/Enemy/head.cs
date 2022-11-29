using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class head : MonoBehaviour
{
    /*
   RaycastHit2D hit;
   bool facingRight = true;
   public bool playerNoticed = false;
   public float distance = 20f;


   public bool hold = false;


   void Update()
   {
       if (!hold)
       {
           Physics2D.queriesStartInColliders = false;
           hit = Physics2D.Raycast(transform.position, Vector2.right * FindObjectOfType<Enemy>().transform.localScale.x, distance);


           if (hit.collider != null && hit.collider.tag == "Player")
           {
               Debug.Log("true0");
           }
           if (hit.collider != null)
           {
               Debug.Log(hit.collider.gameObject.tag);
           }


       }
       Physics2D.queriesStartInColliders = true;

   }
   private void OnDrawGizmos()
   {
       Gizmos.color = Color.red;
       Gizmos.DrawLine(transform.position, transform.position + Vector3.right * FindObjectOfType<Enemy>().transform.localScale.x * distance);
   }
   */



    RaycastHit2D hit, hitBack;
    public LayerMask SeePlayer;
    public float distance = 6f;
    public float distanceBack = 1.5f;

    //[SerializeField] private Enemy enemy;
    public GameObject enemy;

    public bool playerNoticedHead = false;

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
            playerNoticedHead = true;
            
            if (!enemy.gameObject.GetComponent<Enemy>().playerNoticed)
            {
                enemy.gameObject.GetComponent<Enemy>().playerNoticed = true;
            }
        }
        else if (hit.collider == null)
        {
            playerNoticedHead = false;
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

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * enemy.gameObject.GetComponent<Transform>().localScale.x * distance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * enemy.gameObject.GetComponent<Transform>().localScale.x * distanceBack);

    }

}
