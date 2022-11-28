using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.tag == "pistol2")
        {
            Destroy(gameObject);
            AutomaticGun.allAmmo += 35;
        }
        else if (collision.CompareTag("Player") && gameObject.tag == "pistol1")
        {
            Destroy(gameObject);
            Pistol.allAmmo += 15;
            Debug.Log("C");
        }
    }*/
}
