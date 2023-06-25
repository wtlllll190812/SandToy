using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;

public class MainMap : SerializedMonoBehaviour
{
    public bool onDebug;
    [SerializeField] private Displayer displayer;
    [SerializeField] private bool useNoiseGenerator;
    [SerializeField] [ShowIf("@useNoiseGenerator==true")]
    private GenNoise noiseGenerator;
    [SerializeField] [ShowIf("@useNoiseGenerator==false")]
    private int size;
    [SerializeField][TableList] private List<IEvoluteLayer> layers;
    private RenderTexture texture;

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
        var monoLayers = GetComponentsInChildren<IEvoluteLayer>().ToList();
        layers = layers.Union(monoLayers).ToList();
        foreach (var item in layers)
            item.Init(this);
    }

    private void Update()
    {
        var seed = Random.Range(0, 10000);
        foreach (var item in layers)
        {
            if (!item.IsReady()) continue;
            item.Execute(seed);
        }
    }
}