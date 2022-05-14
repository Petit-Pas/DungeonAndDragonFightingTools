using BaseToolsLibrary.IO;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Fight.Events;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BaseToolsLibrary.DependencyInjection;
using TempExtensionsPlayableEntity;

// TODO this class should be injected through DI as singleton instead of being singleton from client code ala Global.Context....

namespace DDFight.Game.Fight
{
    public class FightDataContext : INotifyPropertyChanged
    {
        private static readonly Lazy<IFightersProvider> _lazyFighterProvider = new(DIContainer.GetImplementation<IFightersProvider>);
        private static readonly IFightersProvider _fightersProvider = _lazyFighterProvider.Value;

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

        // TODO couuld we keep the fighting entity displayname instead? might make this easier
        /// <summary>
        ///     Keeps tracks of the index of the fighter in the round
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
        private int _turnIndex = -1;

        public void NextTurn()
        {
            Console.WriteLine("Next Turn");
            if (TurnIndex != -1)
            {
                var entityEnd = _fightersProvider.GetFighterByIndex(TurnIndex);
                entityEnd.EndTurn();
                //OnEndTurn(new TurnEndedEventArgs(entityEnd.DisplayName));
            }
            var newTurn = TurnIndex + 1;
            if (newTurn >= _fightersProvider.FighterCount)
            {
                TurnIndex = 0;
                RoundCount += 1;
            }
            else
            {
                TurnIndex = newTurn;
            }
            var entityStart = _fightersProvider.GetFighterByIndex(TurnIndex);
            entityStart.StartNewTurn();
            OnStartNewTurn(new TurnStartedEventArgs(entityStart.DisplayName));
            // TODO should be implemented somewhere in the commands, should also probably go through an event of the character
            //OnSelectedCharacter(new SelectedCharacterEventArgs()
            //{
            //    Character = entityStart,
            //});
        }

        private void OnStartNewTurn(TurnStartedEventArgs args)
        {
            NewTurnStarted?.Invoke(this, args);
        }
        public event TurnStarted NewTurnStarted;

        private void DumpFightState()
        {
            Logger.Log("==============================");
            Logger.Log("End of the turn of " + CurrentlyPlaying.DisplayName);
            Logger.Log("");
            foreach (var tmp in _fightersProvider.Fighters)
            {
                tmp.Dump();
            }
            Logger.Log("==============================");
        }


        public PlayableEntity CurrentlyPlaying
        {
            get
            {
                if (_fightersProvider.FighterCount == 0)
                    return null;
                if (TurnIndex < 0 || TurnIndex >= _fightersProvider.FighterCount)
                    return null;
                return _fightersProvider.GetFighterByIndex(TurnIndex);
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

        public void RemoveCharacterFromFight(PlayableEntity toRemove)
        {
            var wasPlaying = (toRemove.DisplayName == CurrentlyPlaying.DisplayName);

            // modifies the turn order of the character that were playing after the removed character
            foreach (var tmp in _fightersProvider.Fighters)
            {
                if (tmp.TurnOrder > toRemove.TurnOrder)
                    tmp.TurnOrder -= 1;
            }

            // if it was the turn of the character, we need to skip its turn
            if (wasPlaying)
            {
                NextTurn();
                TurnIndex -= 1;
            }
            // if it was previously in the same round
            else if (CurrentlyPlaying.TurnOrder >= toRemove.TurnOrder)
            {
                TurnIndex -= 1;
            }
            _fightersProvider.RemoveFighter(toRemove);
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
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion INotifyPropertyChanged

        public void Reset()
        {
            TurnIndex = -1;
            RoundCount = 0;
            _fightersProvider.Clear();
        }
    }
}
