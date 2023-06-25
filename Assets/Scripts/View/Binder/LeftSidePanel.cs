using System;
using TMPro;
using UnityEngine;

public class LeftSidePanel : MonoBehaviour
{
    private static Action<float> OnBrushSizeChangeAction;
    private static Action<int> OnBrushTypeChangeAction;
    private static Action<int> OnViewModeChangeAction;
    [SerializeField] private int sizeLevel;
    [SerializeField] private RectTransform brushSize;
    [SerializeField] private RectTransform brushType;
    [SerializeField] private RectTransform displayerMode;
    [SerializeField] private GameObject brushSizeButtonPref;
    [SerializeField] private GameObject brushTypeButtonPref;
    [SerializeField] private SpeciesUiPreset speciesUiItems;
    
    private void Awake()
    {
        foreach (int mode in Enum.GetValues(typeof(Displayer.DisplayMode)))
        {
            var obj = Instantiate(brushSizeButtonPref, displayerMode);
            obj.GetComponent<UiItem>().Init(mode, OnViewModeChange);
        }
        
        for (var i = 1; i <= sizeLevel; i++)
        {
            var obj = Instantiate(brushSizeButtonPref, brushSize);
            obj.GetComponent<UiItem>().Init(i, OnBrushSizeChange);
        }

        foreach (var item in speciesUiItems.SpeciesUiItems)
        {
            if (!item.Show) continue;
            var obj = Instantiate(brushTypeButtonPref, brushType);
            obj.GetComponent<UiItem>().Init((int) item.Kind, OnBrushTypeChange, item.Icon, item.Name);
        }
    }

    private void Start()
    {
        OnBrushSizeChange(2);
        OnBrushTypeChange((int) Species.Sand);
    }

    public static void RegisterOnBrushSizeChange(Action<float> action)
    {
        OnBrushSizeChangeAction += action;
    }

    public static void RegisterOnBrushTypeChange(Action<int> action)
    {
        OnBrushTypeChangeAction += action;
    }

    public static void RegisterOnViewModeChange(Action<int> action)
    {
        OnViewModeChangeAction += action;
    }
    
    private void OnBrushSizeChange(int index)
    {
        OnBrushSizeChangeAction?.Invoke((float) index / sizeLevel);
    }

    private void OnBrushTypeChange(int index)
    {
        OnBrushTypeChangeAction?.Invoke(index);
    }
    
    private void OnViewModeChange(int index)
    {
        OnViewModeChangeAction?.Invoke(index);
    }
}
