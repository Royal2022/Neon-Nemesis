using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Ammo_Pistol : MonoBehaviourPun
{
    public int quantityAmmo = 15;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.root.gameObject.GetComponent<M_Player>().IsItYou)
            {
                M_Player.pistol_AllAmmo += quantityAmmo;
            }
            if (!photonView.IsMine) return;
                PhotonNetwork.Destroy(gameObject);
        }
    }
}

