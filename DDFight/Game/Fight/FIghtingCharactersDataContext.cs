using DDFight.Game.Characteristics;
using DDFight.Game.Dices;
using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DDFight.Game.Fight
{
    public class FightingCharactersDataContext : INotifyPropertyChanged
    {
        public ObservableCollection<PlayableEntity> Fighters 
        {
            get => _figthers;
            set
            {
                _figthers = value;
                NotifyPropertyChanged();
            }
        } 
        private ObservableCollection<PlayableEntity> _figthers = new ObservableCollection<PlayableEntity>();

        /// <summary>
        ///     Sorts the fighters according to their displayName
        /// </summary>
        public void Sort()
        {
            Fighters.Sort((x, y) => {
                return x.DisplayName.CompareTo(y.DisplayName);
            });
        }

        /// <summary>
        ///     Adds a character (only once per character, as its not a copy)
        /// </summary>
        /// <param name="character"></param>
        public void AddCharacter(Character character)
        {
            try
            {
                if (Fighters.SingleOrDefault(x => x.Name == character.Name) == null)
                {
                    Fighters.Add(character);
                    Sort();
                }
            }
            catch (Exception err)
            {
                Logger.Log("ERROR: caught an exception while trying to add a character to the fighting list: " + err.Message);
            }
        }

        /// <summary>
        ///     Adds a monster in the Fighting List (a copy of it) and gives it an index in the PlayableEntity.DisplayName
        /// </summary>
        /// <param name="character"></param>
        public void AddMonster(Monster monster)
        {
            IEnumerable<PlayableEntity> list = Fighters.Where(x => x.Name == monster.Name);
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
            Fighters.Add(new_fighter);
            Sort();
        }


        /// <summary>
        ///     Will sort the list in Initiative + Dexterity order, then sets the PlyableEntity.TurnOrder value
        ///     
        ///     /!\ should only be called at start of fight, theer is a more complicate handling for when in middle of a fight
        ///     See SetTurnOrdersMiddleFight()
        /// </summary>
        public void SetTurnOrders()
        {
            Fighters.Sort (((x, y) => { return -(x.InitiativeRoll + x.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity)).CompareTo
                                                (y.InitiativeRoll + y.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity)); }));
            
            Fighters.Sort (((x, y) => {
                if (x.InitiativeRoll + x.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity) !=
                    y.InitiativeRoll + y.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity))
                {
                    return 0;
                }
                return -(x.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity).CompareTo(
                    y.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Dexterity))
                );
            }));

            uint i = 1;
            foreach (PlayableEntity fighter in Fighters)
            {
                fighter.TurnOrder = i;
                i++;
            }
        }

        public void SetTurnOrdersMiddleFight()
        {
            foreach (PlayableEntity fighter in Global.Context.FightContext.FightersList.Fighters)
            {
                if (fighter.InitiativeRoll == 0)
                {
                    fighter.InitiativeRoll = (uint)DiceRoll.Roll("1d20");
                    foreach (PlayableEntity tmp in Global.Context.FightContext.FightersList.Fighters)
                    {
                        if (tmp.Name == fighter.Name)
                            tmp.InitiativeRoll = fighter.InitiativeRoll;
                    }
                }
            }
            SetTurnOrders();
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
