using DDFight.Game;
using DDFight.Game.Characteristics;
using DDFight.ValidationRules;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Controlers.Game.Characteristics
{
    /// <summary>
    /// Logique d'interaction pour EditableCharacteristicsList.xaml
    /// </summary>
    public partial class EditableCharacteristicsListUserControl : UserControl, IValidable
    {

        private PlayableEntity _dataContext {
            get => (PlayableEntity)this.DataContext;
        }
        public EditableCharacteristicsListUserControl()
        {
            InitializeComponent();
            Loaded += EditableCharacteristicsList_Loaded;
        }

        private void EditableCharacteristicsList_Loaded(object sender, RoutedEventArgs e)
        {
            List<CharacteristicDataContext> items = new List<CharacteristicDataContext>();
            foreach (CharacteristicDataContext dc in _dataContext.Characteristics.CharacteristicsList)
            {
                items.Add(dc);
            }
            CharacteristicsListView.ItemsSource = items;
        }
        
        public bool IsValid()
        {
            return this.AreAllChildrenValid();
        }
    }
}
