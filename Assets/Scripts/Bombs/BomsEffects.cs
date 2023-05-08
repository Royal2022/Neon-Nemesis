using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BomsEffects : MonoBehaviour
{
    public float explosionForce = 150;
    public float explosionRadius = 3;
    public float upwardsModifier = 100;
    public float Damege = 3;

    private List<GameObject> AllPlayerGranadeRadius = new List<GameObject>();
    private List<GameObject> AllEnemyGranadeRadius = new List<GameObject>();


    void Start()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D col in colliders)
        {
            Rigidbody2D rb2 = col.GetComponent<Rigidbody2D>();
            if (rb2 != null)
            {
                if (col.CompareTag("Player"))
                {         
                    if (!col.GetComponent<Player>().ImInGrenadeRadius)
                    {
                        if (!col.GetComponent<Player>().isGround)
                        {
                            Explode(rb2, explosionForce / 2);
                            col.GetComponent<Player>().TakeDamage(Damege, true, transform.position);
                            col.GetComponent<Player>().StartStun();
                        }
                        else
                        {
                            Explode(rb2, explosionForce);
                            col.GetComponent<Player>().TakeDamage(Damege, true, transform.position);
                            col.GetComponent<Player>().StartStun();
                        }
                    }
                    col.GetComponent<Player>().ImInGrenadeRadius = true;
                    AllPlayerGranadeRadius.Add(col.gameObject);
                }
                if (col.CompareTag("Enemy"))
                {
                    if (!col.GetComponent<Enemy>().ImInGrenadeRadius)
                    {
                        if (!col.GetComponent<Enemy>().isGround)
                        {
                            Explode(rb2, explosionForce / 2);
                            col.GetComponent<Enemy>().TakeDamage(Damege);
                        }
                        else
                        {
                            Explode(rb2, explosionForce);
                            col.GetComponent<Enemy>().TakeDamage(Damege);
                        }
                        col.GetComponent<Enemy>().ImInGrenadeRadius = true;
                        AllEnemyGranadeRadius.Add(col.gameObject);
                    }
                }
                else
                    Explode(rb2, 20);
            }
            if (col.CompareTag("GrenadeItem"))
                col.GetComponent<grenade>().enabled = true;
            if (col.CompareTag("Fireplugs"))
                col.GetComponent<Fireplugs>().ExplodeThisObject();
        }
    }

    private void Explode(Rigidbody2D Erb2, float EexplosionForce)
    {
        //Erb2.AddForce((Erb2.transform.position - transform.position).normalized * EexplosionForce, ForceMode2D.Impulse);
        Vector2 direction = Erb2.transform.position - transform.position;
        Erb2.AddForce(direction.normalized * EexplosionForce, ForceMode2D.Impulse);
    }
    public void DestroyEffect()
    {
        for (int i = 0; i < AllPlayerGranadeRadius.Count; i++)
        {
            AllPlayerGranadeRadius[i].GetComponent<Player>().ImInGrenadeRadius = false;
        }
        for (int i = 0; i < AllEnemyGranadeRadius.Count; i++)
        {
            if (AllEnemyGranadeRadius[i] != null)
                AllEnemyGranadeRadius[i].GetComponent<Enemy>().ImInGrenadeRadius = false;
        }
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
