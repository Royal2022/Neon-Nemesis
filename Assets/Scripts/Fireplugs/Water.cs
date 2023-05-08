using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Water : MonoBehaviour
{
    private ParticleSystem PSWater1;
    private ParticleSystem PSWater2;
    private PushingWithWater pushingWithWater;

    void Start()
    {
        PSWater1 = GetComponent<ParticleSystem>();
        PSWater2 = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        pushingWithWater = transform.GetChild(1).gameObject.GetComponent<PushingWithWater>();
    }

    public void StopWaterSTCoroutine()
    {
        StartCoroutine(StopWater());
    }

    public void PlayWater()
    {
        PSWater2.Play();
        transform.GetChild(1).gameObject.SetActive(true);
        Invoke("PlayerWater2", 1);
    }
    private void PlayerWater2()
    {
        PSWater1.Play();
    }

    public IEnumerator StopWater()
    {
        //PSWater1.Stop();
        for (float alpha = 1f; alpha >= 0f; alpha -= .05f)
        {
            var main = PSWater1.main;

            main.startLifetime = alpha;
            if (alpha < 0.1f)
                main.startLifetime = 0;

            yield return new WaitForSeconds(.1f);
        }
        StartCoroutine(StopWater2());
    }
    private IEnumerator StopWater2()
    {
        //PSWater2.Stop();
        StartCoroutine(StopWaterForce());
        for (float alpha = 0.7f; alpha >= 0f; alpha -= .05f)
        {
            var main = PSWater2.main;

            main.startLifetime = alpha;

            if (alpha < 0.1f)
                main.startLifetime = 0;

            yield return new WaitForSeconds(.1f);
        }
    }

    public IEnumerator StopWaterForce()
    {
        for (float alpha = 3f; alpha >= 0f; alpha -= .5f)
        {
            pushingWithWater.Force = alpha;

            yield return new WaitForSeconds(.1f);
        }
        pushingWithWater.gameObject.SetActive(false);
        Invoke("DestroyThis", 2);
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
