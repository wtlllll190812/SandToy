using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EvoDesert : EvoluteLayer
{
    public override void Init()
    {
        kernel  = computeShader.FindKernel("CSMain");
    }

    public override void Execute(RenderTexture renderTexture, MainMap map)
    {
        computeShader.SetTexture(kernel, "Result", renderTexture);
        computeShader.Dispatch(kernel, (int)256 / 8, (int)256 / 8, 1);
    }
}
