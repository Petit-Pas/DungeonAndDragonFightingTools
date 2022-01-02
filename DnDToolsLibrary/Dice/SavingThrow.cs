using BaseToolsLibrary;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Dice
{ 
    public class SavingThrow : INotifyPropertyChanged, ICloneable, IMediatorCommandResponse, IEquivalentComparable<SavingThrow>
    {
        #region Properties

        public SavingThrow()
        {
        }

        public SavingThrow(CharacteristicsEnum characteristic, int difficulty, string targetName)
        {
            Characteristic = characteristic;
            Difficulty = difficulty;
            TargetName = targetName;
        }

        private static IFigtherProvider fighterProvider = DIContainer.GetImplementation<IFigtherProvider>();

        [XmlIgnore]
        public bool IsSuccesful
        {
            get 
            {
                return (Result >= Difficulty);
            }
        }

        [XmlIgnore]
        public bool IsFailed
        {
            get => !IsSuccesful;
        }

        [XmlIgnore]
        public int Result
        {
            get => Modifier + SavingRoll + Target.Characteristics.GetSavingModifier(Characteristic);
        }

        [XmlAttribute]
        public CharacteristicsEnum Characteristic
        {
            get => _characteristic;
            set
            {
                _characteristic = value;
                NotifyPropertyChanged();
            }
        }
        private CharacteristicsEnum _characteristic = CharacteristicsEnum.Dexterity;

        [XmlAttribute]
        public int Difficulty
        {
            get => _difficulty;
            set
            {
                _difficulty = value;
                NotifyPropertyChanged();
            }
        }
        private int _difficulty = 10;

        [XmlAttribute]
        public SituationalAdvantageModifiers AdvantageModifiers
        {
            get => _advantageModifiers;
            set
            {
                _advantageModifiers = value;
                NotifyPropertyChanged();
            }
        }
        private SituationalAdvantageModifiers _advantageModifiers = new SituationalAdvantageModifiers();

        /// <summary>
        ///     An arbitrary modifier for the Saving Throw
        /// </summary>
        [XmlAttribute]
        public int Modifier
        {
            get => _modifier;
            set
            {
                _modifier = value;
                NotifyPropertyChanged();
            }
        }
        private int _modifier = 0;

        [XmlAttribute]
        public int SavingRoll
        {
            get => _savingRoll;
            set
            {
                _savingRoll = value;
                NotifyPropertyChanged();
            }
        }
        private int _savingRoll = 0;

        [XmlIgnore]
        public PlayableEntity Target
        {
            get
            {
                return fighterProvider.GetFighterByDisplayName(TargetName);
            }
            set
            {
                if (value != null)
                    TargetName = value.DisplayName;
                else
                    TargetName = null;
                NotifyPropertyChanged();
            }
        }
        [XmlAttribute]
        public string TargetName
        {
            get => _targetName;
            set
            {
                _targetName = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(this.Target));
            }
        }
        private string _targetName = "";
        #endregion Properties

        #region ICloneable

        public virtual object Clone()
        {
            return new SavingThrow(this);
        }

        protected void init_copy(SavingThrow to_copy)
        {
            this.AdvantageModifiers = to_copy.AdvantageModifiers.Clone() as SituationalAdvantageModifiers;
            this.Characteristic = to_copy.Characteristic;
            this.Difficulty = to_copy.Difficulty;
            this.Modifier = to_copy.Modifier;
            this.SavingRoll = to_copy.SavingRoll;
            this.TargetName = to_copy.TargetName;
        }

        public bool IsEquivalentTo(SavingThrow toCompare)
        {
            if (this.SavingRoll != toCompare.SavingRoll)
                return false;
            if (!this.AdvantageModifiers.IsEquivalentTo(toCompare.AdvantageModifiers))
                return false;
            if (this.Characteristic != toCompare.Characteristic)
                return false;
            if (this.Difficulty != toCompare.Difficulty)
                return false;
            if (this.Modifier != toCompare.Modifier)
                return false;
            if (this.TargetName != toCompare.TargetName)
                return false;
            return true;
        }

        public SavingThrow(SavingThrow to_copy)
        {
            init_copy(to_copy);
        }

        public void CopyAssign(SavingThrow to_copy)
        {
            init_copy(to_copy);
        }

        #endregion ICloneable
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
