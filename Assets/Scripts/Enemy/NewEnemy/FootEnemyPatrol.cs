using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootEnemyPatrol : MonoBehaviour
{
    RaycastHit2D hit_straight, hit_down;

    public float distance = 0.6f;

    private Enemy enemy;

    private LayerMask whatIsGround;


    private void Start()
    {
        enemy = transform.parent.GetComponent<Enemy>();
        whatIsGround = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        hit_down = Physics2D.Raycast(transform.position, Vector2.down, distance, whatIsGround);
        hit_straight = Physics2D.Raycast(transform.position, Vector3.right * enemy.transform.localScale.x, distance, whatIsGround);

        if (!enemy.CanJump)
        {
            if (hit_down.collider == null && enemy.isGround)
            enemy.Flip();

            if (hit_straight.collider != null)
            enemy.Flip();
        }
        else
        {
            if (hit_down.collider == null && enemy.isGround && !enemy.playerNoticed && !enemy.IwasHit)            
                enemy.Flip();            
            else if (hit_down.collider == null && enemy.isGround && (enemy.playerNoticed || enemy.IwasHit) && !enemy.triggerDeath)           
                enemy.JumpEnemy();            

            if (hit_straight.collider != null && !enemy.playerNoticed && !enemy.IwasHit)            
                enemy.Flip();   
            else if (hit_straight.collider != null && enemy.isGround && (enemy.playerNoticed || enemy.IwasHit) && !enemy.triggerDeath)            
                enemy.JumpEnemy();            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (enemy != null)
        {
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distance);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * enemy.transform.localScale.x * distance);
        }
    }
}
