using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyLVL2 : MonoBehaviour
{

    public GameObject bullet;
    public Transform shotPoint1;
    public Transform shotPoint2;

    private float timeBtwShots;
    public float startTimeBtwShots;

    [SerializeField] private GameObject _bullet;
    public GameObject enemy;


    void Start()
    {
        
    }

    void Update()
    {
        _bullet.GetComponent<bulletEnemyLVL2>().Direction = enemy.gameObject.transform.localScale;
    }

    public void Shot()
    {
        Instantiate(bullet, shotPoint1.position, transform.rotation);           
        Instantiate(bullet, shotPoint2.position, transform.rotation);            
    }


}
