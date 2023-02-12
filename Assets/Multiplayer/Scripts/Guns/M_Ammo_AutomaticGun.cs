using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Ammo_AutomaticGun : MonoBehaviourPun
{
    public int quantityAmmo = 35;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.root.gameObject.GetComponent<M_Player>().IsItYou)
            {
                M_Player.automaticGun_AllAmmo += quantityAmmo;
            }
            if (!photonView.IsMine) return;
                PhotonNetwork.Destroy(gameObject);
        }
    }
}
