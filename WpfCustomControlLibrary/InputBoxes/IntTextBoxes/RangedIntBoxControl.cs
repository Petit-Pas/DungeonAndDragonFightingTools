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
    public class RangedIntBoxControl : BaseIntTextBoxControl
    {
        private readonly RangedIntValidationRule _validationRule = new RangedIntValidationRule() {
            Boundaries = new RangedIntValidationRuleBoundaries(),
        };

        public RangedIntBoxControl()
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
        }

        public int Max
        {
            get { return (int)this.GetValue(MaxProperty); }
            set { this.SetValue(MaxProperty, value); }
        }
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
            "Max", typeof(int), typeof(RangedIntBoxControl),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public int Min
        {
            get { return (int)this.GetValue(MinProperty); }
            set { this.SetValue(MinProperty, value); }
        }
        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
            "Min", typeof(int), typeof(RangedIntBoxControl),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public override ValidationRule GetValidationRule()
        {
            return _validationRule;
        }
    }
}
