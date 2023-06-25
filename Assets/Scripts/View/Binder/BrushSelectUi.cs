using System;
using TMPro;
using UnityEngine;

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
        for (var i = 1; i <= sizeLevel; i++)
        {
            var obj = Instantiate(brushSizeButtonPref, brushSize);
            obj.GetComponent<UiItem>().Init(i, OnBrushSizeChange, null);
        }

        foreach (var item in speciesUiItems.SpeciesUiItems)
        {
            if (!item.Show) continue;
            var obj = Instantiate(brushTypeButtonPref, brushType);
            obj.GetComponent<UiItem>().Init((int) item.Kind, OnBrushTypeChange, item.Icon, item.Name);
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

    private void OnBrushSizeChange(int index)
    {
        OnBrushSizeChangeAction?.Invoke((float) index / sizeLevel);
    }

    private void OnBrushTypeChange(int index)
    {
        OnBrushTypeChangeAction?.Invoke(index);
    }
}
