using System;
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

namespace DnDToolsLibrary.Entities
{
    public partial class PlayableEntity : INameable, ICopyAssignable, INotifyPropertyChanged, IDisposable
    {
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

        /// <summary>
        ///     Armor Class
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
            get => (Spells.Elements.Count != 0);
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
        public void InvokeTurnStarted(StartNewTurnEventArgs args)
        {
            NewTurnStarted?.Invoke(this, args);
        }

        public event StartNewTurnEventHandler NewTurnStarted;
        public event EndTurnEventHandler TurnEnded;

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

        /// <summary>
        ///     used to dump a character in log file (in case of crash, it at least gives information back)
        /// </summary>
        public void Dump()
        {
            Logger.Log("----------");
            Logger.Log(DisplayName + " has " + Hp + " hps");
            if (CustomVerboseStatusList.Elements.Count != 0)
            {
                Logger.Log("it has the statuses: ");
                foreach (CustomVerboseStatus status in CustomVerboseStatusList.Elements)
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
            foreach (HitAttackTemplate atk in HitAttacks.Elements)
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
