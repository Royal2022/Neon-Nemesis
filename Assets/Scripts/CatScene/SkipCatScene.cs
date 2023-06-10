using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipCatScene : MonoBehaviour
{
    private PlayableDirector playableDirector;
    private bool playSkip;
    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !playSkip)
        {
            playSkip = true;
            StartCoroutine(skip());
        }
    }

    private IEnumerator skip()
    {
        float currentSkipTime = 0;

        while (currentSkipTime <= (float)playableDirector.duration)
        {
            playableDirector.time += .05f;
            currentSkipTime += .05f;
            yield return null;
        }
        yield return new WaitForSeconds(1);
    }
}
