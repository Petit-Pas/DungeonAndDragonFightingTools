using DDFight.Game.DamageAffinity;
using DDFight.Game.Dices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDFight.Game.Aggression
{
    /// <summary>
    ///     Represents a possible damage, example: 1d6+2 of Fire Damage
    ///     Default Values : 1d4 of Force Damage
    /// </summary>
    public class DamageTemplate : ICloneable, INotifyPropertyChanged
    {
        public DamageTemplate() 
        { 
        }

        public DamageTemplate(string damageFormat, DamageTypeEnum damageType)
        {
            Damage = new DiceRoll(damageFormat);
            DamageType = damageType;
        }

        #region Properties
            
        /// <summary>
        ///     The dices to throw
        /// </summary>
        public DiceRoll Damage
        {
            get => _damage;
            set {
                _damage = value;
                NotifyPropertyChanged();
            }
        }
        private DiceRoll _damage = new DiceRoll("1d4");

        [XmlAttribute]
        public DamageTypeEnum DamageType
        {
            get => _damageType;
            set {
                _damageType = value;
                NotifyPropertyChanged();
            }
        }
        private DamageTypeEnum _damageType = DamageTypeEnum.Force;

        #endregion

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

        protected DamageTemplate(DamageTemplate to_copy)
        {
            Damage = (DiceRoll)to_copy.Damage.Clone();
            DamageType = to_copy.DamageType;
        }

        public object Clone()
        {
            return new DamageTemplate(this);
        }

        #endregion
    }
}
