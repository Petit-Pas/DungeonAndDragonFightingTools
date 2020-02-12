using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DDFight.Game.Aggression.Attacks
{
    public class SituationalAdvantageModifiers : INotifyPropertyChanged
    {
        public bool SituationalAdvantage
        {
            get => _situationalAdvantage;
            set
            {
                _situationalAdvantage = value;
                NotifyPropertyChanged();
            }
        }
        private bool _situationalAdvantage = false;

        public bool SituationalDisadvantage
        {
            get => _situationalDisadvantage;
            set
            {
                _situationalDisadvantage = value;
                NotifyPropertyChanged();
            }
        }
        private bool _situationalDisadvantage = false;

        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
