using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (Player.NumberOfGrenades < 3)
            {
                Player.NumberOfGrenades += 1;
                //collision.collider.GetComponent<Player>().MoneySound.Play();
            }
            Destroy(gameObject);
        }
    }
}
