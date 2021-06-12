namespace DDFight.Commands
{
    public class DnDCommandManager
    {
        private DnDCommandManager() { }

        public static DnDCommandManager Instance
        {
            get => _instance;
        }
        private static DnDCommandManager _instance = new DnDCommandManager();

        public static bool StaticTryExecute(IDnDCommand command, object paramter = null)
        {
            return DnDCommandManager.Instance.TryExecute(command, paramter);
        }

        public bool TryExecute(IDnDCommand command, object parameter = null)
        {
            if (command.CanExecute(parameter))
            {
                command.Execute(parameter);
                return true;
            }
            return false;
        }
    }
}
