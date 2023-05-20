using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_WeaponHold : NetworkBehaviour
{
    public bool hold;
    public float distanceRayCast = 1f;
    RaycastHit2D hit;
    public Transform holdPointPistol;
    public Transform holdPointAutomaticGun;
    public float throwobject = 2f;


    public LayerMask whatIsSolid;


    //[SerializeField] private M_WeaponSwitch wp;

    //public GameObject wp;
    public M_WeaponSwitch wp;
    public GameObject WeaponHands;

    void Start()
    {

        //_Hands = FindObjectOfType<hands>();
    }


    void Update()
    {
        if (!isLocalPlayer) return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeInHandPistol();
        }
    }




    [ClientRpc]
    public void RpcTakeInHandPistol()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distanceRayCast, whatIsSolid);


        if (hit.collider != null && hit.collider.tag == "Weapon" && holdPointPistol.GetChild(0).gameObject)
        {
            wp.weaponSwitch = 1;
            wp.SetActive(1);

            hold = true;

            holdPointPistol.GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, 0.64f);
            holdPointPistol.GetChild(0).gameObject.GetComponent<Rigidbody2D>().simulated = true;
            holdPointPistol.GetChild(0).gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwobject;
            holdPointPistol.GetChild(0).parent = null;

            hit.collider.GetComponent<Rigidbody2D>().simulated = false;

            //FindObjectOfType<hands>().res();
            WeaponHands.gameObject.transform.GetChild(0).GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
            hit.collider.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0.64f);
            holdPointPistol.transform.rotation = Quaternion.Euler(0f, 0f, 0.64f);
            hit.collider.gameObject.transform.position = holdPointPistol.gameObject.transform.position;
            hit.transform.parent = holdPointPistol;
        }


        if (hit.collider != null && hit.collider.tag == "AK" && holdPointAutomaticGun.GetChild(0).gameObject)
        {
            wp.weaponSwitch = 2;
            wp.SetActive(2);

            hold = true;

            holdPointAutomaticGun.GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, 0.64f);
            holdPointAutomaticGun.GetChild(0).gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwobject;
            holdPointAutomaticGun.GetChild(0).gameObject.GetComponent<Rigidbody2D>().simulated = true;
            holdPointAutomaticGun.GetChild(0).parent = null;
            hit.collider.GetComponent<Rigidbody2D>().simulated = false;


            WeaponHands.gameObject.transform.GetChild(1).GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
            hit.collider.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0.64f);
            holdPointAutomaticGun.transform.rotation = Quaternion.Euler(0f, 0f, 0.64f);
            hit.collider.gameObject.transform.position = holdPointAutomaticGun.gameObject.transform.position;
            hit.transform.parent = holdPointAutomaticGun;
        }



        if (holdPointPistol.position.x > transform.position.x && hit.collider != null)
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
        else if (holdPointPistol.position.x < transform.position.x && hit.collider != null)
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


    }

    public void TakeInHandPistol()
    {
        if (isServer)
        {
            RpcTakeInHandPistol();
        }
        else if (isClient)
        {
            CmdTakeInHandPistol();
        }
    }

    [Command]
    public void CmdTakeInHandPistol()
    {
        RpcTakeInHandPistol();
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distanceRayCast);
    }
}
