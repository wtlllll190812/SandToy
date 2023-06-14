using System.IO;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

public enum NoiseType { Perlin, Value, Simplex, Worley };
public enum GenerationMode { None, Abs, Sin };
public enum TextureSize { x64 = 64, x128 = 128, x256 = 256, x512 = 512, x1024 = 1024, x2048 = 2048 };

public class GenNoise : MonoBehaviour
{
    public ComputeShader computeShader;
    public NoiseType noiseType = NoiseType.Perlin;
    public GenerationMode generation = GenerationMode.None;
    public RenderTextureFormat format = RenderTextureFormat.ARGB32;
    public TextureSize size = TextureSize.x512;
    public float scale = 10f;
    public int seed;
    public string path ;

    [HideInInspector]public RenderTexture renderTexture;

    public void Awake()
    {
        GenPerlinNoise();
    }

    void Init()
    {
        if(renderTexture!=null)
            renderTexture.Release();
        renderTexture = CreateRT((int)size);
        int kernel = computeShader.FindKernel("GenNoise");
        computeShader.SetTexture(kernel, "Texture", renderTexture);
        computeShader.SetInt("size", (int)size);
        computeShader.SetFloat("scale", scale * 10f);
        computeShader.SetFloat("seed", (float)seed/10000);
        computeShader.SetInt("Type", (int)noiseType);
        computeShader.SetInt("State", (int)generation);
        computeShader.Dispatch(kernel, (int)size / 8, (int)size / 8, 1);
    }

    private void OnDisable()
    {
        if (renderTexture == null) return;
        renderTexture.Release();
    }

    [Button("GenPerlinNoise")]
    public void GenPerlinNoise()
    {
        Init();
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTexture;
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        RenderTexture.active = previous;

        byte[] bytes = texture.EncodeToTGA();
        File.WriteAllBytes(path, bytes);
    }

    private RenderTexture CreateRT(int size)
    {
        RenderTexture renderTexture = new RenderTexture(size, size, 0, format);
        renderTexture.enableRandomWrite = true;
        renderTexture.filterMode = FilterMode.Point;
        renderTexture.wrapMode = TextureWrapMode.Repeat;
        renderTexture.Create();
        return renderTexture;
    }
}