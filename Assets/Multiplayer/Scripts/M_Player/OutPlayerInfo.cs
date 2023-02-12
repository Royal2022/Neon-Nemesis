using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class OutPlayerInfo : MonoBehaviour
{

    public Text AmmoDisplay;

    public Slider stamine;
    public Slider healthSlider;
    public Slider armorSlider;

    public void HealthInfo(int info)
    {
        healthSlider.value = info;
    }
    public void AmmoInfo(float info1, float info2)
    {
        AmmoDisplay.text = info1 + "/" + info2;
    }
    public void ArmorInfo(int info)
    {
        armorSlider.value = info;
    }
}
