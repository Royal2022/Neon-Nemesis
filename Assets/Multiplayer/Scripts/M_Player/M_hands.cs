using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class M_hands : MonoBehaviourPunCallbacks
{
    [SerializeField] private M_Player player;
    public float offset;

    public int sensitivity = 20;




    void Start()
    {
        player = FindObjectOfType<M_Player>();
    }

    void Update()
    {
        if (!photonView.IsMine) return;
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float r = difference.y * sensitivity;


        if (!photonView.IsMine) return;
        if (difference.y < 1 && difference.y > -1.5f)
        {
            if (M_Player.facingRight)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, r + offset);
            }
            else if (!M_Player.facingRight)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -r + offset);
            }
        }



    }
}
