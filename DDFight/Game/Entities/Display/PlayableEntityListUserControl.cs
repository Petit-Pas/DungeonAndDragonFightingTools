using BaseToolsLibrary;
using DDFight.Controlers;
using DDFight.WpfExtensions;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Memory;
using System.Windows.Input;

namespace DDFight.Game.Entities.Display
{
    public class PlayableEntityListUserControl<T> : SpecializedListUserControl<T>
        where T : PlayableEntity, IEquivalentComparable<T>, new ()
    {
        public PlayableEntityListUserControl() : base ()
        {
        }
        
        public override bool edit(object element)
        {
            if (element is PlayableEntity playableEntity)
            {
                bool retval = playableEntity.OpenEditWindow();
                if (retval)
                    ((GenericList<T>)EntityList).OnListElementChanged(new GenericList<T>.ListElementChangedArgs
                    {
                        Operation = GenericList<T>.GenericListOperation.Modification,
                        Element = (T)playableEntity,
                    });
                return retval;
            }
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
