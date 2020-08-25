using DDFight.ValidationRules;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DDFight.Controlers.Game.Characteristics
{
    /// <summary>
    /// Interaction logic for CharacteristicDropdownListUserControl.xaml
    /// </summary>
    public partial class CharacteristicsListSelectableUserControl : UserControl, IValidable
    {
        public CharacteristicsListSelectableUserControl()
        {
            InitializeComponent();
        }

        public string PropertyPath
        {
            get { return (String)GetValue(PropertyPathProperty); }
            set { SetValue(PropertyPathProperty, value); }
        }

        public static readonly DependencyProperty PropertyPathProperty =
            DependencyProperty.Register(nameof(PropertyPath), typeof(String),
                typeof(CharacteristicsListSelectableUserControl),
                new FrameworkPropertyMetadata(PropertyPath_PropertyChanged));

        protected static void PropertyPath_PropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var ctl = d as CharacteristicsListSelectableUserControl;

            Binding binding = new Binding(ctl.PropertyPath)
            {
                //  Optional. With this, the bound property will be updated and validation 
                //  will be applied on every keystroke. 
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            ctl._ComboBox.SetBinding(ComboBox.SelectedItemProperty, binding);
        }

        public bool IsValid()
        {
            if (_ComboBox.SelectedIndex != -1)
                return true;
            return false;
        }
    }
}
