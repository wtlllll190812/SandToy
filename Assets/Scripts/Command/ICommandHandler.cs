namespace Command
{
    public interface ICommandHandler
    {
        public void HandleCommand(Command command);
        public bool Match(Command command);
    }
}