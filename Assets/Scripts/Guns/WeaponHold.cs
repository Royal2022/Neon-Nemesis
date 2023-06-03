using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class WeaponHold : MonoBehaviour
{
    public float distance = 1f;
    private RaycastHit2D hit;
    public Transform holdPointPistol;
    public Transform holdPointAutomaticGun;
    public float throwobject = 2f;

    public GameObject WeaponSwitch;
    private WeaponSwitch wp;

    public GameObject Hands;
    public GameObject AutomaticGunHands;
    [HideInInspector]
    public Animator HandsAnim;
    [HideInInspector]
    public Animator AutomaticGunHandsAnim;


    public LayerMask whatIsSolid;


    public GameObject gun_hands;
    public GameObject automatic_gun_hands;

    void Start()
    {
        wp = WeaponSwitch.GetComponent<WeaponSwitch>();
        HandsAnim = Hands.GetComponent<Animator>();
        AutomaticGunHandsAnim = AutomaticGunHands.GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (HandsAnim.gameObject.activeSelf)
            { 
                if (!HandsAnim.GetBool("reload"))
                    TakeWeapon();
            }
            else if (AutomaticGunHandsAnim.gameObject.activeSelf)
            {
                if (!AutomaticGunHandsAnim.GetBool("reload"))
                    TakeWeapon();
            }
            else if (!HandsAnim.gameObject.activeSelf && !AutomaticGunHandsAnim.gameObject.activeSelf)
                TakeWeapon();
        }
    }


    private void TakeWeapon()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, whatIsSolid);

        if (hit.collider != null && hit.collider.CompareTag("Weapon") /*&& holdPointPistol.GetChild(0).gameObject*/)
        {
            wp.handIsNotEmpty[1] = true;
            wp.weaponSwitch = 1;
            wp.SelectWeapon(1);


            if (holdPointPistol.GetChild(0).childCount > 0)
            {
                holdPointPistol.GetChild(0).GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, 0.64f);
                holdPointPistol.GetChild(0).GetChild(0).gameObject.GetComponent<Rigidbody2D>().simulated = true;
                holdPointPistol.GetChild(0).GetChild(0).gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwobject;
                holdPointPistol.GetChild(0).GetChild(0).parent = null;
            }

            hit.collider.GetComponent<Rigidbody2D>().simulated = false;

            hit.collider.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0.64f);
            hit.collider.gameObject.transform.position = holdPointPistol.gameObject.transform.position;
            hit.transform.parent = holdPointPistol.GetChild(0);
            FlipWeapon(holdPointPistol.GetChild(0).GetChild(0).gameObject);

            Hands.GetComponent<hands>().res();
        }

        if (hit.collider != null && hit.collider.CompareTag("AK") /*&& holdPointAutomaticGun.GetChild(0).GetChild(0).gameObject*/)
        {
            wp.handIsNotEmpty[2] = true;
            wp.weaponSwitch = 2;
            wp.SelectWeapon(2);


            if (holdPointAutomaticGun.GetChild(0).childCount > 0)
            {
                holdPointAutomaticGun.GetChild(0).GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                holdPointAutomaticGun.GetChild(0).GetChild(0).gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwobject;
                holdPointAutomaticGun.GetChild(0).GetChild(0).gameObject.GetComponent<Rigidbody2D>().simulated = true;
                holdPointAutomaticGun.GetChild(0).GetChild(0).parent = null;
            }

            hit.collider.GetComponent<Rigidbody2D>().simulated = false;

            hit.collider.gameObject.transform.position = holdPointAutomaticGun.gameObject.transform.position;
            hit.collider.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            hit.transform.parent = holdPointAutomaticGun.GetChild(0);
            FlipWeapon(holdPointAutomaticGun.GetChild(0).GetChild(0).gameObject);

            AutomaticGunHands.GetComponent<HandsAutomaticGun>().res();
        }

        if (hit.collider == null)
        {
            if (gun_hands.activeSelf)
            {
                wp.weaponSwitch = 0;
                wp.SelectWeapon(0);
                wp.handIsNotEmpty[1] = false;

                holdPointPistol.GetChild(0).GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, 0.64f);
                holdPointPistol.GetChild(0).GetChild(0).gameObject.GetComponent<Rigidbody2D>().simulated = true;
                holdPointPistol.GetChild(0).GetChild(0).gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwobject;
                GameObject obj = holdPointPistol.GetChild(0).GetChild(0).gameObject;
                holdPointPistol.GetChild(0).GetChild(0).parent = null;
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwobject;
            }
            else if (automatic_gun_hands.activeSelf)
            {
                wp.weaponSwitch = 0;
                wp.SelectWeapon(0);
                wp.handIsNotEmpty[2] = false;

                holdPointAutomaticGun.GetChild(0).GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                holdPointAutomaticGun.GetChild(0).GetChild(0).gameObject.GetComponent<Rigidbody2D>().simulated = true;
                holdPointAutomaticGun.GetChild(0).GetChild(0).gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwobject;
                GameObject obj = holdPointAutomaticGun.GetChild(0).GetChild(0).gameObject;
                holdPointAutomaticGun.GetChild(0).GetChild(0).parent = null;
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwobject;
            }
        }
    }

    private void FlipWeapon(GameObject Weapon)
    {
        if (Player.facingRight)
        {
            if (Weapon.transform.localScale.x > 0)            
                Weapon.transform.localScale = new Vector2(Weapon.transform.localScale.x, Weapon.transform.localScale.y);            
            if (Weapon.transform.localScale.x < 0)            
                Weapon.transform.localScale = new Vector2(-Weapon.transform.localScale.x, Weapon.transform.localScale.y);            
        }
        else if (!Player.facingRight)
        {
            if (Weapon.transform.localScale.x > 0)            
                Weapon.transform.localScale = new Vector2(Weapon.transform.localScale.x, Weapon.transform.localScale.y);            
            if (Weapon.transform.localScale.x < 0)            
                Weapon.transform.localScale = new Vector2(-Weapon.transform.localScale.x, Weapon.transform.localScale.y);            
        }
    }
          
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}
