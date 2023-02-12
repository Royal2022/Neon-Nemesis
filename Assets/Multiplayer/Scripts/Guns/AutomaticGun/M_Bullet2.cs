using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.PackageManager;
using UnityEngine;

public class M_Bullet2 : MonoBehaviourPun
{
    public float speed = 25f;
    public int damage = 1;
    public Rigidbody2D rb;


    public float distanceRayCast = 0.1f;
    public LayerMask whatIsSolid;

     SpriteRenderer sr;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (Player.facingRight == true)
        {
            rb.velocity = transform.right * speed;
        }
        else if (Player.facingRight == false)
        {
            rb.velocity = (transform.right * -1) * speed;
        }
        Invoke("DestroyBullet", 2f);

    }


    void Update()
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distanceRayCast, whatIsSolid);

        
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                DestroyBullet();
            }
            if (hitInfo.collider.CompareTag("EnemyHead"))
            {
                hitInfo.collider.GetComponent<head>().enemy.gameObject.GetComponent<Enemy>().TakeDamage(damage*2);
                DestroyBullet();
            }
            DestroyBullet();        
        }
    }
    void DestroyBullet()
    {
        if (!photonView.IsMine) return;
            PhotonNetwork.Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        if (Player.facingRight == false)
        {
            sr.flipX = true;
        }
    }
}
