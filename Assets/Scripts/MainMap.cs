using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;

public class MainMap : SerializedMonoBehaviour
{
    private static MainMap instance;
    public static MainMap Instance => instance;

    [SerializeField] private string path;
    [SerializeField] private bool useNoiseGenerator;
    [SerializeField] private Displayer displayer;

    [SerializeField] [ShowIf("@useNoiseGenerator==true")]
    private GenNoise noiseGenerator;

    [SerializeField] [ShowIf("@useNoiseGenerator==false")]
    private int size;

    [SerializeField] [TableList] private List<IEvoluteLayer> layers;
    private RenderTexture texture;
    private EvoBrush brush;
    private bool isStop;

    public int Size => size;

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
        brush = GetComponent<EvoBrush>();
    }

    [Button]
    public void SaveTexture()
    {
        RenderTextureUtils.SaveTexture(BasicTexture, path + "/basic.tga");
        RenderTextureUtils.SaveTexture(EnvironmentTexture, path + "/environment.tga");
    }

    public void Clear()
    {
        brush.Clear();
    }

    public void Stop()
    {
        isStop = !isStop;
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
        foreach (var item in layers.Where(item => item.IsReady() && (item is EvoBrush || !isStop)))
        {
            item.Execute(seed);
        }
    }
}