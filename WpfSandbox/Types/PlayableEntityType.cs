using DDFight.Tools;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DDFight.Game.Characteristics;
using DDFight.Game.DamageAffinity;
using DDFight.Game.Status;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace WpfSandbox.Types
{
    public class PlayableEntityType : INameable, IWindowEditable, ICloneable, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public bool OpenEditWindow()
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        [XmlAttribute]
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                }
            }
        }
        private string _name = "Name";

        /// <summary>
        ///     Only used for monsters when they are duplicates in fight, Name will be the same, while DisplayName will be unique
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
                }
            }
        }
        private string _displayName = "DisplayName";

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
                }
            }
        }
        private uint _cA = 10;


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

        /// <summary>
        ///     used to dump a character in log file (in case of crash, it at least gives information back)
        /// </summary>
        internal void Dump()
        {
            Logger.Log("----------");
            Logger.Log(DisplayName + " has " + Hp + " hps");
            if (CustomVerboseStatusList.List.Count != 0)
            {
                Logger.Log("it has the statuses: ");
                foreach (CustomVerboseStatusType status in CustomVerboseStatusList.List)
                {
                    Logger.Log("\t- " + status.Header);
                }
            }
        }


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

        [XmlAttribute]
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

        [XmlAttribute]
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

        [XmlArrayItem("HitAttackTemplate", typeof(HitAttackTemplateType))]
        public ObservableCollection<HitAttackTemplateType> HitAttacks
        {
            get => _hitAttacks;
            set
            {
                _hitAttacks = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<HitAttackTemplateType> _hitAttacks = new ObservableCollection<HitAttackTemplateType>();

        [XmlIgnore]
        public bool HasSpells
        {
            get => (Spells.Elements.Count != 0);
            set
            {
                NotifyPropertyChanged();
            }
        }

        public SpellListType Spells
        {
            get => _spells;
            set
            {
                _spells = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("HasSpells");
            }
        }
        private SpellListType _spells = new SpellListType();

        [XmlArrayItem("Counter", typeof(CounterType))]
        public ObservableCollection<CounterType> Counters
        {
            get => _counters;
            set
            {
                _counters = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<CounterType> _counters = new ObservableCollection<CounterType>();

        #region Status

        public CustomVerboseStatusListType CustomVerboseStatusList
        {
            get => _customVerboseStatusList;
            set
            {
                _customVerboseStatusList = value;
                NotifyPropertyChanged();
            }
        }
        private CustomVerboseStatusListType _customVerboseStatusList = new CustomVerboseStatusListType();

        #endregion Status

        #region Fight

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


        #endregion Transformation

        #region Actions

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

        #endregion Actions

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

        #region Turns

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
    }

    #endregion
    #endregion
}
