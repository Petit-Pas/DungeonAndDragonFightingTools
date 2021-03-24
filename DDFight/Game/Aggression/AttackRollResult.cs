using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Entities;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DDFight.Game.Aggression
{
    public class AttackRollResult : INotifyPropertyChanged, ICloneable
    {
        public AttackRollResult() {
            _hitModifiers.PropertyChanged += _hitModifiers_PropertyChanged;
        }

        private void init_copy(AttackRollResult to_copy)
        {
            this.AdvantageModifiers = to_copy.AdvantageModifiers.Clone() as SituationalAdvantageModifiers;
            this.AttackRoll = to_copy.AttackRoll;
            this.BaseRollModifier = to_copy.BaseRollModifier;
            this.Caster = to_copy.Caster;
            this.HitModifiers = to_copy.HitModifiers.Clone() as SituationalAttackRollModifiers;
            this.Target = to_copy.Target;
        }

        public AttackRollResult(AttackRollResult to_copy)
        {
            init_copy(to_copy);
        }

        public object Clone()
        {
            return new AttackRollResult(this);
        }

        public void Reset()
        {
            this.AttackRoll = 0;
            this.AdvantageModifiers.Reset();
            this.HitModifiers.Reset();
        }

        #region Properties

        public PlayableEntity Target
        {
            get => _target;
            set
            {
                _target = value;
                NotifyPropertyChanged();
            }
        }
        private PlayableEntity _target = null;

        public PlayableEntity Caster
        {
            get => _caster;
            set
            {
                _caster = value;
                NotifyPropertyChanged();
            }
        }
        private PlayableEntity _caster = null;

        public int AttackRoll
        {
            get => _attackRoll;
            set
            {
                if (_attackRoll != value)
                {
                    _attackRoll = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _attackRoll = 0;

        public int BaseRollModifier
        {
            get => _baseRollModifier;
            set
            {
                if (value != _baseRollModifier)
                {
                    _baseRollModifier = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _baseRollModifier = 0;

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

        public SituationalAttackRollModifiers HitModifiers
        {
            get => _hitModifiers;
            set
            {
                _hitModifiers.PropertyChanged -= _hitModifiers_PropertyChanged;
                _hitModifiers = value;
                _hitModifiers.PropertyChanged += _hitModifiers_PropertyChanged;
                NotifyPropertyChanged();
            }
        }
        private SituationalAttackRollModifiers _hitModifiers = new SituationalAttackRollModifiers();

        private void _hitModifiers_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged("Inner");
        }

        #endregion Properties

        #region Tools

        public bool IsRolled { get { return this.AttackRoll != 0; } }

        public bool Crits { get { return this.AttackRoll == 20; } }

        public bool CriticalFailure { get { return this.AttackRoll == 1; } }

        public int TotalAttackScore { get { return this.AttackRoll + this.BaseRollModifier + this.HitModifiers.HitModifier; } }

        public int TotalCAScore { get { return this.Target != null ? (int)this.Target.CA + this.HitModifiers.ACModifier : 0; } }

        public bool Hits { get { return this.Target != null ? this.TotalCAScore <= this.TotalAttackScore : false; } }

        public string Description 
        { 
            get 
            { 
                return this.TotalAttackScore.ToString() + "/" + this.TotalCAScore 
                    + (IsRolled && Target != null ? (" ==> " + 
                    (Hits ? 
                        (Crits ? 
                            "Critical Hit!" : 
                            "Hit!") : 
                        (CriticalFailure ? 
                            "Critical Failure!" :
                            "Missed!"
                        )
                    )) : "") ; 
            } 
        }

        #endregion Tools

        #region PropertyChanged
        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private bool inner = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName != "Description" && inner == false)
                // almost any change changes the Description
                NotifyPropertyChanged("Description");
            if (propertyName == "Inner")
            {
                // sends notifications for "read only" variables
                inner = true;
                NotifyPropertyChanged("Description");
                NotifyPropertyChanged("Crits");
                NotifyPropertyChanged("Hits");
                NotifyPropertyChanged("TotalAttackScore");
                NotifyPropertyChanged("IsRolled");
                NotifyPropertyChanged("CriticalFailure");
                NotifyPropertyChanged("TotalCAScore");
                inner = false;
            }
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion PropertyChanged

    }
}
