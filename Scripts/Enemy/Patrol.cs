using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

    public float distance = 3f;


    private bool movingRight = true;

    RaycastHit2D hit;

    //[SerializeField] private Enemy enemy;

    public bool hold;

    //public Transform groundDetection;

    /*
    private bool isGrounded;
    public LayerMask whatIsGround;
    public float checkRaduis;
    public Transform feetPos;
    public float jumpForce;*/

    [SerializeField] private feetPosEnemy fPE;

    public GameObject enemy;

    void Start()
    {
        //enemy = FindObjectOfType<Enemy>();
        fPE = FindObjectOfType<feetPosEnemy>(); 
    }

    void Update()
    {

        //Debug.Log(isGrounded);
        //isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRaduis, whatIsGround);

        if (!hold)
        {
            Physics2D.queriesStartInColliders = false;

            hit = Physics2D.Raycast(transform.position, Vector3.down * transform.localScale.x, distance);
            if (hit.collider != null && hit.collider.tag == "Ground")
            {
                hold = true;
                //Debug.Log(hit.collider.gameObject.tag);
                //enemy.gameObject.GetComponent<Transform>().localScale = new Vector3(enemy.transform.localScale.x, enemy.transform.localScale.y, enemy.transform.localScale.z);
            }
            if (enemy.gameObject.GetComponent<Enemy>().playerNoticed == true && hit.collider == null && enemy.gameObject.GetComponent<Enemy>().isGrounded == true /*&& (!fPE.playerNoticedLegs || (FindObjectOfType<head>().playerNoticedHead && FindObjectOfType<Enemy>().playerNoticed))*/)
            {
                enemy.gameObject.GetComponent<Enemy>().JumpEnemy();
                enemy.gameObject.GetComponent<Enemy>().speed = 3;
            }
            if (hit.collider == null && enemy.gameObject.GetComponent<Enemy>().isGrounded == true && !enemy.gameObject.GetComponent<Enemy>().playerNoticed)
            {
                enemy.gameObject.GetComponent<Enemy>().Flip();
                Debug.Log(0);
                Debug.Log(enemy.gameObject.GetComponent<Enemy>().isGrounded);
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
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * transform.localScale.x * distance);
    }

}
