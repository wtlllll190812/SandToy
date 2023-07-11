using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data
{
    public struct ViewModePresetData
    {
        public Displayer.DisplayMode Mode;
        public Sprite Icon;
        public bool OnlyForDebug;
    }
    
    [CreateAssetMenu]
    public class ViewModePreset: SerializedScriptableObject
    {
        [TableList] [SerializeField] private List<ViewModePresetData> presets;

        public List<ViewModePresetData> Presets => presets;
    }
}