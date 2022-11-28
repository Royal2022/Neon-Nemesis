using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;
    public float speed;
    public float distance = 1f;
    RaycastHit2D head;


    private void Update()
    {
        Physics2D.queriesStartInColliders = false;

        head = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.z, distance);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
    }


}
