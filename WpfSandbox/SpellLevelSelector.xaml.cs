using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WpfSandbox
{
    /// <summary>
    /// Interaction logic for SpellLevelSelector.xaml
    /// </summary>
    public partial class SpellLevelSelector : UserControl, INotifyPropertyChanged
    {
        public SpellLevelSelector()
        {
            InitializeComponent();
        }

        public bool Button1
        {
            get => _button1;
            set
            {
                _button1 = value;
            }
        }
        private bool _button1 = false;
        
        public bool Button2
        {
            get => _button2;
            set
            {
                _button2 = value;
            }
        }
        private bool _button2 = true;
        
        public bool Button3
        {
            get => _button3;
            set
            {
                _button3 = value;
            }
        }
        private bool _button3 = false;

        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion INotifyPropertyChanged
    }
}
