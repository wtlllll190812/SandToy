using UnityEngine;

public class MonoEvoLayer : MonoBehaviour, IEvoluteLayer
{
    [SerializeField] protected ComputeShader computeShader;
    [SerializeField] protected int fps = 1;
    [SerializeField] private bool setOn = true;
    [SerializeField] private bool onDebug;
    protected int kernel;
    protected MainMap mainMap;
    private float currentTime = 0;

    public virtual bool IsReady()
    {
        currentTime += Time.deltaTime;
        if (!(currentTime >= 1f / fps))
            return false;
        currentTime = 0;
        return setOn && enabled;
    }

    public virtual void Init(MainMap map)
    {
        mainMap = map;
        kernel = computeShader.FindKernel("CSMain");
        computeShader.SetTexture(kernel, "Result", map.BasicTexture);
        computeShader.SetTexture(kernel, "Environment", map.EnvironmentTexture);
    }

    public virtual void Execute(int seed)
    {
        if (onDebug)
        {
            computeShader.SetTexture(kernel, "Result", mainMap.BasicTexture);
            computeShader.SetTexture(kernel, "Environment", mainMap.EnvironmentTexture);
        }

        computeShader.SetInt("seed", seed);
        computeShader.Dispatch(kernel, mainMap.BasicTexture.width / 8, mainMap.BasicTexture.height / 8, 1);
    }
}