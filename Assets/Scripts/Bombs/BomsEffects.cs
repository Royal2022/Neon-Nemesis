using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomsEffects : MonoBehaviour
{
    public void DestroyEffect()
    {
        Destroy(gameObject);
    }

    
    //public float radius = 5f;
    public float force = 2000f;

    public float radiusOfImpact = 5f;
    public LayerMask LayerToHit;

    private float saveForce;

    private void Start()
    {
        saveForce= force;
        Explode();
    }

    //void Explode()
    //{
    //    Collider2D[] overlappedColliders = Physics2D.OverlapCircleAll(transform.position, radius);

    //    for (int j = 0; j < overlappedColliders.Length; j++)
    //    {
    //        Rigidbody2D rigidbody = overlappedColliders[j].attachedRigidbody;
    //        if (rigidbody)
    //        {
    //            rigidbody.AddExplosionForce2D(force, this.transform.position, radius);
    //        }
    //    }
    //}

    private void Explode()
    {
        
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radiusOfImpact, LayerToHit);

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].tag == "Player")
            {
                if (!objects[i].GetComponent<Player>().isGround)
                {
                    force = 200;
                }
                else if (objects[i].GetComponent<Player>().isGround)
                    force = saveForce;
            }
            if (objects[i].tag == "Enemy")
            {
                if (!objects[i].GetComponent<Enemy>().isGround)
                {
                    force = 200;
                }
                else if (objects[i].GetComponent<Enemy>().isGround)
                    force = saveForce;
            }

            if (objects[i].GetComponent<Rigidbody2D>())
            {
                Vector2 direction = objects[i].transform.position - transform.position;
                objects[i].GetComponent<Rigidbody2D>().AddForce(direction * (force * 15));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusOfImpact);
    }
}
