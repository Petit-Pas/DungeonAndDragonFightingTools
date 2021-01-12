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

namespace DDFight.Game.Aggression.Spells.Display
{
    /// <summary>
    ///     Interaction logic for SpellNonAttackCastWindow.xaml
    /// </summary>
    public partial class SpellNonAttackCastWindow : Window, IRollableControl
    {
        public SpellNonAttackCastWindow ()
        {
            InitializeComponent();

            KeyUp += SpellNonAttackCastWindow_KeyUp;
            DataContextChanged += SpellNonAttackCastWindow_DataContextChanged;
        }

        private void SpellNonAttackCastWindow_KeyUp(object sender, KeyEventArgs e)
        {
            refresh_CastButton();
        }

        private void SpellNonAttackCastWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (data_context.HasSavingThrow)
            {
                TargetListControl.LayoutUpdated += TargetListControl_LayoutUpdated;
                CastButtonControl.IsEnabled = false;
            }
            if (data_context.HitDamage.Count != 0)
                CastButtonControl.IsEnabled = false;
        }

        private List<SavingThrow> savings = new List<SavingThrow>();

        private void TargetListControl_LayoutUpdated(object sender, EventArgs e)
        {
            List<FrameworkElement> list = TargetListControl.GetAllChildrenByName("SavingThrowRollableDenseControl");
            savings.Clear();
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

        private void refresh_CastButton()
        {
            CastButtonControl.IsEnabled = false;
            if (this.AreAllChildrenValid())
            {
                if (this.IsFullyRolled())
                    CastButtonControl.IsEnabled = true;
            }
        }

        private NonAttackSpellResult data_context
        {
            get => (NonAttackSpellResult)DataContext;
        }

        public void RollControl()
        {
            RollableWindowTool.RollRollableChildren(this);
        }

        public bool IsFullyRolled()
        {
            return RollableWindowTool.AreAllRollableChildrenRolled(this);
        }

        private void SpellNonAttackCastWindowControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (RollableWindowTool.IsRollControlPressed(e))
            {
                RollControl();
                e.Handled = true;
            }
        }

        private void CastButton_Click(object sender, RoutedEventArgs e)
        {
            data_context.Cast(savings);
            this.Close();
        }
    }
}
