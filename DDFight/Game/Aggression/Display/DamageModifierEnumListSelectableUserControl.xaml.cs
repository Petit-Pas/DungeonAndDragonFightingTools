using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DDFight.Game.Aggression.Display
{
    /// <summary>
    /// Interaction logic for DamageModifierEnumListSelectableUserControl.xaml
    /// </summary>
    public partial class DamageModifierEnumListSelectableUserControl : UserControl
    {
        public DamageModifierEnumListSelectableUserControl()
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
                typeof(DamageModifierEnumListSelectableUserControl),
                new FrameworkPropertyMetadata(PropertyPath_PropertyChanged));

        /// <summary>
        ///     Event handler for 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        protected static void PropertyPath_PropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var ctl = d as DamageModifierEnumListSelectableUserControl;

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
