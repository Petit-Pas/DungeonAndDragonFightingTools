using DDFight.Game;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace DDFight.Windows
{
    /// <summary>
    /// Logique d'interaction pour SelectPlayableEntityWindow.xaml
    /// </summary>
    public partial class SelectPlayableEntityWindow : Window
    {
        private ObservableCollection<PlayableEntity> data_context
        {
            get => (ObservableCollection<PlayableEntity>)DataContext;
        }


        public SelectPlayableEntityWindow()
        {
            InitializeComponent();

            Loaded += SelectPlayableEntityWindow_Loaded;
        }

        private void SelectPlayableEntityWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CharacterListControl.ItemsSource = data_context;
            FilterTextBox.Text = filterPlaceHolder;
        }

        public PlayableEntity SelectedCharacter = null;

        private void CharacterList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CharacterListControl.SelectedIndex != -1)
            {
                SelectedCharacter = (PlayableEntity)CharacterListControl.SelectedItem;
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private string filterPlaceHolder = "Filter...";

        private void FilterTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            CharacterListControl.FilterPlayableEntityListBox(FilterTextBox.Text);

        }

        private void FilterTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilterTextBox.Text))
                FilterTextBox.Text = filterPlaceHolder;
        }

        private void FilterTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FilterTextBox.Text == filterPlaceHolder)
                FilterTextBox.Text = "";
        }
    }
}
