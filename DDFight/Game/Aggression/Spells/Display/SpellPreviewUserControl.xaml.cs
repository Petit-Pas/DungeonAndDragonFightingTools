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
    /// Interaction logic for SpellPreviewUserControl.xaml
    /// </summary>
    public partial class SpellPreviewUserControl : UserControl
    {
        public SpellPreviewUserControl()
        {
            InitializeComponent();
            DataContextChanged += SpellPreviewUserControl_DataContextChanged;
        }

        private void SpellPreviewUserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext == null)
                this.Visibility = Visibility.Collapsed;
            else
                this.Visibility = Visibility.Visible;
        }
    }
}
