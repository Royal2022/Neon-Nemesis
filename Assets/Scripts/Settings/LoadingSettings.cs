using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LoadingSettings : MonoBehaviour
{
    private void Start()
    {
        SetQuality(QualitySettings.GetQualityLevel());
    }

    public void SetQuality(int qualityindex)
    {
        UniversalRenderPipelineAsset URP = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline;

        if (qualityindex == 0)
        {
            URP.renderScale = 0.8f;
            URP.supportsHDR = false;
        }
        else if (qualityindex == 1 || qualityindex == 2)
        {
            URP.renderScale = 1f;
            URP.supportsHDR = true;
        }
        else if (qualityindex == 3)
        {
            URP.renderScale = 1.5f;
            URP.supportsHDR = true;
        }
        else if (qualityindex >= 4)
        {
            URP.renderScale = 2f;
            URP.supportsHDR = true;
        }
    }
}
