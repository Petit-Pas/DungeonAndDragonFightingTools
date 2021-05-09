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
    public class FightersList : GenericList<PlayableEntity>
    {

        private FightersList()
        {
        }

        public static FightersList Instance
        {
            get => _instance;
        }
        private static FightersList _instance = new FightersList();

        /// <summary>
        ///     Sorts the fighters according to their displayName
        /// </summary>
        public void Sort()
        {
            Elements.Sort((x, y) => {
                int retval = x.Name.CompareTo(y.Name);
                if (retval != 0)
                    return retval;
                int num1 = Int32.Parse(x.DisplayName.Substring(x.Name.Length + 2));
                int num2 = Int32.Parse(y.DisplayName.Substring(x.Name.Length + 2));
                return num1.CompareTo(num2);
            });
        }

        public void AddElement(PlayableEntity entity = null)
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
                if (Elements.SingleOrDefault(x => x.Name == character.Name) == null)
                    base.AddElementSilent(character);
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
            IEnumerable<PlayableEntity> list = Elements.Where(x => x.Name == monster.Name);
            PlayableEntity new_fighter = (PlayableEntity)(monster.Clone());

            int i = 0;
            if (list.Count() != 0)
                new_fighter.InitiativeRoll = list.ElementAt(i).InitiativeRoll;
            for (; i < list.Count(); i++)
            {
                string tmp = new_fighter.Name + " - " + i;
                if (list.ElementAt(i).DisplayName != tmp)
                    break;
            }
            new_fighter.DisplayName = new_fighter.Name + " - " + i;
            base.AddElementSilent(new_fighter);
        }


        /// <summary>
        ///     Will sort the list in Initiative + Dexterity order, then sets the PlyableEntity.TurnOrder value
        ///     
        ///     /!\ should only be called at start of fight, thera is a more complicate handling for when in middle of a fight
        ///     See SetTurnOrdersMiddleFight()
        /// </summary>
        public void SetTurnOrders()
        {

            Elements.Sort(((x, y) => {
                int val = (x.InitiativeRoll + x.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity)).CompareTo
                                                (y.InitiativeRoll + y.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity));
                if (val != 0)
                    return -val;

                val = (x.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity).CompareTo(
                    y.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity)));
                if (val != 0)
                    return -val;

                val = x.Name.CompareTo(y.Name);
                if (val != 0)
                    return val;
                int num1 = Int32.Parse(x.DisplayName.Substring(x.Name.Length + 2));
                int num2 = Int32.Parse(y.DisplayName.Substring(x.Name.Length + 2));
                return num1.CompareTo(num2);

            }));


            uint i = 1;
            foreach (PlayableEntity fighter in Elements)
            {
                fighter.TurnOrder = i;
                i++;
            }

        }

        public void SetTurnOrdersMiddleFight()
        {
            foreach (PlayableEntity fighter in Elements)
            {
                if (fighter.InitiativeRoll == 0)
                {
                    fighter.InitiativeRoll = (uint)DiceRoll.Roll("1d20");
                    foreach (PlayableEntity tmp in Elements)
                    {
                        if (tmp.Name == fighter.Name)
                            tmp.InitiativeRoll = fighter.InitiativeRoll;
                    }
                }
            }
            SetTurnOrders();
        }
    }
}
