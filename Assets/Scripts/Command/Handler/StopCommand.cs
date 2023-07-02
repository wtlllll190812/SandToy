namespace Command
{
    public class StopCommand: CommandHandler
    {
        public override void HandleCommand(Command command)
        {
            MainMap.Instance.Stop();
        }

        public override bool Match(Command command)
        {
            return command.Name=="stop"&&command.Parmas.Count==0;
        }
    }
}