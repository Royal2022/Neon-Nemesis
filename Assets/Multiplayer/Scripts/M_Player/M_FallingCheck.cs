using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_FallingCheck : MonoBehaviour
{
    public GameObject Player;
    private M_Player player;

    public Transform FallSmokePos;
    public GameObject prefabFallAnimSmoke;

    void Start()
    {
        player = Player.GetComponent<M_Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && player.rb.velocity.y < -11f)
        {
            player.Health -= (int)((float.Parse(player.rb.velocity.y.ToString()) * -1) / 5);
            //player.FractureSound.Play();
            Player.GetComponent<M_SoundPlayer>().JointSoundPlay(4);
        }
        if (collision.CompareTag("Ground") && player.rb.velocity.y < -1f)
        {
            //player.JumpSound.Play();
            Player.GetComponent<M_SoundPlayer>().JointSoundPlay(0);
            //GameObject obj = Instantiate(prefabFallAnimSmoke);
            //obj.transform.position = FallSmokePos.position;
        }
    }
}
