using DDFight.Tools.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DDFight.Game.Entities.Display
{
    public class CharacterListEditableUserControl : PlayableEntityListEditableUserControl
    {
        public CharacterListEditableUserControl() : base()
        {
            DataContextChanged += NewCharacterListEditableUserControl_DataContextChanged;
        }

        private CharactersList data_context
        {
            get {
                try { return (CharactersList)DataContext; }
                catch (Exception) { return null; }
            }
        }

        private void refresh_entityList()
        {
            EntityList = data_context?.Characters;
        }

        private void NewCharacterListEditableUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            refresh_entityList();
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
                        Global.Context.FightContext.FightersList.AddCharacter(EntityListControl.SelectedItem as Character);
                    }
                }
            }
        }

        #region ListControl
        protected override void edit(PlayableEntity entity)
        {
            if (entity.Edit())
                data_context.Save();
        }

        protected override void remove(PlayableEntity entity)
        {
            data_context.RemoveCharacter(entity as Character);
        }

        protected override void duplicate(PlayableEntity entity)
        {
            Character new_one = (Character)entity.Clone();
            new_one.Name = new_one.Name + " - Copy";
            add_new(new_one);
        }

        protected override void add_new(PlayableEntity entity = null)
        {
            if (entity == null)
                entity = new Character();
            if (entity.Edit())
                data_context.AddCharacter(entity as Character);
        }
        #endregion ListControl
    }
}
