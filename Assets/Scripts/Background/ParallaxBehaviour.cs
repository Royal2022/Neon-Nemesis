using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    public GameObject CineCamera;

    public float Parallax;
    private float startPosX;

    private void Start()
    {
        startPosX = transform.position.x;
    }
    private void Update()
    {
        float distX = (CineCamera.transform.position.x * (1 - Parallax));
        transform.position = new Vector3(startPosX + distX, transform.position.y, transform.position.z);
    }
}
