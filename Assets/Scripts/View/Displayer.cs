using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Displayer : MonoBehaviour
{
    public enum DisplayMode
    {
        Basic,
        Temperature,
    }

    private static readonly int MapTex = Shader.PropertyToID("_MapTex");
    private static readonly int ColorTex = Shader.PropertyToID("_ColorTex");
    
    [SerializeField] private Material mainMaterial;
    [SerializeField] private Material temperatureMaterial;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private ColorTexturePreset colorTexturePreset;

    private MainMap mainMap;

    private void Awake()
    {
        LeftSidePanel.RegisterOnViewModeChange(ChangeDisplayMode);
    }

    public void Init(MainMap map)
    {
        mainMap = map;
        mainMaterial.SetTexture(MapTex, mainMap.BasicTexture);
        mainMaterial.SetTexture(ColorTex, colorTexturePreset.GetTexture());
        temperatureMaterial.SetTexture(MapTex, mainMap.EnvironmentTexture);
    }

    [Button]
    public void ChangeDisplayMode(int mode)
    {
        switch ((DisplayMode)mode)
        {
            case DisplayMode.Basic:
                spriteRenderer.material = mainMaterial;
                break;
            case DisplayMode.Temperature:
                spriteRenderer.material = temperatureMaterial;
                break;
            default:
                break;
        }
    }
}
