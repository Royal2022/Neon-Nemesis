using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
//using UnityEditor.PackageManager;
using UnityEngine;

public class M_Bullet : MonoBehaviourPun
{
    public float speed = 25f;
    public int damage = 1;
    public Rigidbody2D rb;


    public float distanceRayCast = 0.1f;
    public LayerMask whatIsSolid;

    [PunRPC] public GameObject sr;
    //[SerializeField] private Enemy enemy;

    public GameObject player;

    private void Start()
    {
        //sr = GetComponent<SpriteRenderer>();
        if (!photonView.IsMine) return;
        if (M_Player.facingRight)
        {
            rb.velocity = transform.right * speed;
        }
        else if (!M_Player.facingRight)
        {
            rb.velocity = (transform.right * -1) * speed;
            gameObject.GetPhotonView().RPC("unwrap", RpcTarget.AllBuffered);
        }

        Invoke("DestroyBullet", 2f);
    }

    void Update()
    {
        if (!photonView.IsMine) return;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distanceRayCast, whatIsSolid);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Player"))
            {
                hitInfo.collider.gameObject.GetPhotonView().RPC("TakeDamage", RpcTarget.All, damage, hitInfo.collider.gameObject.GetPhotonView().ViewID);
            }

            if (!photonView.IsMine) return;
                PhotonNetwork.Destroy(gameObject);

        }

    }
    void DestroyBullet()
    {
        if (photonView.IsMine)
            PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    public void unwrap()
    {
        PhotonView.Find(sr.gameObject.GetPhotonView().ViewID).gameObject.GetComponent<SpriteRenderer>().flipX = true;
        //sr.flipX = true;
    }




}
