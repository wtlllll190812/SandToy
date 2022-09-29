using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EvoFire : EvoluteLayer
{
    public float burn;
    public float grow;
    public float thunder;

    public override void Init()
    {
        kernel = computeShader.FindKernel("CSMain");
    }

    public override void Excute(RenderTexture renderTexture, MainMap map)
    {
        computeShader.SetTexture(kernel, "Result", renderTexture);
        computeShader.SetFloat("burn", burn);
        computeShader.SetFloat("grow", grow);
        computeShader.SetFloat("thunder", thunder);
        computeShader.SetInt("seed", Random.Range(0,1000));
        computeShader.Dispatch(kernel, (int)64 / 8, (int)64 / 8, 1);
    }
}
