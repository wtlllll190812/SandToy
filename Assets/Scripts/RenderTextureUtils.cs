using UnityEngine;

public static class RenderTextureUtils
{
    public static RenderTexture CreateRT(int size, RenderTextureFormat format = RenderTextureFormat.ARGB32)
    {
        var renderTexture = new RenderTexture(size, size, 0, format)
        {
            enableRandomWrite = true,
            filterMode = FilterMode.Point,
            wrapMode = TextureWrapMode.Clamp,
        };
        renderTexture.Create();
        return renderTexture;
    }
}