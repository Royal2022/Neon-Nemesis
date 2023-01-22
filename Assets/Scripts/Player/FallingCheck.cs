using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCheck : MonoBehaviour
{
    [SerializeField] private Player player;


    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && player.rb.velocity.y < -8.5f)
        {
            //player.health -= (int)((float.Parse(player.rb.velocity.y.ToString()) * -1)/3);
            //player.TakeDamage((int)((float.Parse(player.rb.velocity.y.ToString()) * -1) / 3));
            player.health -= (int)((float.Parse(player.rb.velocity.y.ToString()) * -1) / 3);
        }
    }



}
