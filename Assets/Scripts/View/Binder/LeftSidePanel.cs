using System;
using Command;
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
    [SerializeField] private RectTransform funcButton;
    
    [SerializeField] private GameObject brushSizeButtonPref;
    [SerializeField] private GameObject brushTypeButtonPref;
    [SerializeField] private GameObject funcButtonPref;
    
    [SerializeField] protected SpeciesUiPreset speciesUiItems;
    [SerializeField] protected BrushSizePreset brushSizePreset;
    [SerializeField] protected ViewModePreset viewModePreset;
    [SerializeField] protected FuncButtonPreset funcButtonPreset;
    
    protected void Start()
    {
        CreateBrushTypeButton();
        CreateBrushSizeButton();
        CreateViewModeButton();
        CreateFuncButton();
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
    
    protected void OnBrushSizeChange(int index)
    {
        OnBrushSizeChangeAction?.Invoke(index);
    }

    protected void OnBrushTypeChange(int index)
    {
        OnBrushTypeChangeAction?.Invoke(index);
    }
    
    protected void OnViewModeChange(int index)
    {
        OnViewModeChangeAction?.Invoke(index);
    }
    
    protected virtual void CreateBrushSizeButton()
    {
        
        foreach (var item in brushSizePreset.Presets)
        {
            var obj = Instantiate(brushSizeButtonPref, brushSize);
            obj.SetActive(true);
            obj.GetComponent<UiItem>().Init(item.Size, OnBrushSizeChange,item.Icon);
        }
    }
    
    protected virtual void CreateBrushTypeButton()
    {
        foreach (var item in speciesUiItems.SpeciesUiItems)
        {
            if (!item.Show&&!Debugger.IsDebug) continue;
            var obj = Instantiate(brushTypeButtonPref, brushType);
            obj.SetActive(true);
            obj.GetComponent<UiItem>().Init((int) item.Kind, OnBrushTypeChange, item.Icon, item.Name);
        }
    }
    
    protected virtual void CreateViewModeButton()
    {
        foreach (var mode in viewModePreset.Presets)
        {
            var obj = Instantiate(brushSizeButtonPref, displayerMode);
            obj.SetActive(true);
            obj.GetComponent<UiItem>().Init((int)mode.Mode, OnViewModeChange, mode.Icon);
        }
    }
    
    protected virtual void CreateFuncButton()
    {
        var index = 0;
        foreach (var func in funcButtonPreset.Presets)
        {
            var obj = Instantiate(funcButtonPref, funcButton);
            obj.SetActive(true);
            obj.GetComponent<UiItem>().Init((int)index, (i) =>
            {
                var function = funcButtonPreset.Presets[i];
                CommandSystem.Instance.HandleCommand(function.command);
            }, func.Icon);
            index++;
        }
    }
}