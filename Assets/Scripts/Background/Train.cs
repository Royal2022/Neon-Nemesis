using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public float SpeedTrain = 50f;


    void Update()
    {
        transform.position = new Vector3(transform.position.x + SpeedTrain * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
