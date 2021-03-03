﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DDFight.Game.Aggression.Attacks
{
    public class SituationalHitAttackModifiers : INotifyPropertyChanged
    {
        /// <summary>
        ///     can be provided by various elements, such as cover
        /// </summary>
        public int HitModifier
        {
            get => _hitModifier;
            set
            {
                _hitModifier = value;
                NotifyPropertyChanged();
            }
        }
        private int _hitModifier;

        public int ACModifier
        {
            get => _ACModifier;
            set
            {
                _ACModifier = value;
                NotifyPropertyChanged();
            }
        }
        private int _ACModifier;

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

        public void Reset()
        {
            this.ACModifier = 0;
            this.HitModifier = 0;
        }
        #endregion

    }
}
