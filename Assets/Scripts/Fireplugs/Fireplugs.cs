using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplugs : MonoBehaviour
{
    public GameObject PrebafFireplugsDestroy;
    public Sprite FirePlugs1;
    public Transform PrefabSpawnPoint;

    public GameObject PrefabWater;
    public Transform PrefabWaterSpawn;

    public void ExplodeThisObject()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sprite != FirePlugs1)
        {
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
