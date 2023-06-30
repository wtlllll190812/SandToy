using System;
using Data;
using UnityEngine;

public class LeftSidePanel : MonoBehaviour
{
    private static Action<int> OnBrushSizeChangeAction;
    private static Action<int> OnBrushTypeChangeAction;
    private static Action<int> OnViewModeChangeAction;
    [SerializeField] private RectTransform brushSize;
    [SerializeField] private RectTransform brushType;
    [SerializeField] private RectTransform displayerMode;
    
    [SerializeField] private GameObject brushSizeButtonPref;
    [SerializeField] private GameObject brushTypeButtonPref;
    
    [SerializeField] private SpeciesUiPreset speciesUiItems;
    [SerializeField] private BrushSizePreset brushSizePreset;
    [SerializeField] private ViewModePreset viewModePreset;
    
    private void Start()
    {
        foreach (var mode in viewModePreset.Presets)
        {
            var obj = Instantiate(brushSizeButtonPref, displayerMode);
            obj.SetActive(true);
            obj.GetComponent<UiItem>().Init((int)mode.Mode, OnViewModeChange, mode.Icon);
        }

        foreach (var item in brushSizePreset.Presets)
        {
            var obj = Instantiate(brushSizeButtonPref, brushSize);
            obj.SetActive(true);
            obj.GetComponent<UiItem>().Init(item.Size, OnBrushSizeChange,item.Icon);
        }

        foreach (var item in speciesUiItems.SpeciesUiItems)
        {
            if (!item.Show&&!Debugger.IsDebug) continue;
            var obj = Instantiate(brushTypeButtonPref, brushType);
            obj.SetActive(true);
            obj.GetComponent<UiItem>().Init((int) item.Kind, OnBrushTypeChange, item.Icon, item.Name);
        }
        OnBrushSizeChange(2);
        OnBrushTypeChange((int) Species.Sand);
    }

    public static void RegisterOnBrushSizeChange(Action<int> action)
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
        OnBrushSizeChangeAction?.Invoke(index);
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