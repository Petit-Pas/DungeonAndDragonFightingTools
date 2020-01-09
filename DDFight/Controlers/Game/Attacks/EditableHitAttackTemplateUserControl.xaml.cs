﻿using DDFight.Controlers.Game.Dices;
using DDFight.Game.Aggression;
using DDFight.Game.Aggression.Attacks;
using DDFight.Game.DamageAffinity;
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

namespace DDFight.Controlers.Game.Attacks
{
    /// <summary>
    /// Interaction logic for EditableHitAttackTemplate.xaml
    /// </summary>
    public partial class EditableHitAttackTemplateUserControl : UserControl, IValidable
    {
        private HitAttackTemplate _dataContext
        {
            get => (HitAttackTemplate)this.DataContext;
        }

        public EditableHitAttackTemplateUserControl()
        {
            InitializeComponent();

            Loaded += EditableHitAttackTemplate_Loaded;
        }


        private void EditableHitAttackTemplate_Loaded(object sender, RoutedEventArgs e)
        {
            refresh_damage_list();
        }

        private void refresh_damage_list()
        {
            List<DamageTemplate> list = new List<DamageTemplate>();
            foreach (DamageTemplate tmp in _dataContext.DamageList)
            {
                list.Add(tmp);
            }
            DamageListView.ItemsSource = list;
        }

        private void AddDamage_Button_Click(object sender, RoutedEventArgs e)
        {
            _dataContext.DamageList.Add(new DamageTemplate("1d4", DamageTypeEnum.Force));
            refresh_damage_list();
        }

        private void RemoveDamage_Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            foreach (DamageTemplate tmp in DamageListView.ItemsSource)
            {
                if (tmp == ((Button)sender).DataContext)
                {
                    _dataContext.DamageList.RemoveAt(index);
                    break;
                }
                index += 1;
            }
            refresh_damage_list();
        }

        public bool IsValid()
        {
            return this.AreAllChildrenValid();
        }
    }
}
