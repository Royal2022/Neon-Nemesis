using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_armor : MonoBehaviourPun
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<M_Player>().IsItYou)
                M_Player.armor += 20;

            if (M_Player.armor > collision.gameObject.GetComponent<M_Player>().OutText.armorSlider.maxValue)
            {
                if (collision.gameObject.GetComponent<M_Player>().IsItYou)
                    M_Player.armor = (int)collision.gameObject.GetComponent<M_Player>().OutText.armorSlider.maxValue;
            }
            if (!photonView.IsMine) return;
                PhotonNetwork.Destroy(gameObject);
        }
    }
}
