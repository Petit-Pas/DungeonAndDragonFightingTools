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

namespace DDFight.Controlers.Game.DamageAffinity
{
    /// <summary>
    /// Interaction logic for DamageAffinityDropdownListUserControl.xaml
    /// </summary>
    public partial class DamageAffinityDropdownListUserControl : UserControl
    {
        public DamageAffinityDropdownListUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     The Binded Property (on Path Value)
        /// </summary>
        public String PropertyPath
        {
            get { return (String)GetValue(PropertyPathProperty); }
            set { SetValue(PropertyPathProperty, value); }
        }

        /// <summary>
        ///     DependencyProperty for the Binded Property
        /// </summary>
        public static readonly DependencyProperty PropertyPathProperty =
            DependencyProperty.Register(nameof(PropertyPath), typeof(String),
                typeof(DamageAffinityDropdownListUserControl),
                new FrameworkPropertyMetadata(PropertyPath_PropertyChanged));

        /// <summary>
        ///     Event handler for 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        protected static void PropertyPath_PropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var ctl = d as DamageAffinityDropdownListUserControl;

            Binding binding = new Binding(ctl.PropertyPath)
            {
                //  Optional. With this, the bound property will be updated and validation 
                //  will be applied on every keystroke. 
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            ctl._ComboBox.SetBinding(ComboBox.SelectedItemProperty, binding);
        }
    }
}
