using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_GrenadeItem : NetworkBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            M_ThrowGrenade throwGranade = collision.gameObject.GetComponent<M_ThrowGrenade>();
            if (throwGranade.NumberOfGrenades < 3)
            {
                throwGranade.NumberOfGrenades += 1;
                throwGranade.OutTextInfo();
            }
            NetworkServer.Destroy(gameObject);
        }
    }
}
