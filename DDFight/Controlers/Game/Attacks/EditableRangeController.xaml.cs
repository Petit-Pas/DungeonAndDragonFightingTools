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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DDFight.Controlers.Game.Attacks
{
    /// <summary>
    /// Interaction logic for EditableRangeController.xaml
    /// </summary>
    public partial class EditableRangeController : UserControl
    {
        public EditableRangeController()
        {
            InitializeComponent();
            Loaded += EditableRangeController_Loaded;
            DataContextChanged += EditableRangeController_DataContextChanged;
        }

        private void EditableRangeController_Loaded(object sender, RoutedEventArgs e)
        {
            AAttackTemplate tmp = DataContext as AAttackTemplate;
            //tmp.PropertyChanged += Tmp_PropertyChanged;
            Console.WriteLine("loaded event handler");
        }

        private void EditableRangeController_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("DataContextChanged");
        }

        private void Tmp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Console.WriteLine("a property changed: " + e.PropertyName + " and value " + ((AAttackTemplate)sender).Range);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AAttackTemplate tmp = DataContext as AAttackTemplate;

            Console.WriteLine(tmp.Range.ToString());
            Console.WriteLine("close: " + tmp.IsCloseRanged); 
            Console.WriteLine(CloseRangedCheckBoxControl.IsChecked);
            Console.WriteLine("range: " + tmp.IsLongRanged);
            Console.WriteLine(LongRangedCheckBoxControl.IsChecked);
        }
    }
}
