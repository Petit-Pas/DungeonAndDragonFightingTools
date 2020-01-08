using DDFight.Controlers.Game.Attacks;
using DDFight.Game.Aggression.Attacks;
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
using System.Windows.Shapes;

namespace DDFight.Windows
{
    /// <summary>
    /// Interaction logic for EditAAttackTemplate.xaml
    /// </summary>
    public partial class EditAAttackTemplateWindow : Window
    {
        private List<UserControl> controls = new List<UserControl>();

        public EditAAttackTemplateWindow()
        {
            InitializeComponent();

            Loaded += EditAAttackTemplateWindow_Loaded;
        }

        private void EditAAttackTemplateWindow_Loaded(object sender, RoutedEventArgs e)
        {
            switch (DataContext)
            {
                case HitAttackTemplate template:
                    EditableHitAttackTemplateUserControl control = new EditableHitAttackTemplateUserControl { DataContext = this.DataContext };
                    controls.Add(control);
                    ContentGrid.Children.Add(control);
                    break;
                default:
                    Console.WriteLine("WARNING: unknown type in EditAAtackTemplateWindow.xaml.cs");
                    break;
            }
            Console.WriteLine(DataContext.GetType().ToString());
            //ContentGrid.Children.Add();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            switch (DataContext)
            {
                case HitAttackTemplate template:

                    break;
                default:
                    Console.WriteLine("WARNING: unknown type in EditAAtackTemplateWindow.xaml.cs");
                    break;
            }
        }
    }
}
