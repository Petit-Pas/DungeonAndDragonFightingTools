﻿using DDFight.Game;
using DDFight.Game.Aggression.Attacks;
using DDFight.Game.Characteristics;
using DDFight.Game.Dices;
using System;
using System.Collections.Generic;
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

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Interaction logic for ConcentrationCheckWindow.xaml
    /// </summary>
    public partial class ConcentrationCheckWindow : Window, INotifyPropertyChanged
    {
        private PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
        }

        public ConcentrationCheckWindow()
        {
            InitializeComponent();
        }

        public SituationalAdvantageModifiers SituationalAdvantageModifiers
        {
            get => _situationalAdvantageModifiers;
            set
            {
                _situationalAdvantageModifiers = value;
                NotifyPropertyChanged();
            }
        }
        private SituationalAdvantageModifiers _situationalAdvantageModifiers = new SituationalAdvantageModifiers();

        public SituationalSavingThrowModifier SituationalSavingThrowModifier
        {
            get => _situationalSavingThrowModifier;
            set 
            {
                _situationalSavingThrowModifier = value;
                NotifyPropertyChanged();
            }
        }
        private SituationalSavingThrowModifier _situationalSavingThrowModifier = new SituationalSavingThrowModifier();

        public int Roll
        {
            get => _roll;
            set
            {
                _roll = value;
                NotifyPropertyChanged();
                if (value != 0)
                {
                    ButtonControl.Content = "Validate";
                }
                else
                {
                    ButtonControl.Content = "Roll";
                }
            }
        }
        private int _roll = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(this.AreAllChildrenValid()))
            {
                ErrorMessageControl.Visibility = Visibility.Visible;
                return;
            }
            ErrorMessageControl.Visibility = Visibility.Collapsed;
            if (Roll == 0)
            {
                roll_dice();
            }
            else
            {
                check_concentration();
                this.Close();
            }
        }

        public bool Success = false;

        private void check_concentration()
        {
            if (Roll + SituationalSavingThrowModifier.Modifier + data_context.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Constitution) >= 10)
                Success = true;
            else
            {
                Paragraph paragraph = (Paragraph)Global.Context.UserLogs.Blocks.LastBlock;
                paragraph.Inlines.Add(Extensions.BuildRun(data_context.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
                paragraph.Inlines.Add(Extensions.BuildRun(" lost Focus: ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
                paragraph.Inlines.Add(Extensions.BuildRun((data_context.Characteristics.GetCharacteristicModifier(CharacteristicsEnum.Constitution) + SituationalSavingThrowModifier.Modifier + Roll).ToString()
                    + " / 10\n" , (Brush)Application.Current.Resources["Light"], 15, FontWeights.SemiBold));
            }
        }

        private void roll_dice()
        {
            Roll = DiceRoll.Roll("1d20", SituationalAdvantageModifiers.SituationalAdvantage, SituationalAdvantageModifiers.SituationalDisadvantage);
        }

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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}