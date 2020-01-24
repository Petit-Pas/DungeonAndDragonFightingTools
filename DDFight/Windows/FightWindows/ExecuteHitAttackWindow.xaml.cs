﻿using DDFight.Game;
using DDFight.Game.Aggression;
using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Dices;
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
using System.Windows.Shapes;

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

        private List<FrameworkElement> buttons = new List<FrameworkElement>();
        private List<FrameworkElement> comboBoxes = new List<FrameworkElement>();
        private List<FrameworkElement> groupBoxes = new List<FrameworkElement>();
        private List<FrameworkElement> errorBoxes = new List<FrameworkElement>();

        private void AttackList_LayoutUpdated(object sender, EventArgs e)
        {
            comboBoxes = this.GetAllChildrenByName("HitAttackTargetComboControl");
            foreach (ComboBox box in comboBoxes)
            {
                box.ItemsSource = Global.Context.FightContext.FightersList.Fighters;
            }
            AttackList.LayoutUpdated -= AttackList_LayoutUpdated;

            List<FrameworkElement> damageControls = this.GetAllChildrenByName("DamageControl");
            foreach (ItemsControl control in damageControls)
            {
                //control.ItemsSource = data_context.DamageList.Clone();
            }
            Console.WriteLine(damageControls.Count);

            buttons = this.GetAllChildrenByName("AttackButtonControl");
            foreach (Button btn in buttons)
            {
                btn.IsEnabled = false;
            }
            if (buttons.Count != 0)
                ((Button)buttons.ElementAt(0)).IsEnabled = true;

            groupBoxes = this.GetAllChildrenByName("AttackGroupBoxControl");
            errorBoxes = this.GetAllChildrenByName("ErrorTextblockControl");
        }

        private void AttackButtonControl_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i != buttons.Count; i += 1)
            {
                if (((Button)buttons[i]).IsEnabled == true)
                {
                    if (((ComboBox)comboBoxes[i]).SelectedIndex != -1)
                    {
                        // a character has well been selected
                        if (groupBoxes[i].AreAllChildrenValid())
                        {
                            // there is no error in input boxes

                            // computes unrolled rolls
                            if (attacks[i].HitRoll == 0)
                                attacks[i].HitRoll = (uint)DiceRoll.Roll("1d20");
                            foreach (DamageTemplate dmg in attacks[i].DamageList)
                            {
                                if (dmg.Damage.LastResult == 0)
                                    dmg.Damage.Roll();
                                Console.WriteLine(dmg.Damage.LastResult);
                            }
                            // Go to next Attack
                            ((Button)buttons[i]).IsEnabled = false;
                            if (i + 1 != buttons.Count)
                            {
                                ((Button)buttons[i + 1]).IsEnabled = true;
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

        private void QuitButtonControl_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
