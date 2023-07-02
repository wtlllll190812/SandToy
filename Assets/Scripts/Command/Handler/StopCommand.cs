namespace Command
{
    public class StopCommand: CommandHandler
    {
        public override void HandleCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public override bool Match(Command command)
        {
            return command.Name=="stop"&&command.Parmas.Count==0;
        }
    }
}