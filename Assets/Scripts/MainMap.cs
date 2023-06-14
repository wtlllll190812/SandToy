using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;

public class MainMap : SerializedMonoBehaviour
{
    [SerializeField] private bool useNoiseGenerator;
    [SerializeField] private float updateTime = 1;
    [SerializeField] private List<EvoluteLayer> layers;

    [SerializeField] [ShowIf("@useNoiseGenerator==true")]
    private GenNoise noiseGenerator;

    [SerializeField] [ShowIf("@useNoiseGenerator==false")]
    private int size;

    private static readonly int MapTex = Shader.PropertyToID("_MapTex");
    private Material mainMaterial;
    private RenderTexture texture;

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
        foreach (var item in layers)
            item.Init();

        StartCoroutine(Evolute());
    }

    private IEnumerator Evolute()
    {
        while (layers.Count > 0)
        {
            int seed = Random.Range(0, 10000);
            foreach (var item in layers.Where(item => item.enabled))
            {
                item.Execute(StartTexture, this, seed);
            }

            mainMaterial.SetTexture(MapTex, StartTexture);
            yield return new WaitForSeconds(updateTime);
        }
    }
}