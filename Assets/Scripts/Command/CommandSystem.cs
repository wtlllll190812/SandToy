using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Command
{
    public class CommandSystem : MonoBehaviour
    {
        [SerializeField] private Data.CommandPresetList commandPresetList;

        [Button]
        public void HandleCommand(Command command)
        {
            foreach (var commandPreset in commandPresetList.Commands.Where(commandPreset => commandPreset.Match(command)))
            {
                commandPreset.Handler.HandleCommand(command);
            }
        }
    }
}
