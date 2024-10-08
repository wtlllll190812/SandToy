using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class FuncButtonPresetData
    {
        public Command.Command command;
        public Sprite Icon;
        public bool OnlyForDebug;
    }
    
    [CreateAssetMenu]
    public class FuncButtonPreset: SerializedScriptableObject
    {
        [TableList] public List<FuncButtonPresetData> Presets;
    }
}