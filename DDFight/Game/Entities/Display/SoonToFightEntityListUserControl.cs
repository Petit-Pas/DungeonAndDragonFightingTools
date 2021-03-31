using DDFight.Controlers;
using DDFight.Game.Fight;
using DDFight.Tools;
using System.Windows.Input;

namespace DDFight.Game.Entities.Display
{
    public class SoonToFightEntityListUserControl : BaseListUserControl
    {
        public SoonToFightEntityListUserControl() : base ()
        {
            DataContextChanged += SoonToFightEntityListUserControl_DataContextChanged;
        }

        private void refresh_entityList()
        {
            EntityList = data_context?.Elements;
        }

        private void SoonToFightEntityListUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            refresh_entityList();
        }

        private FighterList data_context
        {
            get => DataContext as FighterList;
        }

        protected override void EntityListControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                remove(EntityListControl.SelectedItem);
                e.Handled = true;
            }
            else
                base.EntityListControl_KeyDown(sender, e);
        }

        #region ListControl

        public override void edit(object obj)
        {
            //PlayableEntity entity = obj as PlayableEntity;
            //entity.OpenEditWindow();
            // TODO
            Logger.Log("CRITICAL THIS SHOULD HAVE BEEN REPLACED");
        }

        public override void remove(object obj)
        {
            PlayableEntity entity = obj as PlayableEntity;
            data_context.RemoveElement(entity);
        }

        public override void duplicate(object obj)
        {
        }

        public override void add_new(object obj = null)
        {
        }

        #endregion ListControl

    }
}
