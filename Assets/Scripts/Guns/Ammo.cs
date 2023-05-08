using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().AmmoSound.Play();
            if (gameObject.CompareTag("pistol1"))
            {
                Player.pistol_ammo += 15;
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("pistol2"))
            {
                Player.automaticGun_ammo += 35;
                Destroy(gameObject);
            }
        }
    }
}
