using DDFight.Controlers;
using DDFight.Tools.Save;
using System;
using System.Windows.Input;

namespace DDFight.Game.Entities.Display
{
    public class CharacterListEditableUserControl : PlayableEntityListUserControl<Character> //BaseListUserControl
    {
        public CharacterListEditableUserControl() : base()
        {
            DataContextChanged += NewCharacterListEditableUserControl_DataContextChanged;
        }

        private CharacterList data_context
        {
            get 
            {
                try { return (CharacterList)DataContext; }
                catch (Exception) { return null; }
            }
        }

        private void refresh_entityList()
        {
            EntityList = data_context?.Elements;
        }

        private void NewCharacterListEditableUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            refresh_entityList();
        }       
    }
}
