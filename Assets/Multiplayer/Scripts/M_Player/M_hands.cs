using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_hands : MonoBehaviour
{
    public float offset;

    public int sensitivity = 20;

    public Animator anim;

    public GameObject player;


    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player.GetComponent<M_Player>().isLocalPlayer)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            float r = difference.y * sensitivity;

            if (difference.y < 1 && difference.y > -1.5f)
            {
                if (player.GetComponent<M_Player>().facingRight)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, r + offset);
                }
                else if (!player.GetComponent<M_Player>().facingRight)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, -r + offset);
                }
            }
        }
    }

    public void AnimOnOff(string name, bool OnOff)
    {
        anim.SetBool(name, OnOff);
    }
}
