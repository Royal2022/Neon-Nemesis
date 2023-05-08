using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPoint : MonoBehaviour
{
    public Animator Animator;

    public void NoRepaet()
    {
        Animator.enabled = false;
    }
}
