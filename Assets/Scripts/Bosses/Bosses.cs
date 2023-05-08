using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Bosses : MonoBehaviour
{
    public float health;
    public float speed;
    public bool triggerDeath;

    private Rigidbody2D rb;
    private Animator anim;

    private RaycastHit2D hit;
    public float distancehit;
    public LayerMask SeePlayer;

    public GameObject TNTPrefab;
    public Transform TNTSpawnPosition;
    public float PowerThrow = 5f;

    public GameObject BulletPrefab;
    public Transform BulletSpawnPosition;

    public bool facingRight = true;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (transform.localScale.x > 0 && !triggerDeath && speed > 0)
        {
            transform.Translate(Vector2.left * -1 * speed * Time.deltaTime);
        }
        else if (transform.localScale.x < 0 && !triggerDeath && speed > 0)
        {
            transform.Translate(Vector2.left * 1 * speed * Time.deltaTime);
        }

        if (speed > 0)
            anim.SetBool("run", true);
        else anim.SetBool("run", false);


        hit = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, distancehit, SeePlayer);
        if (hit.collider != null && hit.collider.gameObject.tag == "Player")
        {
            anim.SetInteger("attack", SelectAttack());
            speed = 0;
        }
        else
        {
            anim.SetInteger("attack", 0);
            speed = 3;
            selectattack = false;
        }

    }

    private bool selectattack;
    private int SaveSelectAttack;
    private int SelectAttack()
    {
        if (!selectattack)
        {
            selectattack = true;
            SaveSelectAttack = Random.Range(1, 5);
            return SaveSelectAttack;
        }
        else
            return SaveSelectAttack;
    }
    public void ThrowTNT()
    {
        SaveSelectAttack = Random.Range(2, 4);
        Instantiate(TNTPrefab, TNTSpawnPosition.position, transform.rotation).GetComponent<grenade>().powerThrow = PowerThrow;
        PowerThrow = 5;
    }
    public void Sneer()
    {
        SaveSelectAttack = Random.Range(1, 4);
    }
    public void Shot()
    {
        BulletPrefab.GetComponent<bullet_Enemy>().Direction = (int)gameObject.transform.localScale.x;
        Instantiate(BulletPrefab, BulletSpawnPosition.position, transform.rotation);
    }

    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 Scaler = gameObject.transform.localScale;
        Scaler.x *= -1;
        gameObject.transform.localScale = Scaler;
    }

    public void TakeDamge(float damage)
    {
        health -= damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distancehit);
    }
}
