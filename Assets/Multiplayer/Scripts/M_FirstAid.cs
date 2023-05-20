using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_FirstAid : NetworkBehaviour
{

    public GameObject EffectFirstAid;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<M_Player>().SpawnChildEffectFirstAid();

            collision.gameObject.GetComponent<M_Player>().Health += 5;
            if (collision.gameObject.GetComponent<M_Player>().Health > collision.gameObject.GetComponent<M_Player>().healthSlider.maxValue)
            {
                collision.gameObject.GetComponent<M_Player>().Health = (int)collision.gameObject.GetComponent<M_Player>().healthSlider.maxValue;
            }
            NetworkServer.Destroy(gameObject);
        }
    }



}
