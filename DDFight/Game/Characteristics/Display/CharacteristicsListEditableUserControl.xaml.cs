using DDFight.Game.Characteristics;
using DDFight.Game.Entities;
using DDFight.Tools;
using DDFight.ValidationRules;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfToolsLibrary.ValidationRules;

namespace DDFight.Controlers.Game.Characteristics
{
    /// <summary>
    /// Logique d'interaction pour EditableCharacteristicsList.xaml
    /// </summary>
    public partial class CharacteristicsListEditableUserControl : UserControl, IValidable
    {

        private PlayableEntity _dataContext {
            get => (PlayableEntity)this.DataContext;
        }
        public CharacteristicsListEditableUserControl()
        {
            InitializeComponent();
            Loaded += EditableCharacteristicsList_Loaded;
        }

        private void EditableCharacteristicsList_Loaded(object sender, RoutedEventArgs e)
        {
            CharacteristicsListView.ItemsSource = _dataContext.Characteristics.CharacteristicsList;
        }
        
        public bool IsValid()
        {
            return this.AreAllChildrenValid();
        }
    }
}
