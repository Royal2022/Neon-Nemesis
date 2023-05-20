using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    private Animator anim;
    public Transform LeftBloodPos;
    public Transform RightBloodPos;
    public GameObject BloodPrefab;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void RandomBloodAnim(bool rightOrLeft)
    {
        if (rightOrLeft)
        {
            GameObject obj = Instantiate(BloodPrefab, RightBloodPos);
            obj.transform.parent = null;
            //obj.transform.parent = RightBloodPos;
            obj.GetComponent<Animator>().SetInteger("bloodNumber", Random.Range(1, 4));
        }
        else if (!rightOrLeft)
        {
            GameObject obj = Instantiate(BloodPrefab, LeftBloodPos);
            obj.transform.parent = null;
            //obj.transform.parent = LeftBloodPos;
            obj.GetComponent<Animator>().SetInteger("bloodNumber", Random.Range(1, 4));

        }
    }

    public void BloodAnimEnd()
    {
        //anim.SetInteger("bloodNumber", 0);
        Destroy(gameObject);
    }
}
