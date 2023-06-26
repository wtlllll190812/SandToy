using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu]
public class ColorTexturePreset : SerializedScriptableObject
{
    [SerializeField] private string path;
    [SerializeField] private int maxElementNum = 32;
    [ColorUsage(true, true)]
    [SerializeField] private List<Color> colors;

    private void Reset()
    {
        colors = new List<Color>();
        for (var i = 0; i < maxElementNum - colors.Count; i++)
        {
            colors.Add(Color.white);
        }
    }

    public Texture2D GetTexture()
    {
        var colorTex = new Texture2D(maxElementNum, 1)
        {
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Point
        };
        colorTex.SetPixels(colors.ToArray());
        colorTex.Apply();
        return colorTex;
    }

    [Button("Save")]
    public void Save()
    {
        RenderTextureUtils.SaveTexture(GetTexture(), path);
    }
}