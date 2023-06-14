using UnityEngine;

public class EvoFire : EvoluteLayer
{
    [SerializeField] private readonly float burn = 0.7f;
    [SerializeField] private readonly float grow = 0.01f;
    [SerializeField] private readonly float thunder = 1e-6f;

    public override void Execute(RenderTexture texture, MainMap map, int seed)
    {
        computeShader.SetFloat("burn", burn);
        computeShader.SetFloat("grow", grow);
        computeShader.SetFloat("thunder", thunder);
        base.Execute(texture, map, seed);
    }
}