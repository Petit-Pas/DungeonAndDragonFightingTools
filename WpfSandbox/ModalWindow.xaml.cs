using DnDToolsLibrary.Attacks;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WpfSandbox
{
    /// <summary>
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window, INotifyPropertyChanged
    {
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public ModalWindow()
        {
            InitializeComponent();
            FightersList.Instance.AddElement(new PlayableEntity { DisplayName = "number 1", CA = 11 });
            FightersList.Instance.AddElement(new PlayableEntity { DisplayName = "number 2", CA = 12 });

        }

        public HitAttackResult Result
        {
            get => result;
            set
            {
                result = value;
                NotifyPropertyChanged();
            }
        }
        private HitAttackResult result = new HitAttackResult()
        {
            DamageList = new DamageResultList()
            {
                Elements = new ObservableCollection<DamageResult>() {
                    new DamageResult()
                    {
                        Damage = new DiceRoll("2d6+3"),
                        DamageType = DamageTypeEnum.Fire,
                    },
                    new DamageResult()
                    {
                        Damage = new DiceRoll("1d10+8"),
                        DamageType = DamageTypeEnum.Cold,
                    },
                },
            },
            RollResult = new AttackRollResult()
            {
                BaseRollModifier = 2,
            },
            Owner = new PlayableEntity()
            {
                DisplayName = "Attacker",
            }
        };
    }
}
