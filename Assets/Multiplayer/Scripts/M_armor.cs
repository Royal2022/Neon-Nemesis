using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Armor : NetworkBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            M_Player player = collision.gameObject.GetComponent<M_Player>();

            player.Armor += 10;

            if (player.Armor > collision.gameObject.GetComponent<M_Player>().OutText.armorSlider.maxValue)
            {
                if (collision.gameObject.GetComponent<M_Player>().IsItYou)
                    player.Armor = (int)collision.gameObject.GetComponent<M_Player>().OutText.armorSlider.maxValue;
            }

            NetworkServer.Destroy(gameObject);
        }
    }
}
