using BaseToolsLibrary.Extensions;
using BaseToolsLibrary.IO;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDToolsLibrary.Fight
{
    public class FightersList : GenericList<PlayableEntity>, IFighterProvider
    {

        private FightersList()
        {
        }

        public static FightersList Instance => _instance;
        private static FightersList _instance = new ();

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


        /// <summary>
        ///     Will sort the list in Initiative + Dexterity order, then sets the PlayableEntity.TurnOrder value
        ///     
        ///     /!\ should only be called at start of fight, there is a more complicate handling for when in middle of a fight
        ///     See SetTurnOrdersMiddleFight()
        /// </summary>
        public void SetTurnOrders()
        {

            this.Sort(((x, y) => {
                var val = (x.InitiativeRoll + x.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity)).CompareTo
                                                (y.InitiativeRoll + y.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity));
                if (val != 0)
                    return -val;

                val = (x.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity).CompareTo(
                    y.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity)));
                if (val != 0)
                    return -val;

                val = string.Compare(x.Name, y.Name, StringComparison.Ordinal);
                if (val != 0)
                    return val;
                var num1 = int.Parse(x.DisplayName[(x.Name.Length + 2)..]);
                var num2 = int.Parse(y.DisplayName[(x.Name.Length + 2)..]);
                return num1.CompareTo(num2);

            }));


            uint i = 1;
            foreach (var fighter in this)
            {
                fighter.TurnOrder = i;
                i++;
            }

        }

        public void SetTurnOrdersMiddleFight()
        {
            foreach (var fighter in this)
            {
                if (fighter.InitiativeRoll == 0)
                {
                    fighter.InitiativeRoll = (uint)DiceRoll.Roll("1d20");
                    foreach (var tmp in this)
                    {
                        if (tmp.Name == fighter.Name)
                            tmp.InitiativeRoll = fighter.InitiativeRoll;
                    }
                }
            }
            SetTurnOrders();
        }

        public PlayableEntity GetFighterByDisplayName(string displayName)
        {
            var result = this.FirstOrDefault(x => x.DisplayName == displayName);
            if (result == null)
                Console.WriteLine($"WARNING: FightersList could not find a fighter with name {displayName}");
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
    }
}
