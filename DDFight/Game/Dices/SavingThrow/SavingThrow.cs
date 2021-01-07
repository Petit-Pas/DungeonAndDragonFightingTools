using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Characteristics;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DDFight.Game.Dices.SavingThrow
{
    public class SavingThrow : INotifyPropertyChanged, ICloneable
    {
        #region Properties

        public SavingThrow()
        {
        }

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

        public PlayableEntity Target
        {
            get => _target;
            set
            {
                _target = value;
                NotifyPropertyChanged();
            }
        }
        private PlayableEntity _target;

        #endregion Properties

        #region ICloneable

        public virtual object Clone()
        {
            return new SavingThrow(this);
        }

        protected void init_copy(SavingThrow to_copy)
        {
            _characteristic = to_copy._characteristic;
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
