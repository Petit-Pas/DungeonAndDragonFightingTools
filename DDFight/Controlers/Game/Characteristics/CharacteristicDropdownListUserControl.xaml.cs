using DDFight.ValidationRules;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DDFight.Controlers.Game.Characteristics
{
    /// <summary>
    /// Interaction logic for CharacteristicDropdownListUserControl.xaml
    /// </summary>
    public partial class CharacteristicDropdownListUserControl : UserControl, IValidable
    {
        public CharacteristicDropdownListUserControl()
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
                typeof(CharacteristicDropdownListUserControl),
                new FrameworkPropertyMetadata(PropertyPath_PropertyChanged));

        protected static void PropertyPath_PropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var ctl = d as CharacteristicDropdownListUserControl;

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
