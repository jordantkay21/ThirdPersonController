public abstract class ICommand
{
    /// <summary>
    /// Performs the core action or behavior associated with this command. Use to trigger the main logic or functionality that this command is responsible for.
    /// </summary>
    /// <param name="controller"></param>
    public abstract void Execute(PlayerController controller);

}
