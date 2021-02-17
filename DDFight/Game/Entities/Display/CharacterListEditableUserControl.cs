using DDFight.Controlers;
using DDFight.Tools.Save;
using System;
using System.Windows.Input;

namespace DDFight.Game.Entities.Display
{
    public class CharacterListEditableUserControl : BaseListUserControl
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
        public override void edit(object obj)
        {
            Character character = obj as Character;
            if (character.Edit())
                data_context.Save();
        }

        public override void remove(object obj)
        {
            Character character = obj as Character;
            data_context.RemoveCharacter(character);
        }

        public override void duplicate(object obj)
        {
            Character character = obj as Character;
            Character new_one = character.Clone() as Character;
            new_one.Name = new_one.Name + " - Copy";
            add_new(new_one);
        }

        public override void add_new(object obj = null)
        {
            Character character = obj as Character;
            if (character == null)
                character = new Character();
            if (character.Edit())
                data_context.AddCharacter(character);
        }
        #endregion ListControl
    }
}
