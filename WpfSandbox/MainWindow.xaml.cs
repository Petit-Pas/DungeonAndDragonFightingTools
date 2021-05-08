using BaseToolsLibrary.IO;
using DDFight;
using DDFight.Commands;
using DnDToolsLibrary.Attacks;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using WpfSandbox;
using WpfToolsLibrary.Navigation;

namespace BindValidation
{

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
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
        #endregion INotifyPropertyChanged

        public int Integer
        {
            get => _integer;
            set { _integer = value;  NotifyPropertyChanged(); }
        }
        private int _integer = 1;

        public MainWindow()
        {

            DamageTemplateList list = new DamageTemplateList()
            {
                Elements = new ObservableCollection<DamageTemplate>() {
                    new DamageTemplate()
                    {
                        Damage = new DiceRoll("2d6+3"),
                        DamageType = DamageTypeEnum.Fire,
                    },
                    new DamageTemplate()
                    {
                        Damage = new DiceRoll("1d10+8"),
                        DamageType = DamageTypeEnum.Cold,
                    },
                },
            };

            this.DataContext = list;
            InitializeComponent();

            

            //TestControl.AttackResult = test;



            Logger.Init();
            ;


        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonCardControl_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.RollRollableChildren();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine();
        }

        private void ShadowButtonControl_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}