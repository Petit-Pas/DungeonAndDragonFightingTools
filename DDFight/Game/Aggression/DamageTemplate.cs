using DDFight.Game.DamageAffinity;
using DDFight.Game.Dices;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Ink;
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
            
        public string ToRollDamage
        {
            get => Damage.RollTemplateToString();
        }

        public string ToRollBonus
        {
            get
            {
                int tmp = Damage.Modifier;
                return (tmp >= 0 ? ("+" + tmp.ToString()) : tmp.ToString());
            }
        }

        /// <summary>
        ///     Set to true when SituationalDamageModifier should be taken into account
        ///     Should always be resetted to false after use
        /// </summary>
        [XmlIgnore]
        public bool LastSavingWasSuccesfull
        {
            get => _lastSavingWasSuccesfull;
            set
            {
                _lastSavingWasSuccesfull = value;
                NotifyPropertyChanged();
            }
        }
        private bool _lastSavingWasSuccesfull = false;

        /// <summary>
        ///     Can be used to determine what happens in case of a successful saving Throw (OnHitStatus damage, spells, etc...)
        /// </summary>
        [XmlAttribute]
        public DamageModifierEnum SituationalDamageModifier
        {
            get => _temporaryDamageModifier;
            set 
            {
                _temporaryDamageModifier = value;
                NotifyPropertyChanged();
            }
        }
        private DamageModifierEnum _temporaryDamageModifier = DamageModifierEnum.Normal;
            

        /// <summary>
        ///     The dices to throw
        /// </summary>
        public DiceRoll Damage
        {
            get => _damage;
            set {
                _damage.PropertyChanged -= _damage_PropertyChanged;
                _damage = value;
                _damage.PropertyChanged += _damage_PropertyChanged;
                NotifyPropertyChanged();
            }
        }

        private void _damage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged("Damage");
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
            SituationalDamageModifier = to_copy.SituationalDamageModifier;
        }

        public object Clone()
        {
            return new DamageTemplate(this);
        }

        #endregion
    }
}
