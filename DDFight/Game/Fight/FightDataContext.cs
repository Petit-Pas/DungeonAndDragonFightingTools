using BaseToolsLibrary.IO;
using DDFight.Game.Fight.FightEvents;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Fight.Events;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using BaseToolsLibrary.DependencyInjection;
using TempExtensionsPlayableEntity;

// TODO this class should be injected through DI as singleton instead of being singleton from client code ala Global.Context....

namespace DDFight.Game.Fight
{
    public class FightDataContext : INotifyPropertyChanged
    {
        private static readonly Lazy<IFighterProvider> _lazyFighterProvider = new(DIContainer.GetImplementation<IFighterProvider>);
        private static readonly IFighterProvider _fighterProvider = _lazyFighterProvider.Value;

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
                var entityEnd = _fighterProvider.ElementAt(TurnIndex);
                entityEnd.EndTurn();
                OnEndTurn(new TurnEndedEventArgs(entityEnd.DisplayName));
            }
            var newTurn = TurnIndex + 1;
            if (newTurn >= _fighterProvider.Count)
            {
                TurnIndex = 0;
                RoundCount += 1;
            }
            else
            {
                TurnIndex = newTurn;
            }
            var entityStart = _fighterProvider.ElementAt(TurnIndex);
            entityStart.StartNewTurn();
            OnStartNewTurn(new StartNewTurnEventArgs(entityStart.DisplayName));
            // TODO should be implemented somewhere in the commands, should also probably go through an event of the character
            OnSelectedCharacter(new SelectedCharacterEventArgs()
            {
                Character = entityStart,
            });
        }

        private void OnStartNewTurn(StartNewTurnEventArgs args)
        {
            NewTurnStarted?.Invoke(this, args);
        }
        public event StartNewTurnEventHandler NewTurnStarted;

        private void OnEndTurn(TurnEndedEventArgs args)
        {
            TurnEnded?.Invoke(this, args);
            DumpFightState();
        }

        private void DumpFightState()
        {
            Logger.Log("==============================");
            Logger.Log("End of the turn of " + CurrentlyPlaying.DisplayName);
            Logger.Log("");
            foreach (var tmp in _fighterProvider)
            {
                tmp.Dump();
            }
            Logger.Log("==============================");
        }

        public event EndTurnEventHandler TurnEnded;

        // TODO check if this even could only take the displayName of the entity instead
        public void OnSelectedCharacter(SelectedCharacterEventArgs args)
        {
            CurrentlySelected = args.Character;
            CharacterSelected?.Invoke(this, args);
        }
        public event SelectedCharacterEventHandler CharacterSelected;

        public PlayableEntity CurrentlyPlaying
        {
            get
            {
                if (_fighterProvider.Count == 0)
                    return null;
                if (TurnIndex < 0 || TurnIndex >= _fighterProvider.Count)
                    return null;
                return _fighterProvider.ElementAt(TurnIndex);
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
            foreach (var tmp in _fighterProvider)
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
            _fighterProvider.Remove(toRemove);
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
            _fighterProvider.Clear();
        }
    }
}
