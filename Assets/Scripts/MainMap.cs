using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;

public class MainMap : SerializedMonoBehaviour
{
    [SerializeField] private GenNoise noiseGenerator;
    [SerializeField] private float updateTime = 1;
    [SerializeField] private List<EvoluteLayer> layers;


    private static readonly int MapTex = Shader.PropertyToID("_MapTex");
    private Material mainMaterial;

    private void Start()
    {
        mainMaterial = GetComponent<SpriteRenderer>().material;
        mainMaterial.SetTexture(MapTex, noiseGenerator.renderTexture);
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
                item.Execute(noiseGenerator.renderTexture, this, seed);
            }

            mainMaterial.SetTexture(MapTex, noiseGenerator.renderTexture);
            yield return new WaitForSeconds(updateTime);
        }
    }
}