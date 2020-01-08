﻿using DDFight.Game.Aggression.Attacks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Aggression
{
    public class Aggression : ICloneable, INotifyPropertyChanged
    {
        Aggression()
        {
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        private string _name;

        public List<AAttackTemplate> AttackTemplatesList
        {
            get => _attackTemplatesList;
            set
            {
                _attackTemplatesList = value;
                NotifyPropertyChanged();
            }
        }
        private List<AAttackTemplate> _attackTemplatesList = new List<AAttackTemplate>();

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

        #region ICloneable

        protected Aggression(Aggression to_copy)
        {
            AttackTemplatesList = (List<AAttackTemplate>)to_copy.AttackTemplatesList.Clone();
            Name = (string)to_copy.Name.Clone();
        }

        public object Clone()
        {
            return new Aggression(this);
        }

        #endregion
    }
}
