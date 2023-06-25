using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrushSelectUi : MonoBehaviour
{
    private static Action<float> OnBrushSizeChangeAction;
    private static Action<int> OnBrushTypeChangeAction;
    [SerializeField] private int sizeLevel;
    [SerializeField] private RectTransform brushSize;
    [SerializeField] private RectTransform brushType;
    [SerializeField] private GameObject brushSizeButtonPref;
    [SerializeField] private GameObject brushTypeButtonPref;
    [SerializeField] private SpeciesUiPreset speciesUiItems;
    
    private void Awake()
    {
        for(int i = 1; i <= sizeLevel; i++)
        {
            var obj = Instantiate(brushSizeButtonPref, brushSize);
            obj.GetComponent<UiItem>().Init(i, OnBrushSizeChange);
        }

        foreach (var item in speciesUiItems.SpeciesUiItems)
        {
            var obj = Instantiate(brushTypeButtonPref, brushType);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = item.Name;
            obj.GetComponent<UiItem>().Init((int)item.Kind, OnBrushTypeChange);
        }
    }

    public static void RegisterOnBrushSizeChange(Action<float> action)
    {
        OnBrushSizeChangeAction += action;
    }

    public static void RegisterOnBrushTypeChange(Action<int> action)
    {
        OnBrushTypeChangeAction += action;
    }

    public void OnBrushSizeChange(int index)
    {
        OnBrushSizeChangeAction?.Invoke((float)index/sizeLevel);
    }

    public void OnBrushTypeChange(int index)
    {
        OnBrushTypeChangeAction?.Invoke(index);
    }
}
