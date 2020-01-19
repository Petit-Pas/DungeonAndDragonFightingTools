﻿using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Characteristics;
using DDFight.Game.DamageAffinity;
using DDFight.Game.Fight.FightEvents;
using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDFight.Game
{
    public class PlayableEntity : ICloneable, INotifyPropertyChanged, ICopyAssignable /*, INotifyPropertyChangedSub*/
    {
        public PlayableEntity() 
        {
        }

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
        [XmlAttribute]
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

        #endregion

        #region Fight

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

        #endregion

        #region INotifyPropertyChangedSub
        /*
        /// <summary>
        ///     Subscribes the given event handler to this + all nested classes' PropertyChanged events
        /// </summary>
        /// <param name="handler"></param>
        public void PropertyChangedSubscript(PropertyChangedEventHandler handler)
        {
            this.PropertyChanged += handler;
            Characteristics.PropertyChangedSubscript(handler);
        }
        */
        #endregion

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
        }

        #region IClonable

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
