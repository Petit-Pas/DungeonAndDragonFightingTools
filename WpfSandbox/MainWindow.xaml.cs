using BaseToolsLibrary.IO;
using BaseToolsLibrary.Memory;
using DDFight;
using DDFight.Commands;
using DnDToolsLibrary.Attacks;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using WpfDnDCustomControlLibrary.Statuses;
using WpfSandbox;
using WpfToolsLibrary.Extensions;
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Global.Loading = false;
            OnHitStatusHandleWindow window = new OnHitStatusHandleWindow(true);

            Character caster = new Character()
            {
                Name = "Caster"
            };
            Character target = new Character()
            {
                Name = "Target"
            };

            OnHitStatus status = new OnHitStatus()
            {
                Affected = target,
                Caster = caster,
                ApplySavingCharacteristic = CharacteristicsEnum.Constitution,
                ApplySavingDifficulty = 14,
                Description = "Each target must succeed on a Wisdom saving throw or be affected by this spell for the duration." +
                                "\r\nAn affected target’s speed is halved, " +
                                "it takes a −2 penalty to AC and Dexterity saving throws, " +
                                "and it can’t use reactions.On its turn, " +
                                "it can use either an action or a bonus action, " +
                                "not both.Regardless of the creature’s abilities or magic items, " +
                                "it can’t make more than one melee or ranged attack during its turn." +
                                "\r\nIf the creature attempts to cast a spell with a casting time of 1 action, " +
                                "roll a d20.On an 11 or higher, " +
                                "the spell doesn’t take effect until the creature’s next turn, " +
                                "and the creature must use its action on that turn to complete the spell.If it can’t, " +
                                "the spell is wasted." +
                                "\r\nA creature affected by this spell makes another Wisdom saving throw at the end of its turn.On a successful save, " +
                                "the effect ends for it.",
                DisplayName = "Slowed",
                OnApplyDamageList = new DamageTemplateList() {
                    Elements = new ObservableCollection<DamageTemplate>()
                    {
                        new DamageTemplate()
                        {
                            Damage = new DiceRoll("1d4+2"),
                            DamageType = DamageTypeEnum.Necrotic,
                            SituationalDamageModifier = DamageModifierEnum.Halved,
                        },
                        new DamageTemplate()
                        {
                            Damage = new DiceRoll("2d5+1"),
                            DamageType = DamageTypeEnum.Force,
                            SituationalDamageModifier = DamageModifierEnum.Canceled,
                        },
                        new DamageTemplate()
                        {
                            Damage = new DiceRoll("3d3+3"),
                            DamageType = DamageTypeEnum.Acid,
                            SituationalDamageModifier = DamageModifierEnum.Normal,
                        },
                    }
                },
                DotDamageList = new DotTemplateList()
                {
                    Elements = new ObservableCollection<DotTemplate>()
                    {
                        new DotTemplate()
                        {
                            Damage = new DiceRoll("2d6"),
                            TriggersStartOfTurn = true,
                            DamageType = DamageTypeEnum.Fire,
                        },
                        new DotTemplate()
                        {
                            Damage = new DiceRoll("1d6+3"),
                            TriggersStartOfTurn = true,
                            DamageType = DamageTypeEnum.Cold,
                        },
                    }
                },
                HasApplyCondition = true,
                Header = "Slowed",
                ToolTip = "Slowed by spell",
                Name = "Slowed",
            };

            window.DataContext = status;

            window.ShowCentered();
        }
    }
}