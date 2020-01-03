using DDFight.Game;
using DDFight.Resources;
using DDFight.ValidationRules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for EditMonsterWindow.xaml
    /// </summary>
    public partial class EditMonsterWindow : Window
    {
        /// <summary>
        ///     contains a list of the parameters IN THE RIGHT ORDER
        /// </summary>
        private List<UserControl> controls = new List<UserControl>();

        private MonsterDataContext data_context { get => (MonsterDataContext)DataContext; }

        /// <summary>
        ///     Ctor
        /// </summary>
        public EditMonsterWindow()
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
                        Console.WriteLine("Warning: unimplemented type for IsValid in NewMonsterWindow.xaml.cs: {0}", ctrl.GetType());
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
    }
}
