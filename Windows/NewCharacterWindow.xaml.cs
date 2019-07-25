using DDFight.Controlers.InputBoxes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Windows
{
    /// <summary>
    /// Interaction logic for NewCharacterWindow.xaml
    /// </summary>
    public partial class NewCharacterWindow : Window
    {
        public NewCharacterWindow()
        {
            InitializeComponent();
            NameBox.Focus();

            InitiativeBox.KeyDown += InitiativeBox_KeyDown;
            CABox.KeyDown += CABox_KeyDown;
            MaxHPBox.KeyDown += MaxHPBox_KeyDown;
            HPBox.KeyDown += HPBox_KeyDown;

        }

        /// <summary>
        ///     Checks that the given textob isn't empty
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        private bool is_valid(TextBox box)
        {
            return box.Text != null && box.Text != string.Empty;
        }

        private void set_focus (TextBox box)
        {
            box.Focus();
        }

        private void set_focus (IntTextBox box)
        {
            box.IntBox.Focus();
        }

        private void set_focus (Control ctrl)
        {
            ctrl.Focus();
        }

        /// <summary>
        ///     Sets the focus on the next element
        /// </summary>
        /// <param name="current"></param>
        /// <param name="next"></param>
        private void goto_next(Control current, Control next)
        {
            if (next != null)
            {
                switch (next)
                {
                    case IntTextBox box:
                        set_focus(box);
                        break;
                    case TextBox box:
                        set_focus(box);
                        break;
                    default:
                        Console.WriteLine("ERROR: unimplemented type in NewCharacterWindow.xaml.cs: {0}", next.GetType ());
                        break;
                }

                if (next.GetType ().ToString () == "IntTextBox")

                next.Focus();
            }
            else
            {
                Close();
            }
        }

        #region key handlers for each textbox

        private void NameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                goto_next(NameBox, InitiativeBox);
            }
        }

        private void InitiativeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                goto_next(InitiativeBox, CABox);
            }
        }

        private void CABox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                goto_next(CABox, MaxHPBox);
            }
        }

        private void MaxHPBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                goto_next(MaxHPBox, HPBox);
            }
        }

        private void HPBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                goto_next(HPBox, null);
            }
        }
        #endregion
    }
}
