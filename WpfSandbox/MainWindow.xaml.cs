using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using BaseToolsLibrary.Memory;
using DDFight;
using DDFight.Commands;
using DnDToolsLibrary.Attacks;
using DnDToolsLibrary.Attacks.AttacksCommands.DamageCommands.DamageResultListQueries;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using DnDToolsLibrary.Status;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using WpfDnDCommandHandlers.AttackQueries.DamageQueries.DamageResultListQueries;
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

            Spell spell = new Spell();

            DamageTemplateList list = new DamageTemplateList()
            {
                new DamageTemplate()
                {
                    Damage = new DiceRoll("2d6+3"),
                    DamageType = DamageTypeEnum.Fire,
                    SituationalDamageModifier = DamageModifierEnum.Halved,
                },
                new DamageTemplate()
                {
                    Damage = new DiceRoll("1d10+8"),
                    DamageType = DamageTypeEnum.Cold,
                },
            };

            spell.HitDamage = list;

            spell.IsAnAttack = false;

            spell.HasSavingThrow = true;
            spell.SavingCharacteristic = CharacteristicsEnum.Intelligence;

            DamageResultListQueryHandler dfghj = new DamageResultListQueryHandler();

            DIConfigurer.ConfigureCore();
            DIConfigurer.ConfigureWpf();
            DIConfigurer.Verify();
            HandlerToUiConfig.Configure();

            Global.Loading = false;

            IFigtherProvider provider = DIContainer.GetImplementation<IFigtherProvider>();
            provider.AddFighter(new PlayableEntity() { DisplayName = "Roger" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Pierre" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Paul" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Jacques" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Michel" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Jhon" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Roger" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Pierre" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Paul" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Jacques" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Michel" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Jhon" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Roger" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Pierre" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Paul" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Jacques" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Michel" });
            provider.AddFighter(new PlayableEntity() { DisplayName = "Jhon" });
            PlayableEntity roger = provider.GetFighterByDisplayName("Roger");
            roger.Characteristics.GetCharacteristic(CharacteristicsEnum.Intelligence).Modifier = 3;
            roger.DamageAffinities.GetAffinity(DamageTypeEnum.Fire).Affinity = DamageAffinityEnum.Resistant;
            roger.CA = 12;

            SavingThrowQuery savingQuery = new SavingThrowQuery(new SavingThrow() { 
                Characteristic = CharacteristicsEnum.Intelligence,
                Difficulty = 15,
                TargetName = "Roger",
            }, "A saving for numerous reasons");
            IMediator mediator = DIContainer.GetImplementation<IMediator>();
            mediator.Execute(savingQuery);



            CastSpellCommand command_spell = new CastSpellCommand(provider.GetFighterByDisplayName("Roger"), spell);
            
            //mediator.Execute(command_spell);
            command_spell.Spell.BaseLevel = 1;
            command_spell.Spell.AmountTargets = 15;
            command_spell.Spell.CanSelectSameTargetTwice = true;
            //mediator.Execute(command_spell);


            DamageResultList damageResultList = list.GetResultList();
            foreach (DamageResult result in damageResultList)
            {
                result.LinkedToSaving = false;
            }
            damageResultList[0].AffinityModifier = DamageAffinityEnum.Resistant;


            DamageResultListQuery command = new DamageResultListQuery(damageResultList, "Because very good reasons");


            //ValidableResponse<GetInputDamageResultListResponse> response = mediator.Execute(command) as ValidableResponse<GetInputDamageResultListResponse>;

            this.DataContext = list;
            InitializeComponent();

            

            //TestControl.AttackResult = test;



            Logger.Init();
            ;
            Button_Click_2(null, null);

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

            IFigtherProvider provider = DIContainer.GetImplementation<IFigtherProvider>();
            provider.AddFighter(target);
            provider.AddFighter(caster);


            target.DamageAffinities.GetAffinity(DamageTypeEnum.Acid).Affinity = DamageAffinityEnum.Weak;
            target.DamageAffinities.GetAffinity(DamageTypeEnum.Force).Affinity = DamageAffinityEnum.Immune;
            caster.DamageAffinities.GetAffinity(DamageTypeEnum.Acid).Affinity = DamageAffinityEnum.Resistant;
            caster.DamageAffinities.GetAffinity(DamageTypeEnum.Force).Affinity = DamageAffinityEnum.Resistant;

            OnHitStatus status = new OnHitStatus()
            {
                Target = target,
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
                },
                DotDamageList = new DotTemplateList()
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