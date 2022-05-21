using System.ComponentModel;
using System.Dynamic;
using System.Windows;
using DnDToolsLibrary.Status;
using WpfCustomControlLibrary.ModalWindows;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace WpfDnDCustomControlLibrary.Statuses
{
    /// <summary>
    /// Logique d'interaction pour StatusReferenceEditWindow.xaml
    /// </summary>
    public partial class StatusReferenceEditWindow : Window, IValidableWindow
    {
        public StatusReferenceEditWindow()
        {
            DataContextChanged += OnDataContextChanged;
            InitializeComponent();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is INotifyPropertyChanged oldObj)
            {
                oldObj.PropertyChanged -= DataContextOnPropertyChanged;
            }

            if (e.NewValue is INotifyPropertyChanged newObj)
            {
                newObj.PropertyChanged += DataContextOnPropertyChanged;
            }
        }

        private void DataContextOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            ValidateButton.IsEnabled = false;
            if (this.AreAllChildrenValid())
            {
                ValidateButton.IsEnabled = true;
            }
        }

        public StatusReference _dataContext => DataContext as StatusReference;

        public bool Validated { get; set; }

        private bool planned_close = false;
        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.AreAllChildrenValid())
            {
                this.Validated = true;
                planned_close = true;
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (planned_close == false)
            {
                var window = new YesNoWindow()
                {
                    Text = "Are you sure you want to discard all your changes?"
                };

                window.Owner = this;
                window.ShowCentered();

                if (window.Validated == false)
                    e.Cancel = true;
            }
        }
    }
}
