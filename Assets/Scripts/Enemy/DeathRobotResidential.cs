using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRobotDeath : MonoBehaviour
{

    public GameObject BoomEffect;

    public void DeathRobotResidential()
    {
        Destroy(gameObject);
        Instantiate(BoomEffect, gameObject.transform.position, transform.rotation);
    }
}
