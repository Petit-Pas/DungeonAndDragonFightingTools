using DnDToolsLibrary.Entities;

namespace DDFight.Game.Entities.Display
{
    public class CharacterListEditableUserControl : PlayableEntityListUserControl<Character> //BaseListUserControl
    {
        public CharacterListEditableUserControl() : base()
        {
            DataContextChanged += CharacterListEditableUserControl_DataContextChanged;
        }

        private CharacterList data_context
        {
            get => DataContext as CharacterList;
        }

        private void refresh_entityList()
        {
            EntityList = data_context?.Elements;
        }

        private void CharacterListEditableUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            refresh_entityList();
        }       
    }
}
