using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class money : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.money += 1;
            Destroy(gameObject);
        }
    }
}
