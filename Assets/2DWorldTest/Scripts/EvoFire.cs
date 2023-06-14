using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EvoFire : EvoluteLayer
{
    [SerializeField] private readonly float burn = 0.7f;
    [SerializeField] private readonly float grow = 0.01f;
    [SerializeField] private readonly float thunder = 1e-6f;

    public override void Init()
    {
        kernel = computeShader.FindKernel("CSMain");
    }

    public override void Execute(RenderTexture renderTexture, MainMap map)
    {
        computeShader.SetTexture(kernel, "Result", renderTexture);
        computeShader.SetFloat("burn", burn);
        computeShader.SetFloat("grow", grow);
        computeShader.SetFloat("thunder", thunder);
        computeShader.SetInt("seed", Random.Range(0, 1000));
        computeShader.Dispatch(kernel, renderTexture.width / 8, renderTexture.height / 8, 1);
    }
}