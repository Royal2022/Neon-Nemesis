using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideOfTheFrame : MonoBehaviour
{
    public Transform SpawnPoint;
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
            if (collision.GetComponent<Rigidbody2D>().simulated)
            {
                Player player = collision.GetComponent<Player>();
                player.health = 0;
                player.death = true;
                player.rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
        }
    }


    public void OnClickReplay()
    {
        player.transform.position = SpawnPoint.position;
        _Player.health = 20;
        player.SetActive(true);
        _ThrowGrenade.WeaponHands.SetActive(true);
        _ThrowGrenade.WeaponHands.GetComponent<WeaponSwitch>().SelectWeapon(0);
        _Player.death = false;
        _Player.DeathCanvas.SetActive(false);
        _Player.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _Player.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}