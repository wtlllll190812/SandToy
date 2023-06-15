using UnityEngine;

[System.Serializable]
public abstract class EvoluteLayer : MonoBehaviour
{
    [SerializeField] protected ComputeShader computeShader;
    [SerializeField] protected int fps = 1;
    protected int kernel;
    private float currentTime = 0;

    public bool ready
    {
        get
        {
            currentTime += Time.deltaTime;
            if (!(currentTime >= 1f / fps)) return false;
            currentTime = 0;
            return true;
        }
    }

    public virtual void Init(RenderTexture texture)
    {
        kernel = computeShader.FindKernel("CSMain");
        computeShader.SetTexture(kernel, "Result", texture);
    }

    public virtual void Execute(RenderTexture texture, MainMap map, int seed)
    {
        computeShader.SetInt("seed", 1);
        computeShader.Dispatch(kernel, texture.width / 8, texture.height / 8, 1);
    }
}