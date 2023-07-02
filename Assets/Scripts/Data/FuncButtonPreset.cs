using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class FuncButtonPresetData
    {
        public string Name;
        public Sprite Icon;
    }
    
    [CreateAssetMenu]
    public class FuncButtonPreset: SerializedScriptableObject
    {
        [TableList] public List<FuncButtonPresetData> Presets;
    }
}