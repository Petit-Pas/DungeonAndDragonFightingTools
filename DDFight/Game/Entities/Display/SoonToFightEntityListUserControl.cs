using DDFight.Controlers;
using DDFight.Game.Fight;
using DDFight.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Game.Entities.Display
{
    public class SoonToFightEntityListUserControl : BaseListUserControl
    {
        public SoonToFightEntityListUserControl() : base ()
        {
            DataContextChanged += NewSoonToFightEntityListUserControl_DataContextChanged;
        }

        private void refresh_entityList()
        {
            EntityList = data_context?.Elements;
        }

        private void NewSoonToFightEntityListUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            refresh_entityList();
        }

        private FighterList data_context
        {
            get
            {
                try { return (FighterList)DataContext; }
                catch (Exception) { return null; }
            }
        }

        #region ListControl
        
        public override void edit(object obj)
        {
            PlayableEntity entity = obj as PlayableEntity;
            entity.Edit();
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
