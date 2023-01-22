using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armor : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().armor += 20;
            if (collision.gameObject.GetComponent<Player>().armor > collision.gameObject.GetComponent<Player>().armorSlider.maxValue)
            {
                collision.gameObject.GetComponent<Player>().armor = (int)collision.gameObject.GetComponent<Player>().armorSlider.maxValue;
            }
            Destroy(gameObject);
        }
    }
}
