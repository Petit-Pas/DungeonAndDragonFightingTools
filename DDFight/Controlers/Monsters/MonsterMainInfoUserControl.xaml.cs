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

namespace DDFight.Controlers.Monsters
{
    /// <summary>
    /// Logique d'interaction pour MonsterMainInfoUserControl.xaml
    /// </summary>
    public partial class MonsterMainInfoUserControl : UserControl, IValidable
    {
        /// <summary>
        ///     contains a list of the controls
        /// </summary>
        private List<UserControl> controls = new List<UserControl>();

        public MonsterMainInfoUserControl()
        {
            InitializeComponent();

            controls.Add(NameBoxUserControl);
            controls.Add(LevelBoxUserControl);
            controls.Add(CABoxUserControl);
            controls.Add(MaxHPBoxUserControl);
            controls.Add(HPBoxUserControl);
            controls.Add(CharacteristicsUserControl);
        }

        public bool IsValid()
        {
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
                        Console.WriteLine("Warning: unimplemented type for IsValid in CharacterMainInfoUserControl.xaml.cs: {0}", ctrl.GetType());
                        break;
                }
            }
            return true;
        }
    }
}
