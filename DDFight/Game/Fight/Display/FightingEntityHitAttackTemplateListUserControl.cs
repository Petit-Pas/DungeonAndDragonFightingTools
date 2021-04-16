using DDFight.Commands;
using DDFight.Game.Aggression.Attacks.Display;
using DDFight.WpfExtensions;
using DnDToolsLibrary.Attacks.HitAttacks;
using System.Windows;
using System.Windows.Input;

namespace DDFight.Game.Fight.Display
{
    public class FightingEntityHitAttackTemplateListUserControl : HitAttackTemplateListUserControl
    {
        public FightingEntityHitAttackTemplateListUserControl() : base()
        {
            this.ButtonsVisibility = Visibility.Collapsed;
            this.IsEditable = false;
        }

        protected override void EntityList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (EntityListControl.SelectedIndex != -1)
            {
                ((HitAttackTemplate)EntityListControl.SelectedItem).ExecuteHitAttack();
                e.Handled = true;
            }
        }

        protected override void EntityListControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (EntityListControl.SelectedIndex != -1)
                {
                    ((HitAttackTemplate)EntityListControl.SelectedItem).ExecuteHitAttack();
                    e.Handled = true;
                }
            }
            base.EntityListControl_KeyDown(sender, e);
        }

    }
}
