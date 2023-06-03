using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class M_PostProcessing : MonoBehaviour
{
    private Volume volume;

    private Vignette vignette;
    private ChromaticAberration chromaticAberration;
    private DepthOfField depthOfField;

    public GameObject DeathCanvas;

    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out depthOfField);
    }

    private void FixedUpdate()
    {
        if (DeathCanvas.activeSelf)
        {
            StopAllCoroutines();
            vignette.intensity.value = 0;
            depthOfField.focalLength.value = 0;
            chromaticAberration.intensity.value = 0;
        }
    }

    private Coroutine dawnCoroutine;

    public IEnumerator Dawn()
    {
        if (dawnCoroutine != null)
        {
            StopCoroutine(dawnCoroutine);
        }
        for (float alpha = vignette.intensity.value; alpha < 0.5f; alpha += 0.1f)
        {
            vignette.intensity.value = alpha;
            yield return new WaitForSeconds(.05f);
        }
        dawnCoroutine = StartCoroutine(Fade());
    }
    public IEnumerator Fade()
    {
        for (float alpha = 0.5f; alpha > 0; alpha -= 0.1f)
        {
            vignette.intensity.value = alpha;
            yield return new WaitForSeconds(.1f);
        }
    }


    private Coroutine stunFadeCoroutine;

    public IEnumerator StunDawn()
    {
        if (stunFadeCoroutine != null)
        {
            StopCoroutine(stunFadeCoroutine);
        }

        for (float alpha = depthOfField.focalLength.value; alpha < 400f; alpha += 100f)
        {
            depthOfField.focalLength.value = alpha;

            if (Mathf.Approximately(depthOfField.focalLength.value, 300f))
            {
                stunFadeCoroutine = StartCoroutine(StunFade());
                yield break;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
    public IEnumerator StunFade()
    {
        for (float alpha = 300; alpha > 10; alpha -= 10f)
        {
            depthOfField.focalLength.value = alpha;
            yield return new WaitForSeconds(0.1f);
        }
    }


    private Coroutine aberrationDawnCoroutine;

    public IEnumerator AberrationDawn()
    {
        if (aberrationDawnCoroutine != null)
        {
            StopCoroutine(aberrationDawnCoroutine);
        }

        for (float alpha = 0; alpha < 1f; alpha += .1f)
        {
            chromaticAberration.intensity.value = alpha;
            yield return new WaitForSeconds(.05f);
        }
        aberrationDawnCoroutine = StartCoroutine(AberrationFade());
    }
    public IEnumerator AberrationFade()
    {
        for (float alpha = 1f; alpha > .01f; alpha -= .05f)
        {
            chromaticAberration.intensity.value = alpha;

            yield return new WaitForSeconds(.1f);
        }
    }
}
