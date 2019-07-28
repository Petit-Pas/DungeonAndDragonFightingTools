﻿using DDFight.Controlers.InputBoxes;
using DDFight.Tools;
using System;
using System.Collections.Generic;
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
        private List<UserControl> parameters = new List<UserControl>();

        /// <summary>
        ///     Ctor
        /// </summary>
        public NewCharacterWindow()
        {
            InitializeComponent();

            // add every parameter to the list IN THE RIGHT ORDER
            // The order is important, as the control will jump from one to another in that order when pressing the Enter key
            parameters.Add(NameBox);
            parameters.Add(InitiativeBox);
            parameters.Add(CABox);
            parameters.Add(MaxHPBox);
            parameters.Add(HPBox);


            NameBox.SetFocus();

            foreach (UserControl ctrl in parameters)
            {
                ctrl.KeyDown += Generic_KeyDown;
            }
        }

        /// <summary>
        ///     Checks that the given textob isn't empty
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        private bool is_valid(Control ctrl)
        {
            switch (ctrl)
            {
                case IIsValidable box:
                    return box.IsValid();
                default:
                    Console.WriteLine("ERROR: unimplemented type for IsValid in NewCharacterWindow.xaml.cs: {0}", ctrl.GetType());
                    return false;
            }
        }

        /// <summary>
        ///     checks if all parameters are corrects
        /// </summary>
        /// <returns></returns>
        private bool are_all_valids ()
        {
            foreach (Control ctrl in parameters)
            {
                switch (ctrl)
                {
                    case IIsValidable _ctrl:
                        if (_ctrl.IsValid () == false)
                        {
                            return false;
                        }
                        break;
                    default:
                        break;
                }
            }
            return true;
        }

        /// <summary>
        ///     Sets the focus on the next element
        /// </summary>
        /// <param name="current"></param>
        /// <param name="next"></param>
        private void focus_next(Control current, Control next)
        {
            if (next != null)
            {
                if (is_valid(current))
                {
                    switch (next)
                    {
                        case IIsFocusable box:
                            box.SetFocus();
                            break;
                        default:
                            Console.WriteLine("ERROR: unimplemented type for Focus in NewCharacterWindow.xaml.cs: {0}", next.GetType());
                            break;
                    }
                }
            }
            else
            {
                if (are_all_valids())
                {
                    Close();
                }
            }
        }

        /// <summary>
        ///     Generic Key handler to handle the return key and switch focus between elements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Generic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                for (int i = 0; i != parameters.Count; i += 1)
                {
                    if (parameters[i] == sender)
                    {
                        focus_next((Control)sender, i + 1 != parameters.Count ? parameters[i + 1] : null);
                    }
                }
            }
        }
    }
}
