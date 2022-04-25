using BaseToolsLibrary.DependencyInjection;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Attacks.Spells
{
    public class NewAttackSpellResult : ICloneable
    {
        public NewAttackSpellResult()
        {
        }

        #region Properties

        public AttackRollResult RollResult
        {
            get => _rollResult;
            set
            {
                _rollResult = value;
                NotifyPropertyChanged();
            }
        }
        private AttackRollResult _rollResult = new AttackRollResult();

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        private string _name = "";

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                NotifyPropertyChanged();
            }
        }
        private int _level = 0;

        public DamageResultList HitDamage
        {
            get => _hitDamage;
            set
            {
                _hitDamage = value;
                NotifyPropertyChanged();
            }
        }
        private DamageResultList _hitDamage = new DamageResultList();

        public bool AutomaticalyHits
        {
            get => _automaticalyHits;
            set
            {
                if (_automaticalyHits != value)
                {
                    _automaticalyHits = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _automaticalyHits = false;

        // not sure this is useful
        public int ToHitBonus
        {
            get => _toHitBonus;
            set
            {
                _toHitBonus = value;
                NotifyPropertyChanged();
            }
        }
        private int _toHitBonus = 0;

        public OnHitStatusList AppliedStatusList
        {
            get => _appliedStatusList;
            set
            {
                _appliedStatusList = value;
                NotifyPropertyChanged();
            }
        }
        private OnHitStatusList _appliedStatusList = new OnHitStatusList();

        private static IFighterProvider fighterProvider = DIContainer.GetImplementation<IFighterProvider>();

        [XmlIgnore]
        public PlayableEntity Caster
        {
            get
            {
                return fighterProvider.GetFighterByDisplayName(CasterName);
            }
            set
            {
                if (this.RollResult != null)
                {
                    this.RollResult.Caster = value;
                }

                if (value != null)
                    CasterName = value.DisplayName;
                else
                    CasterName = null;

                NotifyPropertyChanged();
            }
        }
        [XmlAttribute]
        public string CasterName
        {
            get
            {
                return _casterName;
            }
            set
            {
                _casterName = value;
                NotifyPropertyChanged();
            }
        }
        private string _casterName = null;

        [XmlIgnore]
        public PlayableEntity Target
        {
            get
            {
                return fighterProvider.GetFighterByDisplayName(TargetName);
            }
            set
            {
                if (this.RollResult != null)
                    this.RollResult.Target = value;
                if (this.HitDamage != null)
                    HitDamage.RefreshDamageAffinityModifier(value);

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
            get
            {
                return _targetName;
            }
            set
            {
                _targetName = value;
                NotifyPropertyChanged();
            }
        }
        private string _targetName = null;

        #endregion Properties


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


        private void init_copy(NewAttackSpellResult to_copy)
        {
            this.AppliedStatusList = to_copy.AppliedStatusList.Clone() as OnHitStatusList;
            this.AutomaticalyHits = to_copy.AutomaticalyHits;
            this.CasterName = to_copy.CasterName;
            this.HitDamage = to_copy.HitDamage.Clone() as DamageResultList;
            this.Level = to_copy.Level;
            this.Name = to_copy.Name;
            this.RollResult = to_copy.RollResult.Clone() as AttackRollResult;
            this.TargetName = to_copy.TargetName;
            this.ToHitBonus = to_copy.ToHitBonus;
        }

        public NewAttackSpellResult(NewAttackSpellResult to_copy)
        {
            init_copy(to_copy);
        }

        public object Clone()
        {
            return new NewAttackSpellResult(this);
        }
    }
}
