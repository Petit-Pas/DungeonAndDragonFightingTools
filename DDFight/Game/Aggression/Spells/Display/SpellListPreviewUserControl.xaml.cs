using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DDFight.WpfExtensions;
using DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.CastSpellCommands;
using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities;
using System.Windows.Controls;
using System.Windows.Input;

namespace DDFight.Game.Aggression.Spells.Display
{
    /// <summary>
    /// Interaction logic for SpellListPreviewUserControl.xaml
    /// </summary>
    public partial class SpellListPreviewUserControl : UserControl
    {
        private static IMediator _mediator = DIContainer.GetImplementation<IMediator>();

        private PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
        }

        public SpellListPreviewUserControl()
        {
            Initialized += SpellListPreviewUserControl_Initialized;
            InitializeComponent();
        }

        private void SpellListPreviewUserControl_Initialized(object sender, System.EventArgs e)
        {
            SpellListControl.EntityListControl.SelectionChanged += SpellListControl_SelectionChanged;
            SpellListControl.EntityListControl.MouseDoubleClick += SpellListControl_MouseDoubleClick;
        }

        private void SpellListControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpellListControl.EntityListControl.SelectedItem != null)
                SpellPreviewControl.DataContext = SpellListControl.EntityListControl.SelectedItem;
            else
                SpellPreviewControl.DataContext = null;
        }

        private void SpellListControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SpellListControl.EntityListControl.SelectedIndex != -1) 
            {
                Spell spell = (Spell)SpellListControl.EntityListControl.SelectedItem;
                CastSpellCommand castSpellCommand = new CastSpellCommand(data_context, spell);
                _mediator.Execute(castSpellCommand);
            }
        }
    }
}
