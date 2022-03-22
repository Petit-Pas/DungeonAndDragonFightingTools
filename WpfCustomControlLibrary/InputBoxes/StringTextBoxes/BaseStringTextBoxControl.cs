using System;
using System.Windows;
using System.Windows.Data;
using WpfCustomControlLibrary.InputBoxes.BaseTextBoxes;

namespace WpfCustomControlLibrary.InputBoxes.StringTextBoxes
{
    public abstract class BaseStringTextBoxControl : ValidableTextBoxControl
    {
        public BaseStringTextBoxControl()
            : base()
        {
            Initialized += BaseStringBoxControl_Initialized;
        }

        protected virtual void StringProperty_Updated(DependencyPropertyChangedEventArgs a)
        {
        }

        private void BaseStringBoxControl_Initialized(object sender, EventArgs e)
        {
            Binding binding = new Binding("String");
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            binding.Source = this;
            binding.ValidationRules.Clear();
            binding.ValidationRules.Add(this.GetValidationRule());
            this.SetBinding(TextProperty, binding).UpdateSource();
        }

        private static void stringProperty_Updated(DependencyObject o, DependencyPropertyChangedEventArgs a)
        {
            if (o is BaseStringTextBoxControl obj)
                obj.StringProperty_Updated(a);
        }

        public string String
        {
            get { return (string)this.GetValue(StringProperty); }
            set { this.SetValue(StringProperty, value); }
        }
        public static DependencyProperty StringProperty = DependencyProperty.Register(
            "String", typeof(string), typeof(BaseStringTextBoxControl),
            new FrameworkPropertyMetadata("Default", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, stringProperty_Updated));
    }
}
