using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Aggression.Attacks
{
    public abstract class AAttackTemplate : INotifyPropertyChanged, ICloneable
    {
        public AAttackTemplate()
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

        protected AAttackTemplate(AAttackTemplate to_copy)
        {
            Name = (string)to_copy.Name.Clone();
        }

        public object Clone()
        {
            throw new MissingMethodException("Cannot clone an instance of AAttackTemplate");
        }
        #endregion
    }
}
