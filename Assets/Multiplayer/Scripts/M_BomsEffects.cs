using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class M_BomsEffects : MonoBehaviour
{
    public float explosionForce = 150;
    public float explosionRadius = 3;
    public float upwardsModifier = 100;
    public int Damege = 3;

    private List<GameObject> AllPlayerGranadeRadius = new List<GameObject>();


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
                    if (!col.GetComponent<M_Player>().ImInGrenadeRadius)
                    {
                        if (!col.GetComponent<M_Player>().isGround)
                        {
                            Explode(rb2, explosionForce / 2);
                            col.GetComponent<M_Player>().TakeDamage(Damege/*, true, transform.position*/);
                            col.GetComponent<M_Player>().StartStun();
                        }
                        else
                        {
                            Explode(rb2, explosionForce);
                            col.GetComponent<M_Player>().TakeDamage(Damege/*, true, transform.position*/);
                            col.GetComponent<M_Player>().StartStun();
                        }
                    }
                    col.GetComponent<M_Player>().ImInGrenadeRadius = true;
                    AllPlayerGranadeRadius.Add(col.gameObject);
                }               
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
            AllPlayerGranadeRadius[i].GetComponent<M_Player>().ImInGrenadeRadius = false;
        }
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
