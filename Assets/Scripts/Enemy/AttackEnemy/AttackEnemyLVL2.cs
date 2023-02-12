using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackEnemyLVL2 : MonoBehaviour
{

    public GameObject bullet;
    public Transform shotPoint1;
    public Transform shotPoint2;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private Enemy enemyGetComp;

    void Start()
    {
        enemyGetComp = gameObject.GetComponent<Enemy>();
    }
    void Update()
    {
        if (timeBtwShots <= 0)
        {
            enemyGetComp.anim.SetBool("shot", true);
            timeBtwShots = startTimeBtwShots;  
        }
        else
        {
            enemyGetComp.anim.SetBool("shot", false);
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void Shot()
    {
        bullet.gameObject.GetComponent<bullet_Enemy>().Direction = (int)gameObject.transform.localScale.x;

        Instantiate(bullet, shotPoint1.position, transform.rotation);           
        Instantiate(bullet, shotPoint2.position, transform.rotation);
    }



}
