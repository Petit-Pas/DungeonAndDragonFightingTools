using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfCustomControlLibrary.Windows
{
    /// <summary>
    /// Interaction logic for BaseWindowBodyUserControl.xaml
    /// </summary>
    public partial class BaseWindowBodyUserControl : UserControl
    {
        public BaseWindowBodyUserControl()
        {
            InitializeComponent();
        }

        /*public object Content
        {
            get { return (object)this.GetValue(ContentProperty); }
            set { this.SetValue(ContentProperty, value); }
        }
        private static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            nameof(Content),
            typeof(object),
            typeof(BaseWindowBodyUserControl),
            new PropertyMetadata(null));*/
    }
}
