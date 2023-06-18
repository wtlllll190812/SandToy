using UnityEngine;

public static class RenderTextureUtils
{
    public static RenderTexture CreateRT(int size, RenderTextureFormat format = RenderTextureFormat.ARGB64)
    {
        var renderTexture = new RenderTexture(size, size, 0, format, RenderTextureReadWrite.Linear)
        {
            enableRandomWrite = true,
            filterMode = FilterMode.Point,
            wrapMode = TextureWrapMode.Repeat,
        };
        renderTexture.Create();
        return renderTexture;
    }
}