using DDFight.Game.Dices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Windows.ModalWindows.BlankDiceRollModal
{
    public class BlankDiceRollModalDataContext : INotifyPropertyChanged
    {

        public string WindowTitle 
        {
            get => _windowTitle;
            set
            {
                _windowTitle = value;
                NotifyPropertyChanged();
            }
        }
        private string _windowTitle = "DiceRoll";

        public string WindowDesc
        {
            get => _windowDesc;
            set
            {
                _windowDesc = value;
                NotifyPropertyChanged();
            }
        }
        private string _windowDesc = "DiceRoll";

        public DiceRoll DiceRoll
        {
            get => _diceRoll;
            set
            {
                _diceRoll = value;
                NotifyPropertyChanged();
            }
        }
        private DiceRoll _diceRoll = new DiceRoll("1d4");

        public bool Validated = false;

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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
