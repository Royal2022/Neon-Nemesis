using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEndTeaching : MonoBehaviour
{
    public LoadingScreen loadingScreen;
    public GameObject MainCamera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().simulated = false;
            MainCamera.GetComponent<AudioSource>().enabled = false;
            loadingScreen.Loading();
        }
    }
}
