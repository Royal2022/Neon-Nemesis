using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessing : MonoBehaviour
{
    private Volume volume;

    private Vignette vignette;


    private ChromaticAberration chromaticAberration;
    private DepthOfField depthOfField;


    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out depthOfField);
    }

    public IEnumerator Dawn()
    {
        for (float alpha = vignette.intensity.value; alpha <= 0.5f; alpha += 0.1f)
        {
            vignette.intensity.value = alpha;
            yield return new WaitForSeconds(.05f);
        }
        StartCoroutine(Fade());
    }
    public IEnumerator Fade()
    {
        for (float alpha = 0.5f; alpha >= 0; alpha -= 0.1f)
        {
            vignette.intensity.value = alpha;
            yield return new WaitForSeconds(.05f);
        }
    }



    public IEnumerator StunDawn()
    {
        for (float alpha = 0; alpha <= 300f; alpha += 100f)
        {
            depthOfField.focalLength.value = alpha;
            yield return new WaitForSeconds(.05f);
        }
        StartCoroutine(StunFade());
    }    
    public IEnumerator StunFade()
    {
        for (float alpha = 300; alpha >= 0; alpha -= 10f)
        {
            depthOfField.focalLength.value = alpha;
            yield return new WaitForSeconds(.1f);
        }
    }



    public IEnumerator AberrationDawn()
    {
        for (float alpha = 0; alpha <= 1f; alpha += .1f)
        {
            chromaticAberration.intensity.value = alpha;
            yield return new WaitForSeconds(.05f);
        }
        StartCoroutine(AberrationFade());
    }
    public IEnumerator AberrationFade()
    {
        for (float alpha = 1f; alpha >= .01f; alpha -= .05f)
        {
            chromaticAberration.intensity.value = alpha;

            yield return new WaitForSeconds(.1f);
        }
    }
}
