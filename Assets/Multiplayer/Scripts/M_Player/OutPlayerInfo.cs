using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class OutPlayerInfo : MonoBehaviour
{

    public Text healthDisplay;
    public Text AmmoDisplay;

    void Start()
    {

    }


    public void HealthInfo(int info)
    {
        healthDisplay.text = "" + info;
    }
    public void AmmoInfo(float info1, float info2)
    {
        AmmoDisplay.text = info1 + "/" + info2;
    }
}
