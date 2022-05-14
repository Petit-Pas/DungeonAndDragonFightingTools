using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using BaseToolsLibrary.DependencyInjection;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight.Events;

namespace DnDToolsLibrary.Fight
{
    public class TurnManager : ITurnManager, INotifyPropertyChanged
    {
        private Lazy<IFightersProvider> _lazyFightersProvider = new(DIContainer.GetImplementation<IFightersProvider>());
        private IFightersProvider _fightersProvider => _lazyFightersProvider.Value;

        public TurnManager()
        {
        }

        private static readonly Comparison<PlayableEntity> _initiativeSorter = (x, y) =>
        {
            var comparison = x.GetInitiative().CompareTo(y.GetInitiative());
            // initiative is different, easy case
            if (comparison != 0)
            {
                return comparison;
            }
            // initiative is the same, we need to compare the dexterity now

            comparison = x.GetDexModifier().CompareTo(y.GetDexModifier());
            // dex is different, easy case
            if (comparison != 0)
            {
                return comparison;
            }
            // dexterity is the same, we need to compare the name of the entity to have same occurrence of an entity one after the other

            comparison = string.Compare(x.Name, y.Name, StringComparison.Ordinal);
            // names are different, easy case
            if (comparison != 0)
            {
                return comparison;
            }
            // the compared entities are the same monsters, order them by their number ( - xx suffix of DisplayName)

            return x.GetNameNumber().CompareTo(y.GetNameNumber());
        };

        public void SetTurnOrders()
        {
            MakeSureMonstersInitiativeAreOk();
            MakeSureCharactersInitiativeAreOk();
            
            _fightersProvider.OrderFighters(_initiativeSorter);

            uint i = 1;
            foreach (var fighter in _fightersProvider.Fighters)
            {
                fighter.TurnOrder = i;
                i += 1;
            }
        }

        private void MakeSureCharactersInitiativeAreOk()
        {
            // making sure that all characters have initiative
            foreach (var character in _fightersProvider.Characters)
            {
                if (character.InitiativeRoll == 0)
                {
                    character.InitiativeRoll = (uint)DiceRoll.Roll("1d20");
                    Trace.WriteLine($"Warning: character {character.DisplayName} had no initiative.");
                }
            }
        }

        private void MakeSureMonstersInitiativeAreOk()
        {
            // making sure that all monsters of same kind have the correct initiative
            foreach (var monsters in _fightersProvider.Monsters.GroupBy(x => x.Name))
            {
                var initiative = monsters.FirstOrDefault(x => x.InitiativeRoll != 0)?.InitiativeRoll ?? 0;

                // for some reason, the group of monsters had no initiative
                if (initiative == 0)
                {
                    initiative = (uint)DiceRoll.Roll("1d20");
                    Trace.WriteLine($"Warning: monsters {monsters.First().Name} have no initiative at all");
                }

                foreach (var monster in monsters)
                {
                    monster.InitiativeRoll = initiative;
                }
            }
        }

        public uint RoundCount
        {
            get => _roundCount;
            set
            {
                if (_roundCount != value)
                {
                    _roundCount = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private uint _roundCount = 0;

        public int TurnIndex
        {
            get => _turnIndex;
            set
            {
                if (_turnIndex != value)
                {
                    _turnIndex = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _turnIndex = -1;


        public event TurnStarted TurnStarted;
        public event TurnEnded TurnEnded;
        
        public void InvokeTurnStarted(TurnStartedEventArgs args)
        {
            TurnStarted?.Invoke(this, args);
        }

        public void InvokeTurnEnded(TurnEndedEventArgs args)
        {
            TurnEnded?.Invoke(this, args);
        }

        public void Reset()
        {
            TurnIndex = -1;
            RoundCount = 0;
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
    }
}
