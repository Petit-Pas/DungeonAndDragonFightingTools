using DDFight.Game.Fight.FightEvents;
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
            if (TurnIndex != -1)
            {
                // 1st turn of first round
                Global.Context.FightContext.FightersList.Fighters.ElementAt((int)TurnIndex).EndTurn();
                OnEndTurn(new EndTurnEventArgs()
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

        public void OnEndTurn(EndTurnEventArgs args)
        {
            if (TurnEnded != null)
            {
                TurnEnded(this, args);
            }
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
            TurnIndex = -1;
            RoundCount = 0;
            FightersList.Fighters.Clear();
        }

    }
}
