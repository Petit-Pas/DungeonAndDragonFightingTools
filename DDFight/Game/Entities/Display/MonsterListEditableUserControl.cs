using DDFight.Controlers;
using DDFight.Tools.Save;
using System;
using System.Windows.Input;

namespace DDFight.Game.Entities.Display
{
    public class MonsterListEditableUserControl : BaseListUserControl
    {
        public MonsterListEditableUserControl() : base()
        {
            DataContextChanged += NewMonsterListEditableUserControl_DataContextChanged;
        }

        private MonstersList data_context
        {
            get
            {
                try { return (MonstersList)DataContext; }
                catch (Exception) { return null; }
            }
        }

        private void refresh_entityList()
        {
            EntityList = data_context?.Monsters;
        }

        private void NewMonsterListEditableUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
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
                        Global.Context.FightContext.FightersList.AddMonster(EntityListControl.SelectedItem as Monster);
                    }
                }
            }
        }

        #region ListControl
        
        public override void edit(object obj)
        {
            Monster monster = obj as Monster;
            if (monster.Edit())
                data_context.Save();
        }

        public override void remove(object obj)
        {
            Monster monster = obj as Monster;

            data_context.RemoveMonster(monster);
        }

        public override void duplicate(object obj)
        {
            Monster monster = obj as Monster;
            Monster new_one = (Monster)monster.Clone();
            new_one.Name = new_one.Name + " - Copy";
            add_new(new_one);
        }

        public override void add_new(object obj = null)
        {
            Monster monster = obj as Monster;
            if (monster == null)
                monster = new Monster();
            if (monster.Edit())
                data_context.AddMonster(monster);
        }

        #endregion ListControl
    }
}
