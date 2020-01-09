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
        
        public MonsterMainInfoUserControl()
        {
            InitializeComponent();
        }

        public bool IsValid()
        {
            return this.AreAllChildrenValid();
        }
    }
}
