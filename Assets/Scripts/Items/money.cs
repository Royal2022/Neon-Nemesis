using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class money : MonoBehaviour
{
    public int MoneyAmount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.money += MoneyAmount;
            collision.collider.GetComponent<Player>().MoneySound.Play();
            Destroy(gameObject);
        }
    }
}
