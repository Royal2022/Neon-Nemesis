using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingWithWater : MonoBehaviour
{
    public float Force = 3f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Player>().rb.velocity = Vector2.up * Force;
            }
            else if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Enemy>().rb.velocity = Vector2.up * Force;
            }
        }
    }
}
