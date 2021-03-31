using System.Windows.Controls;
using System.Windows;
using DnDToolsLibrary.Status;

namespace DDFight.Game.Status.Display
{
    /// <summary>
    /// Interaction logic for OnHitStatusListPreviewUserControl.xaml
    /// </summary>
    public partial class OnHitStatusListPreviewUserControl : UserControl
    {
        public OnHitStatusListPreviewUserControl()
        {
            DataContextChanged += OnHitStatusListPreviewUserControl_DataContextChanged;
            InitializeComponent();
        }

        private OnHitStatusList data_context
        {
            get => DataContext as OnHitStatusList;
        }

        private void OnHitStatusListPreviewUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            if (data_context?.Elements.Count != 0)
                Visibility = Visibility.Visible;
        }
    }
}
