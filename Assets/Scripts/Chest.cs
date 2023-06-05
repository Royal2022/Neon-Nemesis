using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator anim;
    public GameObject ItemPrefab;
    public Transform ItemSpawnPosition;
    public float ThrowObject = 1;
    public AudioSource ChestSoundOpen;
    public bool OpenOrClosed = false;
    private LayerMask WhatLayerPlayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        WhatLayerPlayer = LayerMask.GetMask("Player");
    }

    private void FixedUpdate()
    {
        if (!OpenOrClosed)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, .5f, WhatLayerPlayer);
            foreach (Collider2D col in colliders)
            {
                if (col.CompareTag("Player"))
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        anim.SetTrigger("open");
                        ChestSoundOpen.Play();
                        OpenOrClosed = true;
                    }
                }
            }
        }
    }

    public void DropItem()
    {
        Instantiate(ItemPrefab, ItemSpawnPosition.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.y, 1) * ThrowObject;
    }

}
