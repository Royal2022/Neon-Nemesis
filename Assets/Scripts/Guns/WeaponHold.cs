using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHold : MonoBehaviour
{
    public bool hold;
    public float distance = 5f;
    RaycastHit2D hit;
    public Transform holdPoint;
    public float throwobject = 1;

    public Transform pines;

    void Start()
    {
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {

            if (!hold)
            {

                Physics2D.queriesStartInColliders = false;
                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

                if (hit.collider != null && hit.collider.tag == "Weapon")
                {
                    hold = true;
                }
            }
            else
            {
                holdPoint.GetChild(0).gameObject.AddComponent<Rigidbody2D>();
                holdPoint.GetChild(0).gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

                holdPoint.GetChild(0).transform.parent = null;


                hold = false;
                if (hit.collider.gameObject.GetComponent<Rigidbody2D> () != null)
                {
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwobject;
                }
            }
            //holdPoint.GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        }
        if (hold)
        {
            if (hit.collider.gameObject.GetComponent<Rigidbody2D>())
            {
                Destroy(hit.collider.gameObject.GetComponent<Rigidbody2D>());
            }
            hit.transform.parent = holdPoint;
            hit.collider.gameObject.transform.position = holdPoint.position;
            //hit.collider.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            
            if (holdPoint.position.x > transform.position.x && hold == true)
            {
                hit.collider.gameObject.transform.localScale = new Vector2(transform.localScale.x * 0.408f, transform.localScale.y * 0.408f);
            }
            else if (holdPoint.position.x < transform.position.x && hold == true)
            {
                hit.collider.gameObject.transform.localScale = new Vector2(transform.localScale.x * -0.408f, transform.localScale.y * 0.408f);
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }

}
