using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EvoAir : EvoluteLayer
{
    public Texture2D flowMap;
    public override void Init()
    {
        kernel = computeShader.FindKernel("CSMain");
    }

    public override void Excute(RenderTexture renderTexture, MainMap map)
    {
        computeShader.SetTexture(kernel, "Result", renderTexture);
        computeShader.SetTexture(kernel, "FlowMap", flowMap);
        computeShader.SetInt("seed", Random.Range(0, 10000));
        computeShader.Dispatch(kernel, (int)64 / 8, (int)64 / 8, 1);
    }
}
