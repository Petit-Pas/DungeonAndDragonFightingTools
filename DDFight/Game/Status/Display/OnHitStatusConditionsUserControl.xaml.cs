using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Game.Status.Display
{
    /// <summary>
    /// Interaction logic for OnHitStatusConditionsUserControl.xaml
    /// </summary>
    public partial class OnHitStatusConditionsUserControl : UserControl
    {
        public OnHitStatus data_context
        {
            get => (OnHitStatus)DataContext;
        }

        public OnHitStatusConditionsUserControl()
        {
            InitializeComponent();

            Loaded += OnHitStatusConditionsUserControl_Loaded;
        }

        private void OnHitStatusConditionsUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            is_initializing = true;
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
                foreach (CharacteristicsEnum charac in Enum.GetValues(typeof(CharacteristicsEnum)))
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

        private bool is_initializing = false;

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
