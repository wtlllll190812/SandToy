using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data
{
    public struct BrushSizePresetData
    {
        public int Size;
        public Sprite Icon;
    }

    [CreateAssetMenu]
    public class BrushSizePreset : SerializedScriptableObject
    {
        [TableList] [SerializeField] private List<BrushSizePresetData> presets;

        public List<BrushSizePresetData> Presets => presets;
    }
}