using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplugs : MonoBehaviour
{
    [HideInInspector]
    public bool FirePlugsDestroy = false;
    public GameObject PrebafFireplugsDestroy;
    public Sprite FirePlugs1;
    public Transform PrefabSpawnPoint;

    public GameObject PrefabWater;
    public Transform PrefabWaterSpawn;

    private void Start()
    {
        if (FirePlugsDestroy)
            GetComponent<SpriteRenderer>().sprite = FirePlugs1;
    }
    public void ExplodeThisObject()
    {
        if (!FirePlugsDestroy)
            if (gameObject.GetComponent<SpriteRenderer>().sprite != FirePlugs1)
            {
                FirePlugsDestroy = true;
                GameObject obj = Instantiate(PrebafFireplugsDestroy, PrefabSpawnPoint);
                obj.transform.parent = PrefabSpawnPoint;
                gameObject.GetComponent<SpriteRenderer>().sprite = FirePlugs1;
                GameObject Water = Instantiate(PrefabWater, PrefabWaterSpawn);
                Water.transform.parent = PrefabWaterSpawn;
                Water.GetComponent<Water>().Invoke("PlayWater", .5f);
                Water.GetComponent<Water>().Invoke("StopWaterSTCoroutine", Random.Range(15, 30));
            }
    }
}
