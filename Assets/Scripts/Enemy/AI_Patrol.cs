using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AI_Patrol : MonoBehaviour
{
    RaycastHit2D hit_straight, hit_down;

    public float distance = 0.6f;

    public GameObject Enemy;
    private Enemy enemy;

    public LayerMask whatIsGround;


    private void Start()
    {
        enemy = Enemy.GetComponent<Enemy>();
    }

    private void Update()
    {
        if (enemy.CanJump)
        {
            hit_down = Physics2D.Raycast(transform.position, Vector2.down, distance, whatIsGround);
            if (hit_down.collider == null && enemy.isGround && !enemy.playerNoticed && !enemy.IwasHit)
            {
                enemy.Flip();
            }
            else if (hit_down.collider == null && enemy.isGround && (enemy.playerNoticed || enemy.IwasHit) && !enemy.triggerDeath)
            {
                enemy.JumpEnemy();
            }

            hit_straight = Physics2D.Raycast(transform.position, Vector3.right * enemy.gameObject.transform.localScale.x, distance, whatIsGround);
            if (hit_straight.collider != null && !enemy.playerNoticed && !enemy.IwasHit)
            {
                enemy.Flip();
            }
            else if (hit_straight.collider != null && enemy.isGround && (enemy.playerNoticed || enemy.IwasHit) && !enemy.triggerDeath)
            {
                enemy.JumpEnemy();
            }
        }
        else if (!enemy.CanJump)
        {
            hit_down = Physics2D.Raycast(transform.position, Vector2.down, distance, whatIsGround);
            if (hit_down.collider == null && enemy.isGround)
            {
                enemy.Flip();
            }

            hit_straight = Physics2D.Raycast(transform.position, Vector3.right * enemy.gameObject.transform.localScale.x, distance, whatIsGround);
            if (hit_straight.collider != null /*&& !enemy.playerNoticed && !enemy.IwasHit*/ && !hit_straight.collider.CompareTag("Player"))
            {
                enemy.Flip();
            }

        }

    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(transform.position, transform.position + Vector3.down * transform.localScale.x * distance);
    //    Gizmos.DrawLine(transform.position, transform.position + Vector3.right * enemy.gameObject.transform.localScale.x * distance);
    //}
}
