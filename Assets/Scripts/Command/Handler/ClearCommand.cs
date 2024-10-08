namespace Command
{
    public class ClearCommand : CommandHandler
    {
        public override void HandleCommand(Command command)
        {
            MainMap.Instance.Clear();
        }

        public override bool Match(Command command)
        {
            return command.Name == "clear" && command.Parmas.Count == 0;
        }
    }
}