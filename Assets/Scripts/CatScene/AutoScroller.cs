using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroller : MonoBehaviour
{
    private Scrollbar scrollbar;

    private void Start()
    {
        scrollbar= GetComponent<Scrollbar>();
    }
    private void Update()
    {
        if (scrollbar.value > 0)
            scrollbar.value = 0;
    }
}
