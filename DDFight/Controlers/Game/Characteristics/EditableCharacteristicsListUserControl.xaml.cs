using DDFight.Game;
using DDFight.Game.Characteristics;
using DDFight.ValidationRules;
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
using DDFight;
using DDFight.Controlers.InputBoxes;
using System.Threading;

namespace DDFight.Controlers.Game.Characteristics
{
    /// <summary>
    /// Logique d'interaction pour EditableCharacteristicsList.xaml
    /// </summary>
    public partial class EditableCharacteristicsListUserControl : UserControl, IValidable
    {
        /// <summary>
        ///     contains a list of the controls
        /// </summary>
        private List<UserControl> controls = new List<UserControl>();

        private PlayableEntity _dataContext {
            get => (PlayableEntity)this.DataContext;
        }
        public EditableCharacteristicsListUserControl()
        {
            InitializeComponent();
            Loaded += EditableCharacteristicsList_Loaded;

            controls.Add(MasteryBonusBox);
        }

        private void EditableCharacteristicsList_Loaded(object sender, RoutedEventArgs e)
        {
            List<CharacteristicDataContext> items = new List<CharacteristicDataContext>();
            foreach (CharacteristicDataContext dc in _dataContext.Characteristics.CharacteristicsList)
            {
                items.Add(dc);
            }
            CharacteristicsListView.ItemsSource = items;
        }

        private bool hasAlreadyBeenCalled = false;

        public bool IsValid()
        {
            if (!hasAlreadyBeenCalled)
            {
                List<IntTextBox> list = CharacteristicsListView.GetChildrenOfType<IntTextBox>();
                controls.AddRange(list);
                hasAlreadyBeenCalled = true;
            }
            foreach (Control ctrl in controls)
            {
                switch (ctrl)
                {
                    case IValidable _ctrl:
                        if (_ctrl.IsValid() == false)
                        {
                            return false;
                        }
                        break;
                    default:
                        Console.WriteLine("Warning: unimplemented type for IsValid in EditableCharacteristicsListUserControl.xaml.cs: {0}", ctrl.GetType());
                        break;
                }
            }
            return true;
        }
    }
}
