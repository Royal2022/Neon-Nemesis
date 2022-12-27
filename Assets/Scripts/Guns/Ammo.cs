using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.tag == "pistol2")
        {
            Destroy(gameObject);
            AutomaticGun.allAmmo += 35;
        }
        else if (collision.CompareTag("Player") && gameObject.tag == "pistol1")
        {
            Destroy(gameObject);
            Pistol.allAmmo += 15;
            Debug.Log("C");
        }
    }*/

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (gameObject.tag == "pistol1")
    //        if (collision.CompareTag("Player"))
    //        {
    //            Destroy(gameObject);
    //            Player.pistol_ammo += 15;
    //        }
    //    if (gameObject.tag == "pistol2")
    //        if (collision.CompareTag("Player"))
    //        {
    //            Destroy(gameObject);
    //            Player.automaticGun_ammo += 35;
    //        }

    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "pistol1")
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
                Player.pistol_ammo += 15;
            }
        if (gameObject.tag == "pistol2")
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
                Player.automaticGun_ammo += 35;
            }
    }
}
