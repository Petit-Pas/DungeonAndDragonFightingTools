﻿using DDFight.Game.Entities;
using DDFight.Game.Fight.FightEvents;
using DDFight.Tools;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public int TurnIndex
        {
            get => _turnIndex;
            set
            {
                _turnIndex = value;
                NotifyPropertyChanged();
            }
        }
        public int _turnIndex = -1;

        public void NextTurn()
        {
            Console.WriteLine("Next Turn");
            if (TurnIndex != -1)
            {
                Global.Context.FightContext.FightersList.Fighters.ElementAt((int)TurnIndex).EndTurn();
                OnEndTurn(new TurnEndedEventArgs()
                {
                    Character = Global.Context.FightContext.FightersList.Fighters.ElementAt((int)TurnIndex),
                    CharacterIndex = (int)TurnIndex,
                });
            }
            int newTurn = TurnIndex + 1;
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
            OnSelectedCharacter(new SelectedCharacterEventArgs()
            {
                Character = tmp,
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

        public void OnEndTurn(TurnEndedEventArgs args)
        {
            if (TurnEnded != null)
            {
                TurnEnded(this, args);
            }
            DumpFigthState();
        }

        private void DumpFigthState()
        {
            Logger.Log("==============================");
            Logger.Log("End of the turn of " + CurrentlyPlaying.DisplayName);
            Logger.Log("");
            foreach (PlayableEntity tmp in FightersList.Fighters)
            {
                tmp.Dump();
            }
            Logger.Log("==============================");
        }

        public event EndTurnEventHandler TurnEnded;

        public void OnSelectedCharacter(SelectedCharacterEventArgs args)
        {
            CurrentlySelected = args.Character;
            if (CharacterSelected != null)
            {
                CharacterSelected(this, args);
            }
        }
        public event SelectedCharacterEventHandler CharacterSelected;

        public PlayableEntity CurrentlyPlaying
        {
            get
            {
                if (FightersList.Fighters.Count == 0)
                    return null;
                if (TurnIndex < 0 || TurnIndex >= FightersList.Fighters.Count)
                    return null;
                return FightersList.Fighters.ElementAt(TurnIndex);
            }
        }

        public PlayableEntity CurrentlySelected
        {
            get => _currentlySelected;
            set
            {
                _currentlySelected = value;
                NotifyPropertyChanged();
            }
        }
        private PlayableEntity _currentlySelected = null;

        public void RemoveCharacterFromFight(PlayableEntity to_remove)
        {
            bool wasPlaying = false;

            if (to_remove == CurrentlyPlaying)
                wasPlaying = true;

            // modifies the turn order of the character that were playing after the removed character
            foreach (PlayableEntity tmp in FightersList.Fighters)
            {
                if (tmp.TurnOrder > to_remove.TurnOrder)
                    tmp.TurnOrder -= 1;
            }

            // if it was the turn of the character, we need to skip its turn
            if (wasPlaying)
            {
                NextTurn();
                TurnIndex -= 1;
            }
            // if it was previously in the same round
            else if (CurrentlyPlaying.TurnOrder >= to_remove.TurnOrder)
            {
                TurnIndex -= 1;
            }
            FightersList.Fighters.Remove(to_remove);
        }

        #endregion turn

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
        #endregion INotifyPropertyChanged

        public void Reset()
        {
            TurnIndex = -1;
            RoundCount = 0;
            FightersList.Fighters.Clear();
        }

    }
}
