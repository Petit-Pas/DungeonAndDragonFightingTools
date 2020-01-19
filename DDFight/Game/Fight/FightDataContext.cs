﻿using DDFight.Game.Fight.FightEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDFight.Game.Fight
{
    public class FightDataContext : INotifyPropertyChanged
    {
        /// <summary>
        ///     The list of the entities that shall fight when the Fight button is pressed
        /// </summary>
        [XmlIgnore]
        public FightingCharactersDataContext FightersList = new FightingCharactersDataContext();

        /// <summary>
        ///     Counts the amount of rounds of a fight
        /// </summary>
        public uint RoundCount
        {
            get => _roundCount;
            set
            {
                _roundCount = value;
                NotifyPropertyChanged();
            }
        }
        private uint _roundCount = 0;

        #region Turn

        /// <summary>
        ///     Counts the amount of turn in 1 round
        /// </summary>
        public uint TurnIndex
        {
            get => _turnIndex;
            set
            {
                _turnIndex = value;
                NotifyPropertyChanged();
            }
        }
        public uint _turnIndex = 0;

        public void NextTurn()
        {
            Global.Context.FightContext.FightersList.Fighters.ElementAt((int)TurnIndex).EndTurn();
            OnEndTurn(new EndTurnEventArgs() {
                Character = Global.Context.FightContext.FightersList.Fighters.ElementAt((int)TurnIndex),
                CharacterIndex = (int)TurnIndex,
            });
            uint newTurn = TurnIndex + 1;
            if (newTurn >= FightersList.Fighters.Count())
            {
                TurnIndex = 0;
                RoundCount += 1;
            }
            else
            {
                TurnIndex = newTurn;
            }
            PlayableEntity tmp = Global.Context.FightContext.FightersList.Fighters.ElementAt((int)TurnIndex);
            tmp.StartNewTurn();
            OnStartNewTurn(new StartNewTurnEventArgs() 
            { 
                Character = tmp,  
                CharacterIndex = (int)TurnIndex,
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

        public void OnEndTurn(EndTurnEventArgs args)
        {
            if (TurnEnded != null)
            {
                TurnEnded(this, args);
            }
        }
        
        public event EndTurnEventHandler TurnEnded;

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

        public void Reset()
        {
            TurnIndex = 0;
            RoundCount = 0;
            FightersList.Fighters.Clear();
        }

    }
}
