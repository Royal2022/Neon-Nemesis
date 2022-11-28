using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class WeaponHold : MonoBehaviour
{

    //[SerializeField] private hands _Hands;



    public bool hold;
    public float distance = 1f;
    RaycastHit2D hit;
    public Transform holdPointPistol;
    public Transform holdPointAutomaticGun;
    public float throwobject = 1;




    [SerializeField] private WeaponSwitch wp;

    void Start()
    {
        //_Hands = FindObjectOfType<hands>();
        wp = FindObjectOfType<WeaponSwitch>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!hold)
            {
                Physics2D.queriesStartInColliders = false;

                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);


                if (hit.collider != null && hit.collider.tag == "Weapon" && holdPointPistol.GetChild(0).gameObject)
                {
                    if (wp.transform.GetChild(1).gameObject.activeSelf == false)
                    {
                        wp.weaponSwitch = 1;
                        wp.SelectWeapon();
                    }
                    hold = true;
                    //Destroy(holdPoint.GetChild(0).gameObject);

                    holdPointPistol.GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, 0.64f);
                    holdPointPistol.GetChild(0).transform.position = hit.collider.gameObject.transform.position;
                    holdPointPistol.GetChild(0).parent = null;


                    FindObjectOfType<hands>().res();
                    hit.collider.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0.64f);
                    hit.collider.gameObject.transform.position = holdPointPistol.gameObject.transform.position;
                    hit.transform.parent = holdPointPistol;
                }          
                
                if (hit.collider != null && hit.collider.tag == "AK" && holdPointAutomaticGun.GetChild(0).gameObject)
                {
                    Debug.Log(0);
                    if (wp.transform.GetChild(2).gameObject.activeSelf == false)
                    {
                        wp.weaponSwitch = 2;
                        wp.SelectWeapon();
                    }
                    hold = true;
                    //Destroy(holdPoint.GetChild(0).gameObject);

                    holdPointAutomaticGun.GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, 0.64f);
                    holdPointAutomaticGun.GetChild(0).transform.position = hit.collider.gameObject.transform.position;
                    holdPointAutomaticGun.GetChild(0).parent = null;


                    FindObjectOfType<hands>().res();
                    hit.collider.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0.64f);
                    hit.collider.gameObject.transform.position = holdPointAutomaticGun.gameObject.transform.position;
                    hit.transform.parent = holdPointAutomaticGun;
                }
            }
            else
            {
                hold = false;
            }
            Physics2D.queriesStartInColliders = true;

        }
        if (hold)
        {
            if (holdPointPistol.position.x > transform.position.x && hold == true)
            {
                if (hit.collider.transform.localScale.x > 0)
                {
                    hit.collider.gameObject.transform.localScale = new Vector2(hit.collider.transform.localScale.x, hit.collider.transform.localScale.y);
                }
                if (hit.collider.transform.localScale.x < 0)
                {
                    hit.collider.gameObject.transform.localScale = new Vector2(-hit.collider.transform.localScale.x, hit.collider.transform.localScale.y);
                }
            }
            else if (holdPointPistol.position.x < transform.position.x && hold == true)
            {
                if (hit.collider.transform.localScale.x > 0)
                {
                    hit.collider.gameObject.transform.localScale = new Vector2(hit.collider.transform.localScale.x, hit.collider.transform.localScale.y);
                }
                if (hit.collider.transform.localScale.x < 0)
                {
                    hit.collider.gameObject.transform.localScale = new Vector2(-hit.collider.transform.localScale.x, hit.collider.transform.localScale.y);
                }
            }
            hold = false;
        }
    }

            
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}
