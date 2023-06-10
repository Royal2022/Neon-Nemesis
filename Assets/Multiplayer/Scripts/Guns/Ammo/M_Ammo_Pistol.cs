using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Ammo_Pistol : NetworkBehaviour
{
    public int quantityAmmo = 15;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.root.gameObject.GetComponent<M_Player>().IsItYou)
            {
                M_Player.pistol_AllAmmo += quantityAmmo;
                collision.GetComponent<M_SoundPlayer>().JointSoundPlay(1);
            }
            NetworkServer.Destroy(gameObject);
        }
    }
}
