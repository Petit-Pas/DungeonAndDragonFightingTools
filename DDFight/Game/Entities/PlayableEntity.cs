using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Characteristics;
using DDFight.Game.DamageAffinity;
using DDFight.Game.Fight.FightEvents;
using DDFight.Tools;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Serialization;
using System.Windows.Media;
using DDFight.Game.Aggression;
using DDFight.Converters;
using DDFight.Game.Dices;
using DDFight.Game.Status;
using DDFight.Windows.FightWindows;
using DDFight.Game.Counters;
using DDFight.Game.Aggression.Spells;
using DDFight.Game.Entities.Display;

namespace DDFight.Game.Entities
{
    public class PlayableEntity : INameable, IWindowEditable, ICloneable, INotifyPropertyChanged
    {
        public PlayableEntity()
        {
        }

        [XmlIgnore]
        public bool Validated = false;

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

        public event StartNewTurnEventHandler NewTurnStarted;

        /// <summary>
        ///     Functions to ask the PlayableEntity to setup eveyrthing for a new turn
        ///     Will raise the NewTurnStarted event
        /// </summary>
        public void StartNewTurn()
        {
            HasAction = true;
            HasReaction = true;
            HasBonusAction = true;
            OnStartNewTurn(new StartNewTurnEventArgs()
            {
                Character = this,
                CharacterIndex = Global.Context.FightContext.FightersList.Elements.IndexOf(this),
            });
        }

        public void OnStartNewTurn(StartNewTurnEventArgs args)
        {
            Global.Context.UserLogs.Blocks.Add(new Paragraph());
            Paragraph tmp = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;
            tmp.Inlines.Add(Extensions.BuildRun(args.Character.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            tmp.Inlines.Add(Extensions.BuildRun(" starts its turn!\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            NewTurnStarted?.Invoke(this, args);
        }

        public event EndTurnEventHandler TurnEnded;

        /// <summary>
        ///     Function called to end the Turn of the PlayableEntity
        ///     Will raise the OnEndTurn event
        /// </summary>
        public void EndTurn()
        {
            OnEndTurn(new TurnEndedEventArgs()
            {
                Character = this,
                CharacterIndex = Global.Context.FightContext.FightersList.Elements.IndexOf(this),
            });
        }

        public void OnEndTurn(TurnEndedEventArgs args)
        {
            TurnEnded?.Invoke(this, args);
        }

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
            InitCopy(to_transform_into);
            IsTransformed = true;
            this.DisplayName = to_transform_into.DisplayName;
        }

        public void TransformBack()
        {
            InitCopy(realOne);
            IsTransformed = false;
        }

        #endregion Transformation

        #region HpManagement

        public void HealTempHP(DiceRoll to_roll)
        {
            to_roll.Roll();
            int amount = to_roll.LastResult;
            Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(Extensions.BuildRun(this.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));

            if (TempHp < amount)
            {
                paragraph.Inlines.Add(Extensions.BuildRun(" now has ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(amount.ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(" temporary Hps.\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                TempHp = amount;
            }
            else
            {
                paragraph.Inlines.Add(Extensions.BuildRun(" keeps his ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(TempHp.ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(" temporary Hps.\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            }
        }

        public void Heal(DiceRoll to_roll)
        {
            to_roll.Roll();
            int amount = to_roll.LastResult;

            if (Hp + amount >= MaxHp)
                amount = (int)MaxHp - Hp;

            Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(Extensions.BuildRun(this.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(Extensions.BuildRun(" regains ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(Extensions.BuildRun(amount.ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(Extensions.BuildRun(" Hps.\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));

            Hp += amount;
        }

        public void LooseHp(int amount)
        {
            Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(Extensions.BuildRun("\nTotal: ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(Extensions.BuildRun(amount.ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(Extensions.BuildRun(" damage (", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(Extensions.BuildRun(HpString, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
            paragraph.Inlines.Add(Extensions.BuildRun(" ==> ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));

            // handles temp HP
            if (TempHp != 0)
            {
                if (TempHp - amount < 0)
                {
                    amount -= TempHp;
                    TempHp = 0;
                }
                else
                {
                    TempHp -= amount;
                    amount = 0;
                }
            }

            // removes remaining HPs
            Hp -= amount;

            paragraph.Inlines.Add(Extensions.BuildRun(HpString, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
            paragraph.Inlines.Add(Extensions.BuildRun(").\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            if (amount == 0)
                return;

            // handles 0 HP
            if (Hp <= 0)
            {
                Hp = 0;
                if (IsFocused == true)
                {
                    IsFocused = false;
                    paragraph.Inlines.Add(Extensions.BuildRun(this.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
                    paragraph.Inlines.Add(Extensions.BuildRun(": Has reached", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                    paragraph.Inlines.Add(Extensions.BuildRun(" 0 Hps", (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
                    paragraph.Inlines.Add(Extensions.BuildRun(", lost Focus.", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                }
            }

            // handles Concentration Check if required
            if (IsFocused)
            {
                ConcentrationCheckWindow window = new ConcentrationCheckWindow
                {
                    DataContext = this
                };
                window.ShowCentered();
                if (window.Success == false)
                {
                    this.IsFocused = false;
                }
            }
        }

        #endregion HpManagement

        #region HitManagement

        /// <summary>
        ///     method called when a hit attack lands to compute the damage received
        /// </summary>
        /// <param name="damages"></param>
        // TODO Might need to rename this
        public void TakeHitDamage(DamageResultList damages)
        {
            int i = 1;
            int total = 0;

            Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(Extensions.BuildRun(this.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(Extensions.BuildRun(" takes ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));


            foreach (DamageResult dmg in damages.Elements)
            {
                int damage_value = 0;
                if (dmg.Damage.LastResult > 0)
                {
                    DamageAffinityEnum affinity = this.DamageAffinities.GetAffinity(dmg.DamageType).Affinity;

                    // damage resistance / weakness
                    switch (affinity)
                    {
                        case DamageAffinityEnum.Neutral:
                            damage_value = dmg.Damage.LastResult;
                            break;
                        case DamageAffinityEnum.Resistant:
                            damage_value = dmg.Damage.LastResult / 2;
                            break;
                        case DamageAffinityEnum.Immune:
                            damage_value = 0;
                            break;
                        case DamageAffinityEnum.Weak:
                            damage_value = dmg.Damage.LastResult * 2;
                            break;
                    }
                    if (dmg.LastSavingWasSuccesfull)
                    {
                        // Situational damage modifiers (such as a saving throw that could divide damge by 2)
                        switch (dmg.SituationalDamageModifier)
                        {
                            case DamageModifierEnum.Halved:
                                damage_value /= 2;
                                break;
                            case DamageModifierEnum.Canceled:
                                damage_value = 0;
                                break;
                            default:
                                break;
                        }
                        dmg.LastSavingWasSuccesfull = false;
                    }
                }

                if (i == damages.Elements.Count && i != 1)
                    paragraph.Inlines.Add(Extensions.BuildRun("and ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(damage_value.ToString() + " " +  dmg.DamageType.ToString(), (Brush)DamageTypeEnumToBrushConverter.StaticConvert(dmg.DamageType), 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(i == damages.Elements.Count ? " damage" : " damage, ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                total += damage_value;
                i += 1;
            }
            LooseHp(total);
        }

        /// <summary>
        ///     Will evaluate if the HitAttack hits and deal damage if so.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="attacker"></param>
        /// <returns></returns>
        public bool GetAttacked(HitAttackResult result, PlayableEntity attacker)
        {
            Paragraph tmp = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

            tmp.Inlines.Add(Extensions.BuildRun(attacker.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            tmp.Inlines.Add(Extensions.BuildRun(" attacks ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            tmp.Inlines.Add(Extensions.BuildRun(this.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            tmp.Inlines.Add(Extensions.BuildRun(". ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            tmp.Inlines.Add(Extensions.BuildRun(result.RollResult.Description, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            tmp.Inlines.Add(Extensions.BuildRun("\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));

            if (result.RollResult.Hits)
            {
                TakeHitDamage(result.DamageList);
                foreach (OnHitStatus onHitStatus in result.OnHitStatuses.Elements)
                {
                    onHitStatus.CheckIfApply(result.Owner, result.Target);
                }
                return true;
            }
            else
                return false;
        }

        #endregion HitManagement

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
        internal void Dump()
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

        public virtual bool OpenEditWindow()
        {
            PlayableEntityEditWindow window = new PlayableEntityEditWindow();
            PlayableEntity temporary = (PlayableEntity)this.Clone();
            window.DataContext = temporary;
            window.ShowCentered();

            if (temporary.Validated == true)
            {
                InitCopy(temporary);
                return true;
            }
            return false;
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
        protected virtual void InitCopy(PlayableEntity to_copy)
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
           InitCopy(to_copy);
        }

        /// <summary>
        ///     Method to clone a character (Deep Copy)
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            return new PlayableEntity(this);
        }

        #endregion
    }
}
