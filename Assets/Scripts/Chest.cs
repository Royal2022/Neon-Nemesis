using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator anim;
    public GameObject ItemPrefab;
    public Transform ItemSpawnPosition;
    public float ThrowObject = 1;
    public AudioSource ChestSoundOpen;
    private bool OpenOrClosed;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.E) && !OpenOrClosed)
        {
            anim.SetTrigger("open");
            ChestSoundOpen.Play();
            OpenOrClosed = true;
        }
    }
    public void DropItem()
    {
        Instantiate(ItemPrefab, ItemSpawnPosition.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.y, 1) * ThrowObject;
    }

}
