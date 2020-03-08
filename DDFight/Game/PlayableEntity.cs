using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Characteristics;
using DDFight.Game.DamageAffinity;
using DDFight.Game.Fight.FightEvents;
using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DDFight.Game
{
    public class PlayableEntity : ICloneable, INotifyPropertyChanged, ICopyAssignable /*, INotifyPropertyChangedSub*/
    {
        public PlayableEntity()
        {
        }

        [XmlIgnore]
        public bool Validated = false;

        #region Properties

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
            get => _displayName;
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
                }
            }
        }
        private int _hp = 0;

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
                foreach (CustomVerboseStatus status in CustomVerboseStatusList.List)
                {
                    Logger.Log("\t- " + status.Header);
                }
            }
        }


        /// <summary>
        ///     The characteristics of the character
        /// </summary>
        public CharacteristicsDataContext Characteristics
        {
            get => _characteristics;
            set
            {
                _characteristics = value;
                NotifyPropertyChanged();
            }
        }
        private CharacteristicsDataContext _characteristics = new CharacteristicsDataContext();

        public DamageTypeAffinitiesDataContext DamageAffinities
        {
            get => _damageAffinities;
            set
            {
                _damageAffinities = value;
                NotifyPropertyChanged();
            }
        }
        private DamageTypeAffinitiesDataContext _damageAffinities = new DamageTypeAffinitiesDataContext();

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
        public string SpecialCapacities
        {
            get => _specialCapacities;
            set
            {
                _specialCapacities = value;
                NotifyPropertyChanged();
            }
        }
        private string _specialCapacities = "";

        public ObservableCollection<HitAttackTemplate> HitAttacks
        {
            get => _hitAttacks;
            set
            {
                _hitAttacks = value;
                NotifyPropertyChanged();
            }
        }
        private ObservableCollection<HitAttackTemplate> _hitAttacks = new ObservableCollection<HitAttackTemplate>();

        #endregion Characteristics

        #region Status

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
                Console.WriteLine("change is transformed in " + GetType().ToString());
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
            this.CopyAssign(to_transform_into);
            IsTransformed = true;
            this.DisplayName = to_transform_into.DisplayName;
        }

        public void TransformBack()
        {
            this.CopyAssign(realOne);
            IsTransformed = false;
        }

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
                _isFocused = value;
                NotifyPropertyChanged();
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

        public void StartNewTurn()
        {
            HasAction = true;
            HasReaction = true;
            HasBonusAction = true;
            OnStartNewTurn(new StartNewTurnEventArgs()
            {
                Character = this,
                CharacterIndex = Global.Context.FightContext.FightersList.Fighters.IndexOf(this),
            });
        }

        public void OnStartNewTurn(StartNewTurnEventArgs args)
        {
            Global.Context.UserLogs.Blocks.Add(new Paragraph());
            Paragraph tmp = (Paragraph) Global.Context.UserLogs.Blocks.LastBlock;
            tmp.Inlines.Add(Extensions.BuildRun(args.Character.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            tmp.Inlines.Add(Extensions.BuildRun(" starts its turn!\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            if (NewTurnStarted != null)
            {
                NewTurnStarted(this, args);
            }
        }

        public event StartNewTurnEventHandler NewTurnStarted;

        public void EndTurn()
        {
            OnEndTurn(new EndTurnEventArgs()
            {
                Character = this,
                CharacterIndex = Global.Context.FightContext.FightersList.Fighters.IndexOf(this),
            });
        }

        public void OnEndTurn(EndTurnEventArgs args)
        {
            if (TurnEnded != null)
            {
                TurnEnded(this, args);
            }
        }

        public event EndTurnEventHandler TurnEnded;

        #endregion Turns

        #region HpManagement

        public void Heal(DiceRoll to_roll)
        {
            int amount = to_roll.Roll();

            if (Hp + amount >= MaxHp)
                amount = (int)MaxHp - Hp;

            Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(Extensions.BuildRun(this.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(Extensions.BuildRun(" regains ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(Extensions.BuildRun(amount.ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(Extensions.BuildRun(" Hps.\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));

            Hp += amount;
        }

        /// <summary>
        ///     method called when a hit attack lands to compute the damage received
        /// </summary>
        /// <param name="damages"></param>
        public void TakeHitDamage(List<DamageTemplate> damages)
        {
            int i = 1;
            int total = 0;

            Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

            paragraph.Inlines.Add(Extensions.BuildRun(this.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(Extensions.BuildRun(" takes ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));


            foreach (DamageTemplate dmg in damages)
            {
                int damage_value = 0;
                DamageAffinityEnum affinity = this.DamageAffinities.GetAffinity(dmg.DamageType).Affinity;

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
                if (i == damages.Count && i != 1)
                    paragraph.Inlines.Add(Extensions.BuildRun("and ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun(damage_value.ToString() + " " +  dmg.DamageType.ToString(), (Brush)BrushToDamageTypeEnumConverter.StaticConvert(dmg.DamageType), 15, FontWeights.Bold));
                paragraph.Inlines.Add(Extensions.BuildRun(i == damages.Count ? " damage" : " damage, ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                total += damage_value;
                i += 1;
            }
            paragraph.Inlines.Add(Extensions.BuildRun("\nTotal: ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            paragraph.Inlines.Add(Extensions.BuildRun(total.ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            paragraph.Inlines.Add(Extensions.BuildRun(" damage\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            LooseHp(total);
        }

        public void LooseHp(int amount)
        {
            Hp -= amount;
            if (IsFocused)
            {
                ConcentrationCheckWindow window = new ConcentrationCheckWindow();
                window.DataContext = this;
                window.ShowDialog();
                if (window.Success == false)
                {
                    this.IsFocused = false;
                }
            }
        }

        public void GetAttacked(HitAttackResult result, PlayableEntity attacker)
        {
            Paragraph tmp = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;

            tmp.Inlines.Add(Extensions.BuildRun(attacker.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            tmp.Inlines.Add(Extensions.BuildRun(" attacks ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            tmp.Inlines.Add(Extensions.BuildRun(this.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            tmp.Inlines.Add(Extensions.BuildRun(". ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            tmp.Inlines.Add(Extensions.BuildRun((result.HitRoll + result.HitBonus + result.SituationalHitAttackModifiers.HitModifier).ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            tmp.Inlines.Add(Extensions.BuildRun("/", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            tmp.Inlines.Add(Extensions.BuildRun((result.SituationalHitAttackModifiers.ACModifier + this.CA).ToString(), (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            tmp.Inlines.Add(Extensions.BuildRun(" ==> ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));

            // Critical Miss
            if (result.HitRoll == 1)
            {
                tmp.Inlines.Add(Extensions.BuildRun(" Critical Miss!\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            }

            // Critical Hit
            else if (result.HitRoll >= 20)
            {
                tmp.Inlines.Add(Extensions.BuildRun(" Critical Hit!\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                TakeHitDamage(result.DamageList);
            }

            // if the character gets hit with a normal hit
            else if (result.HitRoll + result.HitBonus + result.SituationalHitAttackModifiers.HitModifier >= result.SituationalHitAttackModifiers.ACModifier + this.CA)
            {
                tmp.Inlines.Add(Extensions.BuildRun(" Hit!\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
                TakeHitDamage(result.DamageList);
            }
            // If the character can avoid / block the attack
            else
            {
                tmp.Inlines.Add(Extensions.BuildRun(" Missed!\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            }
        }

        #endregion HpManagement

        #endregion Fight

        public virtual bool OpenEditWindow()
        {
            throw new NotImplementedException();
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
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
            Characteristics = (CharacteristicsDataContext)to_copy.Characteristics.Clone();
            DamageAffinities = (DamageTypeAffinitiesDataContext)to_copy.DamageAffinities.Clone();
            HitAttacks = to_copy.HitAttacks.Clone();
            foreach (HitAttackTemplate atk in HitAttacks)
            {
                atk.Owner = this;
            }
            CustomVerboseStatusList = (CustomVerboseStatusList)to_copy.CustomVerboseStatusList.Clone();
            DisplayName = (string)to_copy.DisplayName.Clone();
            TurnOrder = to_copy.TurnOrder;
            InitiativeRoll = to_copy.InitiativeRoll;
            ActionDescription = (string)to_copy.ActionDescription.Clone();
            SpecialCapacities = (string)to_copy.SpecialCapacities.Clone();
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

        /// <summary>
        ///     reinitialize this object by copying the received one
        /// </summary>
        /// <param name="_to_copy"></param>
        public virtual void CopyAssign(object _to_copy)
        {
            PlayableEntity to_copy = (PlayableEntity)_to_copy;
            init_copy(to_copy);
        }
        #endregion
    }
}
