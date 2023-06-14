using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[System.Serializable]
public abstract class EvoluteLayer
{
    public bool isActive;
    protected int kernel;
    public ComputeShader computeShader;

    public abstract void Init();

    public abstract void Execute(RenderTexture texture, MainMap map);
}
