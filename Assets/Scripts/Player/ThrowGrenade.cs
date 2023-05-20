using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class ThrowGrenade : MonoBehaviour
{

    /*=========Grenade==========*/
    public GameObject Grenade;
    private float PowerThrow = 5;
    public Transform SpawnGrenade;
    public GameObject WeaponHands;
    public GameObject HandPoint;

    private float timeDropGranade;
    public float startTimeDropGranade = 2f;

    public Image TimerImage;
    /*==========================*/


    private Player player;

    private WeaponHold weaponHold;

    void Start()
    {
        player = GetComponent<Player>();
        weaponHold = GetComponent<WeaponHold>();
    }

    void Update()
    {
        /*=========Grenade==========*/
        if (timeDropGranade <= 0 && Player.NumberOfGrenades > 0)
        {
            if (Input.GetKeyDown(KeyCode.G) && player.isGround && !PlayingOrNotAnim("dropGrenade") && !player.anim.GetBool("throwGrenade")
            && !PlayingOrNotAnim("Run")
            && !PlayingOrNotAnim("jump") && !PlayingOrNotAnim("sault")
            && !PlayingOrNotAnim("run_attack") && !PlayingOrNotAnim("attack") && !player.anim.GetBool("player_jump")
            && !PlayingOrNotAnim("ZipLine") && !PlayingOrNotAnim("idleZipLine")
            && !weaponHold.HandsAnim.GetBool("reload") && !weaponHold.AutomaticGunHandsAnim.GetBool("reload"))
            {
                player.anim.SetTrigger("throwGrenadeTrigger");
                player.anim.Play("idle_dropGrenade");
                WeaponHands.SetActive(false);
                HandPoint.SetActive(true);
                player.rb.velocity = new Vector2(0, 0);
            }
            if (Input.GetKeyUp(KeyCode.G) && PlayingOrNotAnim("idle_dropGrenade")
                && !PlayingOrNotAnim("Run")
                && !PlayingOrNotAnim("jump") && !PlayingOrNotAnim("sault")
                && !PlayingOrNotAnim("run_attack") && !PlayingOrNotAnim("attack") && !player.anim.GetBool("player_jump"))
            {
                player.anim.SetBool("throwGrenade", true);
                timeDropGranade = startTimeDropGranade;
            }

            if (Input.GetKey(KeyCode.G) && PlayingOrNotAnim("idle_dropGrenade"))
            {
                PowerThrow += 2 * Time.deltaTime;
                if (PowerThrow > 10)
                    PowerThrow = 10;
            }
        }
        else
        {
            timeDropGranade -= Time.deltaTime;
            var normalizedValue = Mathf.Clamp(timeDropGranade / startTimeDropGranade, 0.0f, 1.0f);
            TimerImage.fillAmount = normalizedValue;
        }
        /*==========================*/
    }


    public bool PlayingOrNotAnim(string name)
    {
        return player.anim.GetCurrentAnimatorStateInfo(0).IsName($"{name}");
    }
    public void WeaponHandsDisabled()
    {
        WeaponHands.SetActive(false);
        HandPoint.SetActive(false);
    }

    /*=========Grenade==========*/
    public void Throw_Grenade()
    {
        Instantiate(Grenade, SpawnGrenade.position, transform.rotation).GetComponent<grenade>().powerThrow = PowerThrow;
        PowerThrow = 5;
        player.anim.ResetTrigger("throwGrenadeTrigger");
        player.anim.SetBool("throwGrenade", false);
        WeaponHands.SetActive(true);
        HandPoint.SetActive(false);
        Player.NumberOfGrenades -= 1;
    }
    /*==========================*/
}
