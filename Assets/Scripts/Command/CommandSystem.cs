using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Command
{
    public class CommandSystem : MonoBehaviour
    {
        private static CommandSystem instance;
        [SerializeField] private Data.CommandPresetList commandPresetList;

        public static CommandSystem Instance => instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        [Button]
        public void HandleCommand(Command command)
        {
            foreach (var commandPreset in commandPresetList.Commands.Where(commandPreset => commandPreset.Match(command)))
            {
                commandPreset.Handler.HandleCommand(command);
            }
        }
        
        [Button]
        public Command CreateCommand(string commandString)
        {
            var com=commandString.Split(' ');
            var command = new Command(com[0], com.Skip(1).ToList());
            return command;
        }
    }
}
