using DnDToolsLibrary.Entities;

namespace DDFight.Game.Entities.Display
{
    public class MonsterListEditableUserControl : PlayableEntityListUserControl<Monster>
    {
        public MonsterListEditableUserControl() : base()
        {
            DataContextChanged += NewMonsterListEditableUserControl_DataContextChanged;
        }

        private MonsterList data_context
        {
            get => DataContext as MonsterList;
        }

        private void refresh_entityList()
        {
            EntityList = data_context?.Elements;
        }

        private void NewMonsterListEditableUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            refresh_entityList();
        }
    }
}
