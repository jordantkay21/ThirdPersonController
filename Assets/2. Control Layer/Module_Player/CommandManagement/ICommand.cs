namespace KayosStudios.ThirdPersonController
{
    public interface ICommand
    {
        /// <summary>
        /// Performs the core action or behavior associated with this command. Use to trigger the main logic or functionality that this command is responsible for.
        /// </summary>
        /// <param name="controller"></param>
        public void Execute(PlayerController controller);

    }
}
