using DDFight.Controlers.InputBoxes;
using DDFight.Game;
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
        private List<UserControl> controls = new List<UserControl>();

        private CharacterDataContext data_context { get => (CharacterDataContext)DataContext; }

        /// <summary>
        ///     Ctor
        /// </summary>
        public NewCharacterWindow()
        {
            InitializeComponent();

            controls.Add(NameBoxUserControl);
            controls.Add(CABoxUserControl);
            controls.Add(MaxHPBoxUserControl);
            controls.Add(HPBoxUserControl);
            controls.Add(CharacteristicsUserControl);

            NameBoxUserControl.SetFocus();
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
                case IValidable box:
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
                        break;
                }
            }
            return true;
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            data_context.Validated = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            data_context.Validated = false;
            Close();
        }
    }
}
