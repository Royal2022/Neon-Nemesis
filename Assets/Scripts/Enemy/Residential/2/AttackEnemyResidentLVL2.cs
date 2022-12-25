using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackEnemyResidentLVL2 : MonoBehaviour
{

    public GameObject bullet;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject enemy;


    void Start()
    {
    }
    void Update()
    {
        if (timeBtwShots <= 0)
        {
            enemy.gameObject.GetComponent<Enemy>().anim.SetBool("shot", true);
            timeBtwShots = startTimeBtwShots;  
        }
        else
        {
            enemy.gameObject.GetComponent<Enemy>().anim.SetBool("shot", false);
            timeBtwShots -= Time.deltaTime;
        }


    }

    public void Shot()
    {
        bullet.gameObject.GetComponent<bulletEnemyLVL2>().Direction = (int)enemy.gameObject.transform.localScale.x;

        Instantiate(bullet, shotPoint.position, transform.rotation);           
    }



}
