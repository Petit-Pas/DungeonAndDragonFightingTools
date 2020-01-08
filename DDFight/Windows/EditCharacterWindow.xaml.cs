using DDFight.Controlers.InputBoxes;
using DDFight.Game;
using DDFight.Game.Aggression;
using DDFight.Game.Aggression.Attacks;
using DDFight.Game.DamageAffinity;
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
    public partial class EditCharacterWindow : Window
    {
        /// <summary>
        ///     contains a list of the controls
        /// </summary>
        private List<UserControl> controls = new List<UserControl>();

        private CharacterDataContext data_context { get => (CharacterDataContext)DataContext; }

        /// <summary>
        ///     Ctor
        /// </summary>
        public EditCharacterWindow()
        {
            InitializeComponent();

            controls.Add(MainInfo);

            MainInfo.NameBoxUserControl.SetFocus();

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

        private void Window_Closing(object sender, CancelEventArgs e)
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

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            EditAAttackTemplateWindow window = new EditAAttackTemplateWindow
            {
                Owner = Window.GetWindow(this),
            };
            HitAttackTemplate dc = new HitAttackTemplate();
            dc.Name = "Name";
            dc.DamageList.Add(new DamageTemplate("1d4+2", DamageTypeEnum.Slashing));
            dc.DamageList.Add(new DamageTemplate("2d6+3", DamageTypeEnum.Fire));
            window.DataContext = dc;

            window.ShowDialog();

            Console.WriteLine(dc.Name);
            Console.WriteLine(dc.HitAmount);
            Console.WriteLine(dc.HitBonus);
            Console.WriteLine(dc.DamageList.ToString());
            Console.WriteLine(dc.DamageList[0].DamageType);
        }
    }
}
