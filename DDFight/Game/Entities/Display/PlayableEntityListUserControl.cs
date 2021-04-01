using DDFight.Controlers;
using DDFight.WpfExtensions;
using DnDToolsLibrary.Entities;
using System.Windows.Input;
using TempExtensionsPlayableEntity;

namespace DDFight.Game.Entities.Display
{
    public class PlayableEntityListUserControl<T> : SpecializedListUserControl<T>
        where T : PlayableEntity, new ()
    {
        public PlayableEntityListUserControl() : base ()
        {
        }
        
        public override bool edit(object element)
        {
            if (element is PlayableEntity playableEntity)
                return playableEntity.OpenEditWindow();
            return false;
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
                        GlobalContext.Context.FightContext.FightersList.AddElement(EntityListControl.SelectedItem as T);
                    }
                }
            }
        }
    }
}
