using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADS : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();

        anim.SetInteger("ADSNumber", Random.Range(1, 13));
    }
}
