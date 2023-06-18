using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MainMap : SerializedMonoBehaviour
{
    [SerializeField] private Displayer displayer;
    [SerializeField] private bool useNoiseGenerator;
    [SerializeField] [ShowIf("@useNoiseGenerator==true")]
    private GenNoise noiseGenerator;
    [SerializeField] [ShowIf("@useNoiseGenerator==false")]
    private int size;
    
    private RenderTexture texture;
    private List<EvoluteLayer> layers;

    public RenderTexture BasicTexture
    {
        get
        {
            if (!useNoiseGenerator)
                texture ??= RenderTextureUtils.CreateRT(size);
            return useNoiseGenerator ? noiseGenerator.renderTexture : texture;
        }
    }

    public RenderTexture EnvironmentTexture { private set; get; }

    private void Awake()
    {
        EnvironmentTexture = RenderTextureUtils.CreateRT(size);
    }

    private void Start()
    {
        displayer.Init(this);
        layers = GetComponents<EvoluteLayer>().ToList();
        foreach (var item in layers)
            item.Init(this);
    }

    private void Update()
    {
        var seed = Random.Range(0, 10000);
        foreach (var item in layers.Where(item => item.enabled))
        {
            if (!item.ready) continue;
            item.Execute(seed);
        }
    }
}