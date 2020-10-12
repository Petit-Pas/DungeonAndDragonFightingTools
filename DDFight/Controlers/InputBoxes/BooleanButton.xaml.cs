using DDFight.Tools;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DDFight.Controlers.InputBoxes
{
    /// <summary>
    /// Logique d'interaction pour BooleanButton.xaml
    /// </summary>
    public partial class BooleanButton : Button, IEventUnregisterable
    {     
        public string PropertyPath
        {
            get => _propertyPath;
            set {
                _propertyPath = value;
            }
        }
        private string _propertyPath;

        private bool _propertyValue
        {
            get {
                Type t = DataContext.GetType();
                PropertyInfo p = t.GetProperty(PropertyPath); // crash ici
                return (bool)p.GetValue(DataContext, null);
            }
            set
            {
                Type t = DataContext.GetType();
                PropertyInfo p = t.GetProperty(PropertyPath);
                p.SetValue(DataContext, value);
            }
        }

        public BooleanButton() : base()
        {
            InitializeComponent();
            Loaded += BooleanButton_Loaded;
        }

        private void BooleanButton_Loaded(object sender, RoutedEventArgs e)
        {
            INotifyPropertyChanged dc = DataContext as INotifyPropertyChanged;
            dc.PropertyChanged += DataContext_PropertyChanged;
            this.Click += ButtonControl_Click;
            UpdateButtonColors();
        }

        private void UpdateButtonColors()
        {
            if (_propertyValue == true)
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

        private void DataContext_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PropertyPath)
            {
                UpdateButtonColors();
            }
        }

        private void ButtonControl_Click(object sender, RoutedEventArgs e)
        {
            _propertyValue = !_propertyValue;
        }

        public void UnregisterToAll()
        {
            INotifyPropertyChanged dc = DataContext as INotifyPropertyChanged;
            dc.PropertyChanged -= DataContext_PropertyChanged;
            this.Click -= ButtonControl_Click;
        }
    }
}
