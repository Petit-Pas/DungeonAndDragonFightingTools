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
    /// Interaction logic for NewCharacterWindow.xaml
    /// </summary>
    public partial class NewCharacterWindow : Window
    {
        public NewCharacterWindow()
        {
            InitializeComponent();
            NameBox.Focus();
        }

        /// <summary>
        ///     Checks that the given textob isn't empty
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        private bool is_valid (TextBox box)
        {
            return box.Text != null && box.Text != string.Empty;
        }

        /// <summary>
        ///     Sets the focus on the next element of the list if the current is valid
        /// </summary>
        /// <param name="current"></param>
        /// <param name="next"></param>
        private void goto_next (TextBox current, TextBox next)
        {
            if (is_valid (current))
            {
                if (next != null)
                {
                    next.Focus();
                }
                else
                {
                    Close();
                }
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
