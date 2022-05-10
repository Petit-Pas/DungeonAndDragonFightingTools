using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DDFight.Controlers.InputBoxes
{
    /// <summary>
    /// Logique d'interaction pour BooleanButton.xaml
    /// </summary>
    public partial class BooleanButton : Button
    {
        public bool Boolean
        {
            get => (bool)this.GetValue(BooleanProperty);
            set
            {
                this.SetValue(BooleanProperty, value);
            }
        }
        public static readonly DependencyProperty BooleanProperty = DependencyProperty.Register(
            "Boolean", typeof(bool), typeof(BooleanButton),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBooleanChanged));

        public static void OnBooleanChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BooleanButton current = d as BooleanButton;
            current.UpdateButtonColors();
        }

        public BooleanButton() : base()
        {
            InitializeComponent();
            Loaded += BooleanButton_Loaded;
        }

        private void BooleanButton_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateButtonColors();
        }

        public void UpdateButtonColors()
        {
            if (Boolean == true)
            {
                Foreground = (Brush)Application.Current.Resources["LightestGray"];
                Background = (Brush)Application.Current.Resources["Light"];
            }
            else
            {
                Background = (Brush)Application.Current.Resources["LightestGray"];
                Foreground = (Brush)Application.Current.Resources["Light"];
            }
        }
    }
}
