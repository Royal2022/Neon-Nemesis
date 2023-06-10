using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class NoWayBack : MonoBehaviour
{
    public GameObject LeftBorder;
    public GameObject RightBorder;

    private GameObject boss;

    private void Start()
    {
        boss = transform.GetChild(0).gameObject;
        boss.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            boss.SetActive(true);
            LeftBorder.SetActive(true);
            RightBorder.SetActive(true);
        }
    }
    private void Update()
    {
        if (boss == null)
        {
            LeftBorder.SetActive(false);
            RightBorder.SetActive(false);
        }
    }
}
