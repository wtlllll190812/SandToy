using UnityEngine;

namespace Command
{
    public abstract class CommandHandler
    {
        public abstract void HandleCommand(Command command);
        public abstract bool Match(Command command);
    }
}