using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : MonoBehaviour
{

    public GameObject EffectFirstAid;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Instantiate(EffectFirstAid, collision.gameObject.transform.position, Quaternion.identity);
            Instantiate(EffectFirstAid, collision.gameObject.transform.position, Quaternion.identity).gameObject.transform.SetParent(collision.gameObject.transform);
            collision.gameObject.GetComponent<Player>().health += 5;
            if (collision.gameObject.GetComponent<Player>().health > collision.gameObject.GetComponent<Player>().healthSlider.maxValue)
            {
                collision.gameObject.GetComponent<Player>().health = (int)collision.gameObject.GetComponent<Player>().healthSlider.maxValue; 
            }
            Destroy(gameObject);
        }
    }
}
