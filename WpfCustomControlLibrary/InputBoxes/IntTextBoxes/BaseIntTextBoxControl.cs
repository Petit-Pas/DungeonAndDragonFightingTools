using System;
using System.Windows;
using System.Windows.Data;
using WpfCustomControlLibrary.InputBoxes.BaseTextBoxes;

namespace WpfCustomControlLibrary.InputBoxes.IntTextBoxes
{
    public abstract class BaseIntTextBoxControl : ValidableTextBoxControl
    {
        public BaseIntTextBoxControl() : base()
        {
            Initialized += BaseIntBoxControl_Initialized;
        }

        private void BaseIntBoxControl_Initialized(object sender, EventArgs e)
        {
            Binding binding = new Binding("Integer");
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            binding.Source = this;
            binding.ValidationRules.Clear();
            binding.ValidationRules.Add(this.GetValidationRule());
            this.SetBinding(TextProperty, binding).UpdateSource();
        }

        protected virtual void IntegerProperty_Updated(DependencyPropertyChangedEventArgs a)
        {
        }

        private static void integerProperty_Updated(DependencyObject o, DependencyPropertyChangedEventArgs a)
        {
            if (o is BaseIntTextBoxControl obj)
                obj.IntegerProperty_Updated(a);
        }

        public int Integer
        {
            get { return (int)this.GetValue(IntegerProperty); }
            set { this.SetValue(IntegerProperty, value); }
        }
        public static readonly DependencyProperty IntegerProperty = DependencyProperty.Register(
            "Integer", typeof(int), typeof(BaseIntTextBoxControl),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, integerProperty_Updated));
    }
}
