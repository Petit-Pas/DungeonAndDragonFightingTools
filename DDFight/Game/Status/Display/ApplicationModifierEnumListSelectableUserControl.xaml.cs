using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DDFight.Game.Status.Display
{
    /// <summary>
    /// Logique d'interaction pour ApplicationModifierEnumListSelectableUserControl.xaml
    /// </summary>
    public partial class ApplicationModifierEnumListSelectableUserControl : UserControl
    {
        public ApplicationModifierEnumListSelectableUserControl()
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
                typeof(ApplicationModifierEnumListSelectableUserControl),
                new FrameworkPropertyMetadata(PropertyPath_PropertyChanged));

        /// <summary>
        ///     Event handler for 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        protected static void PropertyPath_PropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var ctl = d as ApplicationModifierEnumListSelectableUserControl;

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
