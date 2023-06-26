using System;
using System.Collections.Generic;
using Command;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data
{
    public class CommandPreset
    {
        public string Name;
        public string Description;
        public CommandHandler Handler;

        public bool Match(Command.Command command)
        {
            return command.Name == Name && Handler.Match(command);
        }
    }
    
    [CreateAssetMenu]
    public class CommandPresetList : SerializedScriptableObject
    {
        [TableList][SerializeField] private List<CommandPreset> commands;

        public List<CommandPreset> Commands => commands;
    }
}