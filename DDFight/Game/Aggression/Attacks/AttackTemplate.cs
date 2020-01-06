using DDFight.Game.DamageAffinity;
using DDFight.Game.Dices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Aggression.Attacks
{
    public class AttackTemplate : Aggression, ICloneable, INotifyPropertyChanged
    {
        public AttackTemplate()
        {
        }

        public int AttackBonus
        {
            get => _attackBonus;
            set
            {
                _attackBonus = value;
                NotifyPropertyChanged();
            }
        }
        private int _attackBonus = 0;

        public DamageAffinityEnum DamageType
        {
            get => _damageType;
            set {
                _damageType = value;
                NotifyPropertyChanged();
            }
        }
        private DamageAffinityEnum _damageType;

        public DiceRoll Damage
        {
            get => _damage;
            set {
                _damage = value;
                NotifyPropertyChanged();
            }
        }
        private DiceRoll _damage;


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

        protected AttackTemplate(AttackTemplate to_copy)
        {
            AttackBonus = to_copy.AttackBonus;
            DamageType = to_copy.DamageType;
            Damage = (DiceRoll)to_copy.Damage.Clone();
        }

        public object Clone()
        {
            return new AttackTemplate(this);
        }

        #endregion
    }
}
