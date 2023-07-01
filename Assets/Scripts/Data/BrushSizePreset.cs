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

        public int GetSize(float value)
        {
            var index = (int) (value * presets.Count);
            if (index >= presets.Count)
                index = presets.Count - 1;
            return presets[index].Size;
        }
    }
}