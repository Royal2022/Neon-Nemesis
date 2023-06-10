using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject PlayerEnterPortal;
    public Animator anim;
    public LoadingScreen loadingScreen;
    public GameObject Frame;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("Activated", true);
            collision.gameObject.SetActive(false);
            GameObject player =  Instantiate(PlayerEnterPortal);
            player.transform.position = new Vector3(transform.position.x, collision.transform.position.y, collision.transform.position.z);
        }
    }

    public void TheEndGame()
    {
        loadingScreen.Loading();
        Frame.SetActive(false);
    }
}
