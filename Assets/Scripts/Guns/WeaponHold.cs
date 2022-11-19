using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class WeaponHold : MonoBehaviour
{

    //[SerializeField] private hands Handss;



    public bool hold;
    public float distance = 1f;
    RaycastHit2D hit;
    public Transform holdPoint;
    public float throwobject = 1;


    

    void Start()
    {
        //Handss = FindObjectOfType<hands>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            if (!hold)
            {
                Physics2D.queriesStartInColliders = false;

                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);


                if (hit.collider != null && hit.collider.tag == "Weapon" && holdPoint.GetChild(0).gameObject)
                {
                    hold = true;
                    //Destroy(holdPoint.GetChild(0).gameObject);

                    //holdPoint.GetChild(0).GetComponent<Pistol>().enabled = false;
                    holdPoint.GetChild(0).transform.rotation = Quaternion.Euler(0f, 0f, 0.64f);
                    holdPoint.GetChild(0).transform.position = hit.collider.gameObject.transform.position;
                    holdPoint.GetChild(0).parent = null;


                    //hit.collider.gameObject.GetComponent<AutomaticGun>().enabled = true;
                    FindObjectOfType<hands>().res();
                    hit.collider.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0.64f);
                    hit.collider.gameObject.transform.position = holdPoint.gameObject.transform.position;
                    hit.transform.parent = holdPoint;

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
            if (holdPoint.position.x > transform.position.x && hold == true)
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
            else if (holdPoint.position.x < transform.position.x && hold == true)
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
