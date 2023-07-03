using UnityEngine;

namespace EvoLayers
{
    public class EvoFluidSim: MonoEvoLayer
    {
        public override void Init(MainMap map)
        {
            base.Init(map);
            computeShader.SetInt("texSize", mainMap.BasicTexture.width);
        }

        public override void OnDebug()
        {
            base.OnDebug();
            computeShader.SetInt("texSize", mainMap.BasicTexture.width);
        }
    }
}