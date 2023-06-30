using UnityEngine;

public class EvoTempSim: MonoEvoLayer
{
    public override void Init(MainMap map)
    {
        base.Init(map);
        computeShader.SetInt("texSize", mainMap.BasicTexture.width);
    }
}
