using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Aggression.Attacks
{
    public class SituationalHitAttackModifiers : INotifyPropertyChanged
    {
        /// <summary>
        ///     can be provided by various elements, such as cover
        /// </summary>
        public int HitModifier
        {
            get => _situationalHitModifier;
            set
            {
                _situationalHitModifier = value;
                NotifyPropertyChanged();
            }
        }
        private int _situationalHitModifier;

        public int ACModifier
        {
            get => _situationalACModifier;
            set
            {
                _situationalACModifier = value;
                NotifyPropertyChanged();
            }
        }
        private int _situationalACModifier;

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
