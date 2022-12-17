using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliding : MonoBehaviour
{
    private bool isGround;
    //public Transform feetPos;
    public float checkRaduis;
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
