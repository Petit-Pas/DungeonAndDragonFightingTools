using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Dice;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DnDToolsLibrary.Attacks.Damage
{
    public class DamageResult : ICloneable, INotifyPropertyChanged
    {
        public DamageResult()
        {
        }

        public bool IsSameKind(DamageTemplate template)
        {
            if (DamageType == template.DamageType)
                if (SituationalDamageModifier == template.SituationalDamageModifier)
                    return true;
            return false;
        }

        public void Add(DamageTemplate to_combine)
        {
            if (!this.IsSameKind(to_combine))
                throw new Exception("Trying to combine non likely DamageTemplates, try using DamageTemplate.IsSameKind() before calling DamageTemplate.Add()");
            foreach (DnDToolsLibrary.Dice.Dices dices in to_combine.Damage.DicesList)
            {
                this.Damage.AddDice(dices);
            }
            this.Damage.Modifier += to_combine.Damage.Modifier;
        }

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

        public DamageAffinityEnum AffinityModifier
        {
            get => _affinityModifier;
            set
            {
                _affinityModifier = value;
                NotifyPropertyChanged();
            }
        }
        private DamageAffinityEnum _affinityModifier = DamageAffinityEnum.Unspecified;

        /// <summary>
        ///     The dices to throw
        /// </summary>
        public DiceRoll Damage
        {
            get => _damage;
            set
            {
                _damage.PropertyChanged -= _damage_PropertyChanged;
                _damage = value;
                _damage.PropertyChanged += _damage_PropertyChanged;
                NotifyPropertyChanged();
            }
        }
        private DiceRoll _damage = new DiceRoll("1d4");

        /// <summary>
        ///     links the changes in the DiceRoll to the NotifyPropertyChanged of the DamageResult
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _damage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged("Damage");
        }

        public void Reset()
        {
            Damage.Reset();
        }

        public DamageTypeEnum DamageType
        {
            get => _damageType;
            set
            {
                _damageType = value;
                NotifyPropertyChanged();
            }
        }

        private DamageTypeEnum _damageType = DamageTypeEnum.Force;


        private void init_copy(DamageResult to_copy)
        {
            this.LastSavingWasSuccesfull = to_copy.LastSavingWasSuccesfull;
            this.SituationalDamageModifier = to_copy.SituationalDamageModifier;
            this.Damage = (DiceRoll)to_copy.Damage.Clone();
            this.DamageType = to_copy.DamageType;
        }
        public DamageResult(DamageTemplate template)
        {
            this.SituationalDamageModifier = template.SituationalDamageModifier;
            this.Damage = (DiceRoll)template.Damage.Clone();
            this.DamageType = template.DamageType;
        }

        public DamageResult(DamageResult to_copy)
        {
            init_copy(to_copy);
        }

        public object Clone()
        {
            return new DamageResult(this);
        }

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
