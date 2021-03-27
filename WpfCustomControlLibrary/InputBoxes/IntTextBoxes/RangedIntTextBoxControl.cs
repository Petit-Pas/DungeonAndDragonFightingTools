using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.ValidationRules.Numeric;

namespace WpfCustomControlLibrary.InputBoxes.IntTextBoxes
{
    public class RangedIntTextBoxControl : BaseIntTextBoxControl
    {
        private readonly RangedIntValidationRule _validationRule = new RangedIntValidationRule() {
            Boundaries = new RangedIntValidationRuleBoundaries(),
            ValidatesOnTargetUpdated = true,
        };

        public RangedIntTextBoxControl()
            : base()
        {
            Initialized += RangedIntBoxControl_Initialized;
        }

        private void RangedIntBoxControl_Initialized(object sender, EventArgs e)
        {
            // mirrors the Min and Max property to the boundaries of the ValidationRule
            Binding minBinding = new Binding("Min");
            Binding maxBinding = new Binding("Max");
            minBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            maxBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            minBinding.Source = this;
            maxBinding.Source = this;
            _validationRule.Boundaries.SetBinding(RangedIntValidationRuleBoundaries.MinProperty, minBinding);
            _validationRule.Boundaries.SetBinding(RangedIntValidationRuleBoundaries.MaxProperty, maxBinding);

            refresh_validation();
        }

        private void refresh_validation()
        {
            GetBindingExpression(TextBox.TextProperty)?.ValidateWithoutUpdate();
        }

        private static void boundary_updated(DependencyObject o, DependencyPropertyChangedEventArgs a)
        {
            if (o is RangedIntTextBoxControl control)
                control.refresh_validation();
        }

        public int Max
        {
            get { return (int)this.GetValue(MaxProperty); }
            set { this.SetValue(MaxProperty, value); }
        }
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
            "Max", typeof(int), typeof(RangedIntTextBoxControl),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, boundary_updated));

        public int Min
        {
            get { return (int)this.GetValue(MinProperty); }
            set { this.SetValue(MinProperty, value); }
        }
        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
            "Min", typeof(int), typeof(RangedIntTextBoxControl),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, boundary_updated));

        public override ValidationRule GetValidationRule()
        {
            return _validationRule;
        }
    }
}
