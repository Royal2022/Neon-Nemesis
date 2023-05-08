using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideOfTheFrame : MonoBehaviour
{
    public Transform SpawnPoint;
    private float time;
    private bool death;
    public GameObject player;

    private Player _Player;
    private ThrowGrenade _ThrowGrenade;


    private void Start()
    {
        _Player = player.GetComponent<Player>();
        _ThrowGrenade = player.GetComponent<ThrowGrenade>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.health = 0;
            time = 3;
            death = true;
        }
    }


    public void OnClickReplay()
    {
        player.transform.position = SpawnPoint.position;
        _Player.health = 30;
        player.SetActive(true);
        _ThrowGrenade.WeaponHands.SetActive(true);
        death = false;
        _Player.DeathCanvas.SetActive(false);
    }
}