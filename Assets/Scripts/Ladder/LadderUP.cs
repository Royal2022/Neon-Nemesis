using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderUP : MonoBehaviour
{
    public static bool upTriger;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerLegs"))
            upTriger = true;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerLegs"))
            upTriger = false;
    }
}
