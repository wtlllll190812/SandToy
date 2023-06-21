using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrushBinder : MonoBehaviour
{
    private static Action<float> OnBrushSizeChangeAction;
    private static Action<int> OnBrushTypeChangeAction;
    [SerializeField] private TMP_Dropdown brushTypeDropdown;
    [SerializeField] private Slider brushSizeSlider;

    private void Start()
    {
        brushTypeDropdown.options.Clear();
        foreach (var type in Enum.GetValues(typeof(Species)))
            brushTypeDropdown.options.Add(new TMP_Dropdown.OptionData(type.ToString()));
        brushTypeDropdown.onValueChanged.AddListener(OnBrushTypeChange);
        brushSizeSlider.onValueChanged.AddListener(OnBrushSizeChange);
    }

    public static void RegisterOnBrushSizeChange(Action<float> action)
    {
        OnBrushSizeChangeAction += action;
    }

    public static void RegisterOnBrushTypeChange(Action<int> action)
    {
        OnBrushTypeChangeAction += action;
    }

    public void OnBrushSizeChange(float value)
    {
        OnBrushSizeChangeAction?.Invoke(value);
    }

    public void OnBrushTypeChange(int value)
    {
        OnBrushTypeChangeAction?.Invoke(value);
    }
}
