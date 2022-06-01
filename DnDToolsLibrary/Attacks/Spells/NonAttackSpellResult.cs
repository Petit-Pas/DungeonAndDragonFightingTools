using BaseToolsLibrary.DependencyInjection;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace DnDToolsLibrary.Attacks.Spells
{
    public class NonAttackSpellResult : INotifyPropertyChanged, ICloneable
    {
        public NonAttackSpellResult()
        {
        }

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

        private static IFightersProvider _fightersProvider = DIContainer.GetImplementation<IFightersProvider>();

        [XmlIgnore]
        public PlayableEntity Caster
        {
            get
            {
                return _fightersProvider.GetFighterByDisplayName(CasterName);
            }
            set
            {
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
                return _fightersProvider.GetFighterByDisplayName(TargetName);
            }
            set
            {
                if (this.HitDamage != null)
                    HitDamage.RefreshDamageAffinityModifier(value);
                if (Saving != null)
                    Saving.Target = value;
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

        [XmlAttribute]
        public bool HasSavingThrow
        {
            get => _hasSavingThrow;
            set
            {
                _hasSavingThrow = value;
                NotifyPropertyChanged();
            }
        }
        private bool _hasSavingThrow = false;

        

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

        public SavingThrow Saving 
        {
            get => _saving;
            set
            {
                if (value != null)
                {
                    if (_saving != null)
                    {
                        _saving.PropertyChanged -= refreshLastSavingSuccesfulOfHitDamage;
                    }
                    value.PropertyChanged += refreshLastSavingSuccesfulOfHitDamage;
                }
                _saving = value;
                NotifyPropertyChanged();
            }
        }
        private SavingThrow _saving = null;

        private void refreshLastSavingSuccesfulOfHitDamage(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Saving.SavingRoll))
            {
                foreach (DamageResult result in HitDamage)
                {
                    result.LastSavingWasSuccesfull = Saving.IsSuccesful;
                }
            }
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
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private void init_copy(NonAttackSpellResult to_copy)
        {
            this.AppliedStatusList = to_copy.AppliedStatusList.Clone() as OnHitStatusList;
            this.CasterName = to_copy.CasterName;
            this.HasSavingThrow = to_copy.HasSavingThrow;
            this.HitDamage = to_copy.HitDamage.Clone() as DamageResultList;
            this.Level = to_copy.Level;
            this.Name = to_copy.Name;
            this.TargetName = to_copy.TargetName;
            this.Saving = to_copy.Saving?.Clone() as SavingThrow;
        }

        public NonAttackSpellResult(NonAttackSpellResult to_copy)
        {
            init_copy(to_copy);
        }

        public object Clone()
        {
            return new NonAttackSpellResult(this);
        }
    }
}
