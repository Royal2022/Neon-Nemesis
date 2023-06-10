using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCatScene : MonoBehaviour
{
    public LoadingScreen loadingScreen;
    public GameObject MainCamera;

    private bool playerNotEntry = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !playerNotEntry)
        {
            playerNotEntry = true;
            MainCamera.GetComponent<AudioSource>().enabled = false;
            loadingScreen.Loading();
        }
    }
}
