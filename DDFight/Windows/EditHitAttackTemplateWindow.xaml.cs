﻿using DDFight.Controlers.Game.Attacks;
using DDFight.Game.Aggression.Attacks;
using DDFight.Resources;
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
using System.Windows.Shapes;

namespace DDFight.Windows
{
    /// <summary>
    /// Interaction logic for EditAAttackTemplate.xaml
    /// </summary>
    public partial class EditHitAttackTemplateWindow : Window
    {
        private List<UserControl> controls = new List<UserControl>();

        private HitAttackTemplate data_context
        {
            get => (HitAttackTemplate)DataContext;
        }

        public EditHitAttackTemplateWindow()
        {
            InitializeComponent();

            controls.Add(AttacksListUserControl);
            Loaded += EditHitAttackTemplateWindow_Loaded;
        }

        private void EditHitAttackTemplateWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private bool are_all_valids()
        {
            foreach (Control ctrl in controls)
            {
                switch (ctrl)
                {
                    case IValidable _ctrl:
                        if (_ctrl.IsValid() == false)
                        {
                            return false;
                        }
                        break;
                    default:
                        Console.WriteLine("Warning: unimplemented type for IsValid in NewCharacterWindow.xaml.cs: {0}", ctrl.GetType());
                        break;
                }
            }
            return true;
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
            if (!are_all_valids())
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

                message.ShowDialog();
            }
            else
            {
                data_context.Validated = true;
                self_close = true;
                Close();
            }
        }

        /// <summary>
        ///     Handler for the click event on the Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            data_context.Validated = false;
            self_close = true;
            Close();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!self_close)
            {
                // user exited the window manually

                if (!are_all_valids())
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
                    window.ShowDialog();

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
                    window.ShowDialog();

                    if (context.Yes)
                    {
                        data_context.Validated = true;
                    }
                }
            }
        }
    }
}