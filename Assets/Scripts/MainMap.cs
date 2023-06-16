using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;

public class MainMap : SerializedMonoBehaviour
{
    [SerializeField] private bool useNoiseGenerator;
    [SerializeField] [ShowIf("@useNoiseGenerator==true")]
    private GenNoise noiseGenerator;
    [SerializeField] [ShowIf("@useNoiseGenerator==false")]
    private int size;
    
    [SerializeField] private ColorTexturePreset colorTexturePreset;
    
    private static readonly int MapTex = Shader.PropertyToID("_MapTex");
    private static readonly int ColorTex = Shader.PropertyToID("_ColorTex");
    private Material mainMaterial;
    private RenderTexture texture;
    private List<EvoluteLayer> layers;

    private RenderTexture StartTexture
    {
        get
        {
            if (!useNoiseGenerator)
                texture ??= RenderTextureUtils.CreateRT(size);
            return useNoiseGenerator ? noiseGenerator.renderTexture : texture;
        }
    }
    
    private void Start()
    {
        mainMaterial = GetComponent<SpriteRenderer>().material;
        mainMaterial.SetTexture(MapTex, StartTexture);
        mainMaterial.SetTexture(ColorTex, colorTexturePreset.GetTexture());
        layers = GetComponents<EvoluteLayer>().ToList();
        foreach (var item in layers)
            item.Init(StartTexture);
    }

    private void Update()
    {
        var seed = Random.Range(0, 10000);
        foreach (var item in layers.Where(item => item.enabled))
        {
            if (!item.ready) continue;
            item.Execute(StartTexture, this, seed);
        }
    }
}