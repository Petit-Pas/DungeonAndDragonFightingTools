using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfCustomControlLibrary.CardControls
{
    public abstract class BaseCardControl : ContentControl
    {
        public BaseCardControl()
            : base()
        {
        }

        public Brush CardColor
        {
            get { return (Brush)this.GetValue(CardColorProperty); }
            set { this.SetValue(CardColorProperty, value); }
        }
        private static readonly DependencyProperty CardColorProperty = DependencyProperty.Register(
                nameof(CardColor),
                typeof(Brush),
                typeof(BaseCardControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["Gray"])
            );

        public Color ShadowColor
        {
            get { return (Color)this.GetValue(ShadowColorProperty); }
            set { this.SetValue(ShadowColorProperty, value); }
        }
        private static readonly DependencyProperty ShadowColorProperty = DependencyProperty.Register(
                nameof(ShadowColor),
                typeof(Color),
                typeof(BaseCardControl),
                new PropertyMetadata(System.Windows.Application.Current.Resources["RawIndigo"])
            );


    }
}
