using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace WpfToolsLibrary.ValidationRules.Numeric
{
    public class RangedIntValidationRuleBoundaries : DependencyObject
    {
        public int Max
        {
            get { return (int)this.GetValue(MaxProperty); }
            set { this.SetValue(MaxProperty, value); }
        }
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
            "Max", typeof(int), typeof(RangedIntValidationRuleBoundaries),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public int Min
        {
            get { return (int)this.GetValue(MinProperty); }
            set { this.SetValue(MinProperty, value); }
        }
        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
            "Min", typeof(int), typeof(RangedIntValidationRuleBoundaries),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    }

    [ContentProperty("Boundaries")]
    public class RangedIntValidationRule : ValidationRule
    {
        public RangedIntValidationRule()
        {
        }

        public RangedIntValidationRuleBoundaries Boundaries { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int nb = 0;

            try
            {
                if (((string)value).Length > 0)
                    nb = Int32.Parse((String)value);
            }
            catch (Exception)
            {
                return new ValidationResult(false, $"Please enter a number in the range: {Boundaries.Min}-{Boundaries.Max}.");
            }

            if ((nb < Boundaries.Min) || (nb > Boundaries.Max))
            {
                return new ValidationResult(false,
                  $"{value}: Please enter a number in the range: {Boundaries.Min}-{Boundaries.Max}.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
