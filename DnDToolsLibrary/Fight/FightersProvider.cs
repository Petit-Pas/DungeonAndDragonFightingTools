using BaseToolsLibrary.Extensions;
using BaseToolsLibrary.IO;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Memory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace DnDToolsLibrary.Fight
{
    public class FightersProvider : GenericList<PlayableEntity>, IFightersProvider
    {

        public FightersProvider()
        {
        }

        /// <summary>
        ///     Sorts the fighters according to their displayName
        /// </summary>
        private void Sort()
        {
            this.Sort((x, y) => {
                var retVal = string.Compare(x.Name, y.Name, StringComparison.Ordinal);
                if (retVal != 0)
                    return retVal;
                var num1 = int.Parse(x.DisplayName[(x.Name.Length + 2)..]);
                var num2 = int.Parse(y.DisplayName[(x.Name.Length + 2)..]);
                return num1.CompareTo(num2);
            });
        }

        public void AddElement(PlayableEntity entity)
        {
            if (entity != null)
            {
                if (entity.GetType() == typeof(Character))
                    add_character(entity as Character);
                else if (entity.GetType() == typeof(Monster))
                    add_monster(entity as Monster);
                Sort();
            }
            else
                Logger.Log("WARNING: tried to add null entity to FighterList");
        }

        /// <summary>
        ///     Adds a character (only once per character, as its not a copy)
        /// </summary>
        /// <param name="character"></param>
        private void add_character(Character character)
        {
            try
            {
                if (this.SingleOrDefault(x => x.Name == character.Name) == null)
                    AddElementSilent(character);
            }
            catch (Exception err)
            {
                Logger.Log("ERROR: caught an exception while trying to add a character to the fighting list: " + err.Message);
            }
        }

        /// <summary>
        ///     Adds a monster in the Fighting List (a copy of it) and gives it an index in the PlayableEntity.DisplayName
        /// </summary>
        /// <param name="monster"></param>
        private void add_monster(Monster monster)
        {
            var list = this.Where(x => x.Name == monster.Name).ToList();
            var newFighter = (PlayableEntity)(monster.Clone());

            var i = 0;
            if (list.Count != 0)
                newFighter.InitiativeRoll = list.ElementAt(i).InitiativeRoll;
            for (; i < list.Count; i++)
            {
                var tmp = newFighter.Name + " - " + i;
                if (list.ElementAt(i).DisplayName != tmp)
                    break;
            }
            newFighter.DisplayName = newFighter.Name + " - " + i;
            AddElementSilent(newFighter);
        }


        public PlayableEntity First()
        {
            return this.ElementAt(0);
        }

        public void OrderFighters(Comparison<PlayableEntity> initiativeSorter)
        {
            this.Sort(initiativeSorter);
        }

        public IEnumerable<Monster> Monsters => this.OfType<Monster>();
        public IEnumerable<Character> Characters => this.OfType<Character>();
        public IEnumerable<PlayableEntity> Fighters => this;

        public ObservableCollection<PlayableEntity> GetObservableCollection()
        {
            return this;
        }



        public int GetCurrentTurnIndex()
        {
            // TODO 
            throw new NotImplementedException();
        }

        public PlayableEntity GetFighterByDisplayName(string displayName)
        {
            var result = this.FirstOrDefault(x => x.DisplayName == displayName);
            if (result == null)
                Console.WriteLine($"WARNING: FightersProvider could not find a fighter with name {displayName}");
            return result;
        }

        public int CountWithName(string name)
        {
            return this.Count(x => x.Name == name);
        }

        public void AddFighter(PlayableEntity fighter)
        {
            AddElementSilent(fighter);
            Sort();
        }

        public List<string> GetFightersNames()
        {
            return this.Select(x => x.DisplayName).ToList();
        }

        public void AddOrUpdateFighter(PlayableEntity fighter)
        {
            var inPlace = this.FirstOrDefault(x => x.DisplayName == fighter.DisplayName);
            if (inPlace != null)
                RemoveElement(inPlace);
            AddElementSilent(fighter);
        }

        public bool RemoveFighter(PlayableEntity fighter)
        {
            return Remove(fighter);
        }

        public bool RemoveFighter(string displayName)
        {
            return RemoveFighter(this.FirstOrDefault(x => x.DisplayName == displayName));
        }

        public int FighterCount => Count;

        public PlayableEntity GetFighterByIndex(int fightContextTurnIndex)
        {
            if (fightContextTurnIndex < Count)
            {
                return this.ElementAt(fightContextTurnIndex);
            }

            return null;
        }

        public IEnumerable<PlayableEntity> GetMonstersByName(string name)
        {
            return this.Monsters.Where(x => x.Name == name);
        }

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
            Trace.WriteLine("Info: Switching to next turn");

            // finishing current turn
            if (TurnIndex != -1)
            {
                
            }


        }

    }
}
