using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DnDToolsLibrary.Attacks
{
    public class SituationalAdvantageModifiers : INotifyPropertyChanged, ICloneable
    {

        public SituationalAdvantageModifiers() { }

        private void init_copy(SituationalAdvantageModifiers to_copy)
        {
            this.SituationalAdvantage = to_copy.SituationalAdvantage;
            this.SituationalDisadvantage = to_copy.SituationalDisadvantage;
        }

        public SituationalAdvantageModifiers(SituationalAdvantageModifiers to_copy) { init_copy(to_copy); }

        public object Clone() { return new SituationalAdvantageModifiers(this); }

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

        internal void Reset()
        {
            this.SituationalAdvantage = false;
            this.SituationalDisadvantage = false;
        }
        #endregion

    }
}
