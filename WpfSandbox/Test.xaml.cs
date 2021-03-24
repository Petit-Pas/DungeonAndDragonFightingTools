using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace WpfSandbox
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : UserControl, INotifyPropertyChanged
    {
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion INotifyPropertyChanged

        public int Integer
        {
            get => _integer;
            set { _integer = value + 1; NotifyPropertyChanged(); }
        }
        private int _integer = 1;


        public Test()
        {
            this.DataContext = this;
            InitializeComponent();
        }
    }
}
