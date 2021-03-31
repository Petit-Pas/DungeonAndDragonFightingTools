using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WpfToolsLibrary.ConsoleTools
{
    public class FightConsole : INotifyPropertyChanged
    {
        private FightConsole()
        {
        }

        public void Reset()
        {
            _instance = new FightConsole();
        }

        public static FightConsole Instance
        {
            get => _instance;
        }
        private static FightConsole _instance = new FightConsole();

        /// <summary>
        ///     Used to log what happens on the user side
        /// </summary>
        public FlowDocument UserLogs
        {
            get
            {
                return _userLogs;
            }
            set
            {
                _userLogs = value;
                NotifyPropertyChanged();
            }
        }
        private FlowDocument _userLogs = new FlowDocument();

        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
