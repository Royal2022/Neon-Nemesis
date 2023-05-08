using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PartsFireplugs : MonoBehaviour
{
    public Vector2 XY;
    public int Twist;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(XY);
        rb.AddTorque(Twist);

        Invoke("DestroyObj", Random.Range(2, 5));
    }

    private void DestroyObj()
    {
        Destroy(gameObject);
    }
}
