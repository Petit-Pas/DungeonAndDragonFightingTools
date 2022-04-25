using System;
using DDFight.Controlers;
using DDFight.WpfExtensions;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Fight;
using System.Windows.Input;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Fight.FightCommands.FighterCommands.RemoveFighterCommands;

namespace DDFight.Game.Entities.Display
{
    public class SoonToFightEntityListUserControl : BaseListUserControl
    {
        private Lazy<IMediator> _lazyMediator = new(() => DIContainer.GetImplementation<IMediator>());
        private IMediator _mediator => _lazyMediator.Value;

        public SoonToFightEntityListUserControl() : base ()
        {
            DataContextChanged += SoonToFightEntityListUserControl_DataContextChanged;
        }

        private void refresh_entityList()
        {
            EntityList = data_context;
        }

        private void SoonToFightEntityListUserControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            refresh_entityList();
        }

        private FightersList data_context
        {
            get => DataContext as FightersList;
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

        public override bool edit(object obj)
        {
            PlayableEntity entity = (PlayableEntity)obj;
            return entity.OpenEditWindow();
        }

        public override void remove(object obj)
        {
            _mediator.Execute(new RemoveFighterCommand(obj as PlayableEntity));
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
