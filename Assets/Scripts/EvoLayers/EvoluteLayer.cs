using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public abstract class EvoluteLayer : MonoBehaviour
{
    [SerializeField] protected ComputeShader computeShader;
    protected int kernel;

    public virtual void Init()
    {
        kernel = computeShader.FindKernel("CSMain");
    }

    public virtual void Execute(Texture texture, MainMap map, int seed)
    {
        computeShader.SetInt("seed", seed);
        computeShader.SetTexture(kernel, "Result", texture);
        computeShader.Dispatch(kernel, texture.width / 8, texture.height / 8, 1);
    }
}