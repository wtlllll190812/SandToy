using System;

namespace Command
{
    [Serializable]
    public class SetTemperatureCommand: CommandHandler
    {
        public override void HandleCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public override bool Match(Command command)
        {
            var count = command.Parmas.Count;
            if (count != 1) return false;
            var param = command.Parmas[0];
            return param == "temperature";
        }
    }
}