﻿using DDFight.Controlers.InputBoxes;
using DDFight.Game;
using DDFight.Resources;
using DDFight.Tools;
using DDFight.ValidationRules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Windows
{
    /// <summary>
    ///     Interaction logic for NewCharacterWindow.xaml
    /// </summary>
    public partial class NewCharacterWindow : Window
    {
        /// <summary>
        ///     contains a list of the parameters IN THE RIGHT ORDER
        /// </summary>
        private List<UserControl> controls = new List<UserControl>();

        private CharacterDataContext data_context { get => (CharacterDataContext)DataContext; }

        /// <summary>
        ///     Ctor
        /// </summary>
        public NewCharacterWindow()
        {
            InitializeComponent();

            controls.Add(NameBoxUserControl);
            controls.Add(LevelBoxUserControl);
            controls.Add(CABoxUserControl);
            controls.Add(MaxHPBoxUserControl);
            controls.Add(HPBoxUserControl);

            controls.Add(CharacteristicsUserControl);

            NameBoxUserControl.SetFocus();

            Loaded += OnControlLoaded;
        }

        /// <summary>
        ///     when the cntrol is loaded, and the DataContext accessible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnControlLoaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        ///     checks if all parameters are corrects
        /// </summary>
        /// <returns></returns>
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
            Close();
        }
    }
}
