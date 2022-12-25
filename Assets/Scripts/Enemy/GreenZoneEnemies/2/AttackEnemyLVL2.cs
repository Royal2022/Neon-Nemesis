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

    //[SerializeField] private GameObject _bullet;
    public GameObject enemy;

    //public GameObject Pref;

    //float timer = 0;
    //bool timerReached = false;


    void Start()
    {
        //Pref = Resources.Load<GameObject>("bullet(EnemyLVL2)");
    }
    void Update()
    {/*
        if (enemy.gameObject.GetComponent<Enemy>().playerNoticed)
        {
            if (!timerReached)
                timer += Time.deltaTime;

            if (!timerReached && timer > 1.5f)
            {
                enemy.gameObject.GetComponent<Enemy>().anim.SetBool("shot", true);
                timerReached = true;
            }
            else if (timerReached)
            {
                timer = 0;
                timerReached = false;
                enemy.gameObject.GetComponent<Enemy>().anim.SetBool("shot", false);
            }
        }*/


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

        Instantiate(bullet, shotPoint1.position, transform.rotation);           
        Instantiate(bullet, shotPoint2.position, transform.rotation);
    }



}
