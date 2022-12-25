using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_sliding : MonoBehaviour
{
    private bool isGround;
    //public Transform feetPos;
    public float checkRaduis = 0.3f;
    public LayerMask whatIsGround;

    void Update()
    {
        isGround = Physics2D.OverlapCircle(gameObject.transform.position, checkRaduis, whatIsGround);


        if (isGround)
        {
            gameObject.GetComponent<Rigidbody2D>().drag = 10f;
        }
        else if (!isGround)
        {
            gameObject.GetComponent<Rigidbody2D>().drag = 0;
        }
    }
}
