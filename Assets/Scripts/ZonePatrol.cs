using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonePatrol : MonoBehaviour
{
    public bool PlayerInZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            PlayerInZone = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            PlayerInZone = false;
    }
}
