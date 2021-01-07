using DDFight.Game.Dices.SavingThrow;
using DDFight.Game.Dices.SavingThrow.Display;
using DDFight.Tools.UXShortcuts;
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

namespace DDFight.Game.Aggression.Spells.Display
{
    /// <summary>
    /// Interaction logic for SpellNonAttackCastWindow.xaml
    /// </summary>
    public partial class SpellNonAttackCastWindow : Window, IRollableControl
    {
        public SpellNonAttackCastWindow ()
        {
            InitializeComponent();

            DataContextChanged += SpellNonAttackCastWindow_DataContextChanged;
        }

        private void SpellNonAttackCastWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (data_context.HasSavingThrow)
                TargetListControl.LayoutUpdated += TargetListControl_LayoutUpdated;
        }

        private ObservableCollection<SavingThrow> savings = new ObservableCollection<SavingThrow>();

        private void TargetListControl_LayoutUpdated(object sender, EventArgs e)
        {
            List<FrameworkElement> list = TargetListControl.GetAllChildrenByName("SavingThrowRollableDenseControl");
            if (list.Count == data_context.Targets.Count)
            {
                TargetListControl.LayoutUpdated -= TargetListControl_LayoutUpdated;
                foreach (PlayableEntity target in data_context.Targets)
                {
                    SavingThrow new_one = new SavingThrow
                    {
                        Characteristic = data_context.SavingCharacteristic,
                        Difficulty = data_context.SavingDifficulty,
                        Target = target,
                    };
                    savings.Add(new_one);
                }
                int i = 0;
                foreach (SavingThrowRollableDenseUserControl control in list)
                {
                    control.DataContext = savings.ElementAt(i);
                    i += 1;
                }
            }
        }

        private NonAttackSpellResult data_context
        {
            get => (NonAttackSpellResult)DataContext;
        }

        public void RollControl()
        {
            foreach (DamageTemplate damageTemplate in data_context.HitDamage)
            {
                if (damageTemplate.Damage.LastRoll == 0)
                    damageTemplate.Damage.Roll();
            }
            RollableWindowTool.RollRollableChildren(this);
        }

        private void SpellNonAttackCastWindowControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (RollableWindowTool.IsRollControlPressed(e))
            {
                RollControl();
                e.Handled = true;
            }
        }
    }
}
