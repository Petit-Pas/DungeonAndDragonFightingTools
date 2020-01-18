using DDFight.Game.Characteristics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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
