using DDFight.Controlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DDFight.Game.Entities.Display
{
    public class PlayableEntityListUserControl<T> : SpecializedListUserControl<T>
        where T : PlayableEntity, new ()
    {
        public PlayableEntityListUserControl() : base ()
        {
        }



        protected override void EntityListControl_KeyDown(object sender, KeyEventArgs e)
        {
            base.EntityListControl_KeyDown(sender, e);

            if (e.Handled == false)
            {
                if (e.Key == Key.Right || e.Key == Key.Enter)
                {
                    e.Handled = true;
                    if (EntityListControl.SelectedIndex != -1)
                    {
                        Global.Context.FightContext.FightersList.AddElementSilent(EntityListControl.SelectedItem as T);
                    }
                }
            }
        }
    }
}
