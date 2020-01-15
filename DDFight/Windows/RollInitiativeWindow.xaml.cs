using DDFight.Game;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace DDFight.Windows
{
    /// <summary>
    /// Interaction logic for RollInitiativeWindow.xaml
    /// </summary>
    public partial class RollInitiativeWindow : Window
    {
        public RollInitiativeWindow()
        {
            InitializeComponent();
            Loaded += RollInitiativeWindow_Loaded;
        }

        private void RollInitiativeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CharactersItemsControl.ItemsSource = (ObservableCollection<PlayableEntity>)DataContext;
        }
    }
}
