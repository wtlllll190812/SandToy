using UnityEngine;

public class EvoTempSim: EvoluteLayer
{
    [SerializeField] private int maxQ = 1;

    public override void Execute(int seed)
    {
        computeShader.SetInt("maxQ", maxQ);
        base.Execute(seed);
    }
}
