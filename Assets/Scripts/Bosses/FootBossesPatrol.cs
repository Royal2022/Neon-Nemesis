using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootBossesPatrol : MonoBehaviour
{
    RaycastHit2D hit_straight, hit_down;

    public float distance = 0.6f;

    private Bosses bosses;

    private LayerMask whatIsGround;


    private void Start()
    {
        bosses = transform.parent.GetComponent<Bosses>();
        whatIsGround = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        hit_down = Physics2D.Raycast(transform.position, Vector2.down, distance, whatIsGround);
        hit_straight = Physics2D.Raycast(transform.position, Vector3.right * bosses.transform.localScale.x, distance, whatIsGround);

        if (!bosses.CanJump)
        {
            if (hit_down.collider == null && bosses.isGround)
                bosses.Flip();

            if (hit_straight.collider != null)
                bosses.Flip();
        }
        else
        {
            if (hit_down.collider == null && bosses.isGround && !bosses.playerNoticed && !bosses.IwasHit)            
                bosses.Flip();            
            else if (hit_down.collider == null && bosses.isGround && (bosses.playerNoticed || bosses.IwasHit) && !bosses.triggerDeath)            
                bosses.JumpBosses();            

            if (hit_straight.collider != null && !bosses.playerNoticed && !bosses.IwasHit)
                bosses.Flip();
            else if (hit_straight.collider != null && bosses.isGround && (bosses.playerNoticed || bosses.IwasHit) && !bosses.triggerDeath)
                bosses.JumpBosses();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (bosses != null)
        {
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distance);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * bosses.transform.localScale.x * distance);
        }
    }
}
