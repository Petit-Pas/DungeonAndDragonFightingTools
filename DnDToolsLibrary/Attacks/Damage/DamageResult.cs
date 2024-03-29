﻿using BaseToolsLibrary;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Dice;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Attacks.Damage
{
    public class DamageResult : ICloneable, INotifyPropertyChanged, IEquivalentComparable<DamageResult>
    {
        public DamageResult()
        {
        }

        public DamageResult(string damage, DamageTypeEnum type)
        {
            Damage = new DiceRoll(damage);
            DamageType = type;
        }

        public static DamageResult Create(string damage, DamageTypeEnum type)
        {
            return new DamageResult(damage, type);
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
        [XmlAttribute]
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
        ///     Should be set to the default of the Target but can always be overriden
        /// </summary>
        [XmlAttribute]
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

        [XmlAttribute]
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

        [XmlAttribute]
        public bool LinkedToSaving
        {
            get => _linkedToSaving;
            set
            {
                if (_linkedToSaving != value)
                {
                    _linkedToSaving = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _linkedToSaving = true;


        private void init_copy(DamageResult to_copy)
        {
            this.LastSavingWasSuccesfull = to_copy.LastSavingWasSuccesfull;
            this.SituationalDamageModifier = to_copy.SituationalDamageModifier;
            this.Damage = to_copy.Damage.Clone() as DiceRoll;
            this.DamageType = to_copy.DamageType;
            this.LinkedToSaving = to_copy.LinkedToSaving;
            this.AffinityModifier = to_copy.AffinityModifier;
        }
        
        public DamageResult(DamageTemplate template, bool linked_to_saving = true)
        {
            this.SituationalDamageModifier = template.SituationalDamageModifier;
            this.Damage = (DiceRoll)template.Damage.Clone();
            this.DamageType = template.DamageType;
            this.LinkedToSaving = linked_to_saving;
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

        public bool IsEquivalentTo(DamageResult toCompare)
        {
            if (this.AffinityModifier != toCompare.AffinityModifier)
                return false;
            if (!this.Damage.IsEquivalentTo(toCompare.Damage))
                return false;
            if (this.DamageType != toCompare.DamageType)
                return false;
            if (this.LastSavingWasSuccesfull != toCompare.LastSavingWasSuccesfull)
                return false;
            if (this.LinkedToSaving != toCompare.LinkedToSaving)
                return false;
            if (this.SituationalDamageModifier != toCompare.SituationalDamageModifier)
                return false;
            if (this.ToRollBonus != toCompare.ToRollBonus)
                return false;
            if (this.ToRollDamage != ToRollDamage)
                return false;
            return true;
        }

    }
}
