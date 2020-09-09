using DDFight.Game.Characteristics;
using DDFight.Windows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Game.Status.Display
{
    /// <summary>
    /// Logique d'interaction pour EditCustomVerboseStatusWindow.xaml
    /// </summary>
    public partial class OnHitStatusEditWindow : Window
    {
        public OnHitStatus data_context
        {
            get => (OnHitStatus)DataContext;
        } 

        public OnHitStatusEditWindow()
        {
            InitializeComponent();
            
            Loaded += EditCustomVerboseStatusWindow_Loaded;
        }

        /// <summary>
        ///     used to avoid triggering certain event when forcing the SelectedIndex in ComboBoxes
        /// </summary>
        private bool is_initializing = false;

        private void EditCustomVerboseStatusWindow_Loaded(object sender, RoutedEventArgs e)
        {
            is_initializing = true;
            HeaderBox.StringBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            HeaderBox.StringBox.TextChanged += StringBox_TextChanged;
            List<string> DCList = new List<string>();
            DCList.Add("None");
            foreach (var charac in Enum.GetValues(typeof(CharacteristicsEnum)))
                DCList.Add(charac.ToString());
            ApplySavingCharacteristicControler.ItemsSource = DCList;
            ApplySavingCharacteristicControler.SelectedIndex = 0;
            if (data_context.HasApplyCondition)
            {
                is_initializing = true;
                int i = 0;
                foreach(CharacteristicsEnum charac in Enum.GetValues(typeof(CharacteristicsEnum)))
                {
                    i += 1;
                    if (charac == data_context.ApplySavingCharacteristic)
                    {
                        ApplySavingCharacteristicControler.SelectedIndex = i;
                        break;
                    }
                }
            }
            is_initializing = false;
        }

        private void StringBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.AreAllChildrenValid())
                ErrorMessage.Visibility = Visibility.Collapsed;
        }

        private bool planned_close = false;

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.AreAllChildrenValid())
            {
                data_context.Validated = true;
                planned_close = true;
                this.Close();
            }
            else
            {
                ErrorMessage.Visibility = Visibility.Visible;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (planned_close == false)
            {
                AskYesNoWindow window = new AskYesNoWindow();
                AskYesNoDataContext dc = new AskYesNoDataContext { Message = "Are you sure you want to discard all your changes?" };

                window.DataContext = dc;
                window.ShowCentered();
                window.Owner = this;

                if (dc.Yes == false)
                    e.Cancel = true;
            }
        }

        private void ApplySavingCharacteristicControler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!is_initializing)
            {
                if (ApplySavingCharacteristicControler.SelectedIndex == 0)
                {
                    data_context.HasApplyCondition = false;
                }
                else
                {
                    data_context.HasApplyCondition = true;
                    data_context.ApplySavingCharacteristic = (CharacteristicsEnum)Enum.Parse(typeof(CharacteristicsEnum), (string)ApplySavingCharacteristicControler.SelectedItem);
                }
            }
        }
    }
}
