using System.IO;
using UnityEngine;

public static class RenderTextureUtils
{
    public static RenderTexture CreateRT(int size, RenderTextureFormat format = RenderTextureFormat.ARGBFloat)
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

    public static void SaveTexture(Texture2D texture, string path)
    {
        byte[] bytes = texture.EncodeToTGA();
        File.WriteAllBytes(path, bytes);
    }
    
    public static void SaveTexture(RenderTexture renderTexture, string path)
    {
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTexture;
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        RenderTexture.active = previous;

        byte[] bytes = texture.EncodeToTGA();
        File.WriteAllBytes(path, bytes);
    }
}