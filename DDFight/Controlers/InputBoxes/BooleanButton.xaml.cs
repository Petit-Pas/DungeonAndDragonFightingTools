using DDFight.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DDFight.Controlers.InputBoxes
{
    /// <summary>
    /// Logique d'interaction pour BooleanButton.xaml
    /// </summary>
    public partial class BooleanButton : Button
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
                PropertyInfo p = t.GetProperty(PropertyPath);
                return (bool)p.GetValue(DataContext, null);
            }
            set
            {
                Type t = DataContext.GetType();
                PropertyInfo p = t.GetProperty(PropertyPath);
                p.SetValue(DataContext, value);
            }
        }

        public BooleanButton()
        {
            InitializeComponent();
            Loaded += BooleanButton_Loaded;
        }



        private void BooleanButton_Loaded(object sender, RoutedEventArgs e)
        {
            INotifyPropertyChanged dc = DataContext as INotifyPropertyChanged;
            dc.PropertyChanged += DataContext_PropertyChanged;
            this.Click += ButtonControl_Click;
        }

        private void DataContext_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PropertyPath)
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
        }

        private void ButtonControl_Click(object sender, RoutedEventArgs e)
        {
            _propertyValue = !_propertyValue;
        }
    }
}
