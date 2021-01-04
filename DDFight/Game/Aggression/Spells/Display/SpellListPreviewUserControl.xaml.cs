﻿using System;
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

namespace DDFight.Game.Aggression.Spells.Display
{
    /// <summary>
    /// Interaction logic for SpellListPreviewUserControl.xaml
    /// </summary>
    public partial class SpellListPreviewUserControl : UserControl
    {
        private SpellsList data_context
        {
            get => (SpellsList)DataContext;
        }

        public SpellListPreviewUserControl()
        {
            InitializeComponent();

        }

        private void SpellListControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpellListControl.SelectedIndex != -1)
                SpellPreviewControl.DataContext = SpellListControl.SelectedItem;
            else
                SpellPreviewControl.DataContext = null;
        }
    }
}