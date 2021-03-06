﻿using DDFight.Resources;
using DnDToolsLibrary.Attacks.HitAttacks;
using System.Windows;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace DDFight.Windows
{
    /// <summary>
    /// Interaction logic for EditAAttackTemplate.xaml
    /// </summary>
    public partial class HitAttackTemplateEditWindow : Window, IValidableWindow
    {
    
        private HitAttackTemplate data_context
        {
            get => (HitAttackTemplate)DataContext;
        }

        public HitAttackTemplateEditWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Sets to true if the window close itself (not the red X button)
        /// </summary>
        private bool self_close = false;

        /// <summary>
        ///     Handler for the click event on the validate button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.AreAllChildrenValid())
            {
                StatusMessageWindowDataContext context = new StatusMessageWindowDataContext
                {
                    Message = "At least one of the parameters is wrong",
                    Icon = ResourceManager.BmUnchecked(),
                };
                StatusMessageWindow message = new StatusMessageWindow
                {
                    Title = "Cannot validate",
                    DataContext = context,
                    Owner = this,
                };
                message.ShowCentered();
            }
            else
            {
                this.Validated = true;
                self_close = true;
                Close();
            }
        }

        public bool Validated { get; set; } = false;

        /// <summary>
        ///     Handler for the click event on the Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Validated = false;
            self_close = true;
            Close();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!self_close)
            {
                // user exited the window manually

                if (!this.AreAllChildrenValid())
                {
                    AskYesNoDataContext context = new AskYesNoDataContext
                    {
                        Message = "At least 1 of the Parameter is wrong, if you exit, nothing will be saved. Exit anyway ?",
                    };
                    AskYesNoWindow window = new AskYesNoWindow
                    {
                        Owner = this,
                        DataContext = context,
                    };
                    window.ShowCentered();

                    if (!context.Yes)
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    AskYesNoDataContext context = new AskYesNoDataContext
                    {
                        Message = "You didn't use the validate button, do you wish to save your changes anyway ?",
                    };
                    AskYesNoWindow window = new AskYesNoWindow
                    {
                        Owner = this,
                        DataContext = context,
                    };
                    window.ShowCentered();

                    if (context.Yes)
                    {
                        this.Validated = true;
                    }
                }
            }
        }
    }
}
