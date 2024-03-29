﻿using DDFight.Windows;
using DnDToolsLibrary.Attacks.Damage;
using System.Windows;
using System.Windows.Input;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace DDFight.Game.Aggression.Display
{
    /// <summary>
    /// Interaction logic for DamageTemplateListRollableWindow.xaml
    /// </summary>
    public partial class DamageResultListRollableWindow : Window
    {
        public bool Validated = false;

        private DamageResultList data_context
        {
            get => DataContext as DamageResultList;
        }

        public DamageResultListRollableWindow()
        {
            DataContextChanged += DamageResultListRollableWindow_DataContextChanged;
            InitializeComponent();
            ValidateButton.IsEnabled = false;
        }

        private void DamageResultListRollableWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (data_context != null)
            {
                foreach (DamageResult dmg in data_context)
                {
                    dmg.PropertyChanged += Dmg_PropertyChanged;
                }
            }
        }

        private void Dmg_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ValidateButton.IsEnabled = false;
            if (this.AreAllRollableChildrenRolled())
                ValidateButton.IsEnabled = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.IsRollControlPressed(e))
            {
                e.Handled = true;
                this.RollRollableChildren();
            }
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            Validated = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Validated == false || this.AreAllRollableChildrenRolled() == false)
            {
                AskYesNoDataContext ctx = new AskYesNoDataContext()
                {
                    Message = "Are you sure you wish to cancel this?",
                };
                AskYesNoWindow win = new AskYesNoWindow() { DataContext = ctx };
                win.ShowCentered();

                if (ctx.Yes == false)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
