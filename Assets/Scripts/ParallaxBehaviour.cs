using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    /*
    [SerializeField] Transform followingTarget;
    [SerializeField, Range(0f, 1f)] float parallaxStrength = 0.1f;
    [SerializeField] bool disableVerticalParallax;
    Vector3 targetPreviousPosition;

    void Start()
    {
        if (!followingTarget)
        {
            followingTarget = Camera.main.transform;
        }
        targetPreviousPosition = followingTarget.position;
    }

    void Update()
    {
        var delta = followingTarget.position - targetPreviousPosition;

        if (disableVerticalParallax)
        {
            delta.y = 0;
        }
        targetPreviousPosition = followingTarget.position;

        transform.position += delta * parallaxStrength;
    }
    */

    public GameObject cam;

    public float Parallax;
    float startPosX;

    private void Start()
    {
        startPosX = transform.position.x;
    }
    private void Update()
    {
        float distX = (cam.transform.position.x * (1 - Parallax));
        transform.position = new Vector3(startPosX + distX, transform.position.y, transform.position.z);
    }
}
