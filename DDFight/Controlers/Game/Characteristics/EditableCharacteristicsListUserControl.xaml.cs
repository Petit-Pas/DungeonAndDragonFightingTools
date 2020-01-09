﻿using DDFight.Game;
using DDFight.Game.Characteristics;
using DDFight.ValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DDFight;
using DDFight.Controlers.InputBoxes;
using System.Threading;

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
