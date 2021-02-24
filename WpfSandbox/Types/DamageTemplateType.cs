﻿using DDFight.Game.Aggression;
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

namespace WpfSandbox.Types
{
    public class DamageTemplateType : ICloneable, INotifyPropertyChanged
    {
        public static List<DamageTemplate> ConvertList(IEnumerable<DamageTemplateType> list)
        {
            List<DamageTemplate> result = new List<DamageTemplate>();
            foreach (DamageTemplateType type in list)
                result.Add(Convert(type));
            return result;
        }

        private static DamageTemplate Convert(DamageTemplateType type)
        {
            DamageTemplate result = new DamageTemplate()
            {
                Damage = type.Damage,
                DamageType = type.DamageType,
                LastSavingWasSuccesfull = type.LastSavingWasSuccesfull,
                SituationalDamageModifier = type.SituationalDamageModifier,
            };
            return result;
        }

        public DamageTemplateType()
        {
        }

        public bool IsSameKind(DamageTemplateType template)
        {
            if (DamageType == template.DamageType)
                if (SituationalDamageModifier == template.SituationalDamageModifier)
                    return true;
            return false;
        }

        public void Add(DamageTemplateType to_combine)
        {
            if (!this.IsSameKind(to_combine))
                throw new Exception("Trying to combine non likely DamageTemplates");
            foreach (Dices dices in to_combine.Damage.DicesList)
            {
                this.Damage.AddDice(dices);
            }
            this.Damage.Modifier += to_combine.Damage.Modifier;
        }

        public DamageTemplateType(string damageFormat, DamageTypeEnum damageType)
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
            set
            {
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
            set
            {
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
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region ICloneable 

        private void init_copy(DamageTemplateType to_copy)
        {
            Damage = (DiceRoll)to_copy.Damage.Clone();
            DamageType = to_copy.DamageType;
            SituationalDamageModifier = to_copy.SituationalDamageModifier;
        }

        protected DamageTemplateType(DamageTemplateType to_copy)
        {
            init_copy(to_copy);
        }

        public virtual object Clone()
        {
            return new DamageTemplateType(this);
        }

        #endregion
    }
}

