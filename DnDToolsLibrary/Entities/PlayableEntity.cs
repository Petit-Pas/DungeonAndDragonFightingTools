﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using DnDToolsLibrary.Fight.Events;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Memory;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Counters;
using DnDToolsLibrary.Status;
using BaseToolsLibrary;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Dice;

namespace DnDToolsLibrary.Entities
{
    public partial class PlayableEntity : INameable, ICopyAssignable, INotifyPropertyChanged, IDisposable, IEquivalentComparable<PlayableEntity>
    {
        private static readonly Lazy<IMediator> _mediator = new(DIContainer.GetImplementation<IMediator>);
        protected static IMediator Mediator => _mediator.Value;

        private static readonly Lazy<IStatusProvider> _statusProvider = new(DIContainer.GetImplementation<IStatusProvider>());
        protected static IStatusProvider StatusProvider => _statusProvider.Value;

        public PlayableEntity()
        {
        }

        #region Properties

        #region Properties_Name

        [XmlAttribute]
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("DisplayName");
                }
            }
        }
        private string _name = "Name";

        /// <summary>
        ///     Names are not unique when multiple instance of same entities are loaded, display name is there to fix it
        /// </summary>
        [XmlIgnore]
        public virtual string DisplayName
        {
            get
            {
                if (_displayName == "DisplayName")
                    return _name;
                return _displayName;
            }
            set
            {
                if (value != _displayName)
                {
                    _displayName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _displayName = "DisplayName";

        #endregion Properties_Name

        #region Properties_Stats

        [XmlIgnore] 
        public uint EffectiveCA => CA + (HasAShield ? ShieldValue : 0);

        /// <summary>
        ///     Armor Class
        /// WARNING: does not count the shield, only base CA
        /// </summary>
        [XmlAttribute]
        public uint CA
        {
            get => _cA;
            set
            {
                if (value != _cA)
                {
                    _cA = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private uint _cA = 10;

        [XmlAttribute]
        public bool HasAShield
        {
            get => _hasAShield;
            set
            {
                if (value != _hasAShield)
                {
                    _hasAShield = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _hasAShield = false;

        [XmlAttribute]
        public uint ShieldValue
        {
            get => _shieldValue;
            set
            {
                if (value != _shieldValue)
                {
                    _shieldValue = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private uint _shieldValue = 2;

        #region Properties_Stats_Hp

        /// <summary>
        ///     Max Hps that this character can have (Temporary Hps not taken into account)
        /// </summary>
        [XmlAttribute]
        public uint MaxHp
        {
            get => _maxHp;
            set
            {
                if (value != _maxHp)
                {
                    _maxHp = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private uint _maxHp = 0;

        [XmlAttribute]
        public int TempHp
        {
            get => _tempHP;
            set
            {
                if (value != _tempHP)
                {
                    _tempHP = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("HpString");
                }
            }
        }
        private int _tempHP = 0;

        /// <summary>
        ///     Current Hps (Temporary Hps not taken into account)
        /// </summary>
        [XmlAttribute]
        public int Hp
        {
            get => _hp;
            set
            {
                if (value != _hp)
                {
                    _hp = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("HpString");
                }
            }
        }
        private int _hp = 0;

        #endregion Properties_Stats_Hp

        #region Properties_Stats_SpellCastingAbility

        [XmlAttribute]
        public int SpellSave
        {
            get => _spellSave;
            set
            {
                _spellSave = value;
                NotifyPropertyChanged();
            }
        }
        private int _spellSave = 10;

        /// <summary>
        ///     The number added to rolls when executing a spell attack
        /// </summary>
        [XmlAttribute]
        public int SpellHitModifier
        {
            get => _spellHitModifier;
            set
            {
                if (_spellHitModifier != value)
                {
                    _spellHitModifier = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _spellHitModifier = 0;


        #endregion Properties_Stats_SpellCastingAbility

        /// <summary>
        ///     The characteristics of the character
        /// </summary>
        public CharacteristicList Characteristics
        {
            get => _characteristics;
            set
            {
                _characteristics = value;
                NotifyPropertyChanged();
            }
        }
        private CharacteristicList _characteristics = new CharacteristicList();

        public DamageTypeAffinityList DamageAffinities
        {
            get => _damageAffinities;
            set
            {
                _damageAffinities = value;
                NotifyPropertyChanged();
            }
        }
        private DamageTypeAffinityList _damageAffinities = new DamageTypeAffinityList();

        #endregion Properties_Stats

        #region Properties_Actions

        /// <summary>
        ///     Tells wether or not he action of the character was consumed
        /// </summary>
        [XmlIgnore]
        public bool HasAction
        {
            get => _hasAction;
            set
            {
                _hasAction = value;
                NotifyPropertyChanged();
            }
        }
        private bool _hasAction = true;

        /// <summary>
        ///     Tells wether or not he action of the character was consumed
        /// </summary>
        [XmlIgnore]
        public bool HasBonusAction
        {
            get => _hasBonusAction;
            set
            {
                _hasBonusAction = value;
                NotifyPropertyChanged();
            }
        }
        private bool _hasBonusAction = true;

        /// <summary>
        ///     Tells wether or not he action of the character was consumed
        /// </summary>
        [XmlIgnore]
        public bool HasReaction
        {
            get => _hasReaction;
            set
            {
                _hasReaction = value;
                NotifyPropertyChanged();
            }
        }
        private bool _hasReaction = true;

        /// <summary>
        ///     Additional information about the actions that this PlayableEntity can take
        /// </summary>
        public string ActionDescription
        {
            get => _actionDescription;
            set
            {
                _actionDescription = value;
                NotifyPropertyChanged();
            }
        }
        private string _actionDescription = "";

        /// <summary>
        ///     Additional informations about the special abilites that this PlayableEntity can use (in our out of fight)
        /// </summary>
        public string SpecialAbilities
        {
            get => _specialAbilities;
            set
            {
                _specialAbilities = value;
                NotifyPropertyChanged();
            }
        }
        private string _specialAbilities = "";

        public HitAttackTemplateList HitAttacks
        {
            get => _hitAttacks;
            set
            {
                _hitAttacks = value;
                NotifyPropertyChanged();
            }
        }
        private HitAttackTemplateList _hitAttacks = new HitAttackTemplateList();

        [XmlIgnore]
        public bool HasSpells
        {
            get => (Spells.Count != 0);
            set
            {
                NotifyPropertyChanged();
            }
        }

        public SpellList Spells
        {
            get => _spells;
            set
            {
                _spells = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("HasSpells");
            }
        }
        private SpellList _spells = new SpellList();

        public CounterList Counters
        {
            get => _counters;
            set
            {
                _counters = value;
                NotifyPropertyChanged();
            }
        }
        private CounterList _counters = new CounterList();

        #endregion Properties_Actions

        #region Properties_Turn

        /// <summary>
        ///     The initiative rolled for a given fight
        ///     Does not take dexterity into account
        /// </summary>
        [XmlIgnore]
        public uint InitiativeRoll
        {
            get => _initiativeRoll;
            set
            {
                if (_initiativeRoll != value)
                {
                    _initiativeRoll = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private uint _initiativeRoll = 0;

        /// <summary>
        ///     The turn for a given fight
        /// </summary>
        [XmlIgnore]
        public uint TurnOrder
        {
            get => _turnOrder;
            set
            {
                if (_turnOrder != value)
                {
                    _turnOrder = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private uint _turnOrder;

        #endregion Properties_Turn

        [XmlIgnore]
        public bool IsFocused
        {
            get => _isFocused;
            set
            {
                if (_isFocused != value)
                {
                    _isFocused = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool _isFocused;

        #endregion Properties

        #region Events

        #region Events_Turn

        public void InvokeTurnEnded(TurnEndedEventArgs args)
        {
            TurnEnded?.Invoke(this, args);
        }
        public void InvokeTurnStarted(TurnStartedEventArgs args)
        {
            TurnStarted?.Invoke(this, args);
        }

        // these events are dedicated to any UI that should catch these event => it should not be used to do any domain logic, only UI
        public event TurnStarted TurnStarted;
        public event TurnEnded TurnEnded;

        #endregion Events_Turn

        #endregion Events

        #region AffectingStatus

        public CustomVerboseStatusList CustomVerboseStatusList
        {
            get => _customVerboseStatusList;
            set
            {
                _customVerboseStatusList = value;
                NotifyPropertyChanged();
            }
        }
        private CustomVerboseStatusList _customVerboseStatusList = new CustomVerboseStatusList();

        public StatusReferenceList AffectingStatusList 
        {
            get => _affectingStatusList;
            set
            {
                _affectingStatusList = value;
                NotifyPropertyChanged();
            }
        }
        private StatusReferenceList _affectingStatusList = new StatusReferenceList();

        #endregion AffectingStatus

        #region Transformation

        [XmlIgnore]
        /// <summary>
        ///     Tells wether or not the character is currenlty transformed
        /// </summary>
        public bool IsTransformed
        {
            get => _isTransformed;
            set
            {
                _isTransformed = value;
                NotifyPropertyChanged();
            }
        }
        private bool _isTransformed = false;

        /// <summary>
        ///     Stored an instance of the actual character (non transformed)
        /// </summary>
        private PlayableEntity realOne; 

        public void Transform(PlayableEntity to_transform_into)
        {
            realOne = (PlayableEntity)this.Clone();
            to_transform_into.DisplayName = to_transform_into.Name + " (" + this.DisplayName + ")";
            CopyAssign(to_transform_into);
            IsTransformed = true;
            this.DisplayName = to_transform_into.DisplayName;
        }

        public void TransformBack()
        {
            CopyAssign(realOne);
            IsTransformed = false;
        }

        #endregion Transformation

        public int ReduceAllDamageBy 
        {
            get => _ReduceAllDamageBy;
            set
            {
                _ReduceAllDamageBy = value;
                NotifyPropertyChanged();
            }
        }
        private int _ReduceAllDamageBy = 0;

        [XmlIgnore]
        public string HpString
        {
            get
            {
                string result = Hp.ToString();

                if (TempHp != 0)
                {
                    result += "(+";
                    result += TempHp.ToString();
                    result += ")";
                }
                return result;
            }
            set
            {
                NotifyPropertyChanged();
            }
        }

        public SavingThrow GetSavingThrowTemplate(CharacteristicsEnum characteristic, int difficulty)
        {
            SavingThrow result = new SavingThrow();
            
            result.Characteristic = characteristic;
            result.Difficulty = difficulty;

            return result;
        }

        public int Initiative
        {
            get => (int)InitiativeRoll + Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity);
            set
            {
                ;
            }
        }

        /// <summary>
        ///     used to dump a character in log file (in case of crash, it at least gives information back)
        /// </summary>
        public void Dump()
        {
            Logger.Log("----------");
            Logger.Log(DisplayName + " has " + Hp + " hps");
            if (CustomVerboseStatusList.Count != 0)
            {
                Logger.Log("it has the statuses: ");
                foreach (CustomVerboseStatus status in CustomVerboseStatusList)
                {
                    Logger.Log("\t- " + status.Header);
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
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public bool IsEquivalentTo(PlayableEntity toCompare)
        {
            if (Name != toCompare.Name)
                return false;
            if (CA != toCompare.CA)
                return false;
            if (Hp != toCompare.Hp)
                return false;
            if (MaxHp != toCompare.MaxHp)
                return false;
            if (SpellSave != toCompare.SpellSave)
                return false;
            if (!Characteristics.IsEquivalentTo(toCompare.Characteristics))
                return false;
            if (!HitAttacks.IsEquivalentTo(toCompare.HitAttacks))
                return false;
            if (!CustomVerboseStatusList.IsEquivalentTo(toCompare.CustomVerboseStatusList))
                return false;
            if (TurnOrder != toCompare.TurnOrder)
                return false;
            if (InitiativeRoll != toCompare.InitiativeRoll)
                return false;
            if (ActionDescription != toCompare.ActionDescription)
                return false;
            if (SpecialAbilities != toCompare.SpecialAbilities)
                return false;
            if (!Counters.IsEquivalentTo(toCompare.Counters))
                return false;
            if (!Spells.IsEquivalentTo(toCompare.Spells))
                return false;
            if (TempHp != toCompare.TempHp)
                return false;
            if (SpellHitModifier != toCompare.SpellHitModifier)
                return false;
            if (HasAShield != toCompare.HasAShield)
                return false;
            if (ShieldValue != toCompare.ShieldValue)
                return false;
            if (ReduceAllDamageBy != toCompare.ReduceAllDamageBy)
                return false;
            return true;
        }

        #region IClonable

        /// <summary>
        ///     this method is required to completely initialize an instance of this by copying another object
        /// </summary>
        private void init_copy(PlayableEntity to_copy)
        {
            Name = (string)to_copy.Name.Clone();
            CA = to_copy.CA;
            Hp = to_copy.Hp;
            MaxHp = to_copy.MaxHp;
            SpellSave = to_copy.SpellSave;
            Characteristics = (CharacteristicList)to_copy.Characteristics.Clone();
            DamageAffinities = (DamageTypeAffinityList)to_copy.DamageAffinities.Clone();
            HitAttacks = (HitAttackTemplateList)to_copy.HitAttacks.Clone();
            foreach (HitAttackTemplate atk in HitAttacks)
            {
                atk.Owner = this;
            }
            CustomVerboseStatusList = to_copy.CustomVerboseStatusList.Clone() as CustomVerboseStatusList;
            TurnOrder = to_copy.TurnOrder;
            InitiativeRoll = to_copy.InitiativeRoll;
            ActionDescription = (string)to_copy.ActionDescription.Clone();
            SpecialAbilities = (string)to_copy.SpecialAbilities.Clone();
            Counters = (CounterList)to_copy.Counters.Clone();
            Spells = (SpellList)to_copy.Spells.Clone();
            TempHp = to_copy.TempHp;
            SpellHitModifier = to_copy.SpellHitModifier;
            AffectingStatusList = to_copy.AffectingStatusList.Clone() as StatusReferenceList;
            HasAShield = to_copy.HasAShield;
            ShieldValue = to_copy.ShieldValue;
            ReduceAllDamageBy = to_copy.ReduceAllDamageBy;
        }

        /// <summary>
        ///     copy contructor, required for the Clone method to work properly with derived classes
        /// </summary>
        /// <param name=""></param>
        protected PlayableEntity(PlayableEntity to_copy)
        {
            init_copy(to_copy);
        }

        /// <summary>
        ///     Method to clone a character (Deep Copy)
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            return new PlayableEntity(this);
        }

        public virtual void CopyAssign(object to_copy)
        {
            this.DisposeAllDisposableMembers();
            init_copy(to_copy as PlayableEntity);
        }

        public void Dispose()
        {
            this.DisposeAllDisposableMembers();
        }

        ~PlayableEntity()
        {
        }

        #endregion
    }
}
