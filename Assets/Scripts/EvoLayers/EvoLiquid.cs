using Sirenix.OdinInspector;
using UnityEngine;

public class EvoLiquid: EvoBasicElements
{
    [SerializeField] private Species liquidKind;

    public override void Execute(int seed)
    {
        computeShader.SetInt("liquidKind", (int) liquidKind);
        base.Execute(seed);
    }
}
