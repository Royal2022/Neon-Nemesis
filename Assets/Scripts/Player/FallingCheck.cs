using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCheck : MonoBehaviour
{
    public GameObject Player;
    private Player player;

    void Start()
    {
        player = Player.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && player.rb.velocity.y < -8.5f)
        {
            player.health -= (int)((float.Parse(player.rb.velocity.y.ToString()) * -1) / 3);
            player.FractureSound.Play();
        }
        if (collision.CompareTag("Ground") && player.rb.velocity.y < -1f)
            player.JumpSound.Play();
    }
}
