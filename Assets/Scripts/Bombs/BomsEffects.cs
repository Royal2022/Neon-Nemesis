using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BomsEffects : MonoBehaviour
{
    public float explosionForce = 150;
    public float explosionRadius = 3;
    public float upwardsModifier = 100;
    public float Damege = 3;

    private List<GameObject> AllPlayerGranadeRadius = new List<GameObject>();
    private List<GameObject> AllEnemyGranadeRadius = new List<GameObject>();
    private List<GameObject> AllBossesGranadeRadius = new List<GameObject>();


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
                            col.GetComponent<Player>().StartStun();
                        }
                        else
                        {
                            Explode(rb2, explosionForce);
                            col.GetComponent<Player>().StartStun();
                        }
                    }
                    col.GetComponent<Player>().TakeDamage(Damege, transform.position);
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
                        }
                        else
                        {
                            Explode(rb2, explosionForce);
                        }
                        col.GetComponent<Enemy>().TakeDamage(Damege);
                        col.GetComponent<Enemy>().ImInGrenadeRadius = true;
                        AllEnemyGranadeRadius.Add(col.gameObject);
                    }
                }
                if (col.CompareTag("Bosses"))
                {
                    if (!col.GetComponent<Bosses>().ImInGrenadeRadius)
                    {
                        if (!col.GetComponent<Bosses>().isGround)
                        {
                            Explode(rb2, explosionForce / 2);
                        }
                        else
                        {
                            Explode(rb2, explosionForce);
                        }
                        col.GetComponent<Bosses>().TakeDamage(Damege);
                        col.GetComponent<Bosses>().ImInGrenadeRadius = true;
                        AllBossesGranadeRadius.Add(col.gameObject);
                    }
                }
                else
                    Explode(rb2, 20);
            }
            if (col.CompareTag("GrenadeItem"))
                col.GetComponent<grenade>().enabled = true;
            if (col.CompareTag("Fireplugs"))
                col.GetComponent<Fireplugs>().ExplodeThisObject();
            if (col.CompareTag("Grenade"))
            {
                if (col.GetComponent<TNT>())
                    col.GetComponent<TNT>().Explosion();
                if (col.GetComponent<grenade>())
                    col.GetComponent<TNT>().Explosion();
            }
                
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
        if (AllPlayerGranadeRadius != null)
            for (int i = 0; i < AllPlayerGranadeRadius.Count; i++)
            {
                AllPlayerGranadeRadius[i].GetComponent<Player>().ImInGrenadeRadius = false;
            }
        if (AllEnemyGranadeRadius != null)
            for (int i = 0; i < AllEnemyGranadeRadius.Count; i++)
            {
                if (AllEnemyGranadeRadius[i] != null)
                    AllEnemyGranadeRadius[i].GetComponent<Enemy>().ImInGrenadeRadius = false;
            }
        if (AllBossesGranadeRadius != null)
            for (int i = 0; i < AllBossesGranadeRadius.Count; i++)
            {
                if (AllBossesGranadeRadius[i] != null)
                    AllBossesGranadeRadius[i].GetComponent<Bosses>().ImInGrenadeRadius = false;
            }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
