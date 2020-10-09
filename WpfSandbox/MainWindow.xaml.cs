// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace BindValidation
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public bool Test 
        {
            get => _test;
            set
            {
                _test = value;
                NotifyPropertyChanged();
            }
        }
        private bool _test = true;


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

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;

            this.DataContext = this;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Test)
            {
                TextBlockControl.Background = new SolidColorBrush(Colors.Black);
                TextBlockControl.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                TextBlockControl.Background = new SolidColorBrush(Colors.White);
                TextBlockControl.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
    }
}