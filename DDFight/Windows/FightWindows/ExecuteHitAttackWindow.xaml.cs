using DDFight.Game;
using DDFight.Game.Aggression;
using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Dices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Interaction logic for ExecuteHitAttackWindow.xaml
    /// </summary>
    public partial class ExecuteHitAttackWindow : Window
    {
        public HitAttackTemplate data_context
        {
            get => (HitAttackTemplate)DataContext;
        }

        public ExecuteHitAttackWindow()
        {
            InitializeComponent();
            Loaded += ExecuteHitAttackWindow_Loaded;
        }

        private List<HitAttackResult> attacks = new List<HitAttackResult>();

        private void ExecuteHitAttackWindow_Loaded(object sender, RoutedEventArgs e)
        {
            attacks = data_context.GetResultTemplate();
            AttackList.ItemsSource = attacks;
            AttackList.LayoutUpdated += AttackList_LayoutUpdated;
        }

        private List<FrameworkElement> rollButtons = new List<FrameworkElement>();
        private List<FrameworkElement> validateButtons = new List<FrameworkElement>();
        private List<FrameworkElement> targetBoxes = new List<FrameworkElement>();
        private List<FrameworkElement> groupBoxes = new List<FrameworkElement>();
        private List<FrameworkElement> errorBoxes = new List<FrameworkElement>();

        private void AttackList_LayoutUpdated(object sender, EventArgs e)
        {
            targetBoxes = this.GetAllChildrenByName("HitAttackTargetComboControl");
            foreach (ComboBox box in targetBoxes)
            {
                box.ItemsSource = Global.Context.FightContext.FightersList.Fighters;
            }
            AttackList.LayoutUpdated -= AttackList_LayoutUpdated;

            rollButtons = this.GetAllChildrenByName("RollButtonControl");
            foreach (Button btn in rollButtons)
            {
                btn.IsEnabled = false;
            }
            if (rollButtons.Count != 0)
                ((Button)rollButtons.ElementAt(0)).IsEnabled = true;

            validateButtons = this.GetAllChildrenByName("ValidateButtonControl");
            foreach(Button btn in validateButtons)
            {
                btn.IsEnabled = false;
            }

            groupBoxes = this.GetAllChildrenByName("AttackGroupBoxControl");
            errorBoxes = this.GetAllChildrenByName("ErrorTextblockControl");
        }

        private void RollButtonControl_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i != rollButtons.Count; i += 1)
            {
                if (((Button)rollButtons[i]).IsEnabled == true)
                {
                    if (((ComboBox)targetBoxes[i]).SelectedIndex != -1)
                    {
                        // a character has well been selected
                        if (groupBoxes[i].AreAllChildrenValid())
                        {
                            // there is no error in input boxes

                            // computes unrolled rolls
                            if (attacks[i].HitRoll == 0)
                            {
                                attacks[i].HitRoll = (uint)DiceRoll.Roll(
                                    "1d20",
                                    attacks[i].SituationalAdvantageModifiers.SituationalAdvantage,
                                    attacks[i].SituationalAdvantageModifiers.SituationalDisadvantage);
                            }
                            foreach (DamageTemplate dmg in attacks[i].DamageList)
                            {
                                if (dmg.Damage.LastResult == 0)
                                    dmg.Damage.Roll(attacks[i].HitRoll >= 20 ? true : false);
                            }
                            // Enables the validate button
                            ((Button)rollButtons[i]).IsEnabled = false;
                            ((Button)validateButtons[i]).IsEnabled = true;
                            ((TextBlock)errorBoxes[i]).Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            ((TextBlock)errorBoxes[i]).Text = "One of the input boxes is badly filled";
                            ((TextBlock)errorBoxes[i]).Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        ((TextBlock)errorBoxes[i]).Text = "Please select a target first";
                        ((TextBlock)errorBoxes[i]).Visibility = Visibility.Visible;
                    }
                    return;
                }
            }
        }

        private void QuitButtonControl_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void triggerAttack(int attack_index)
        {
            attacks[attack_index].Target = (PlayableEntity)((ComboBox)targetBoxes[attack_index]).SelectedItem;
            attacks[attack_index].Target.GetAttacked(attacks[attack_index], data_context.Owner);
        }

        private void ValidateButtonControl_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i != validateButtons.Count; i += 1)
            {
                if (validateButtons[i].IsEnabled == true)
                {
                    if (((ComboBox)targetBoxes[i]).SelectedIndex != -1)
                    {
                        // a character has well been selected
                        if (groupBoxes[i].AreAllChildrenValid())
                        {
                            // there is no error in input boxes

                            triggerAttack(i);

                            // Enables next Attack control
                            ((Button)validateButtons[i]).IsEnabled = false;
                            if (i + 1 != rollButtons.Count)
                            {
                                ((Button)rollButtons[i + 1]).IsEnabled = true;
                            }
                            else
                            {
                                this.QuitButtonControl.Visibility = Visibility.Visible;
                                this.AttackScrollViewControl.ScrollToEnd();
                            }
                            ((TextBlock)errorBoxes[i]).Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            ((TextBlock)errorBoxes[i]).Text = "One of the input boxes is badly filled";
                            ((TextBlock)errorBoxes[i]).Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        ((TextBlock)errorBoxes[i]).Text = "Please select a target first";
                        ((TextBlock)errorBoxes[i]).Visibility = Visibility.Visible;
                    }
                    return;
                }
            }
        }
    }
}
