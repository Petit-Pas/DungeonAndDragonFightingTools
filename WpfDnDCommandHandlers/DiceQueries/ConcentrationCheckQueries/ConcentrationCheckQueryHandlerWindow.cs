using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Entities.EntitiesCommands.ConcentrationCommands.ConcentrationCheckQueries;
using WpfCustomControlLibrary.ModalWindows;
using WpfDnDCommandHandlers.DiceQueries.SavingThrowQueries;

namespace WpfDnDCommandHandlers.DiceQueries.ConcentrationCheckQueries
{
    public class ConcentrationCheckQueryHandlerWindow : SavingThrowQueryHandlerWindow, IResultWindow<ConcentrationCheckQuery, SavingThrow>
    {
        public void LoadContext(ConcentrationCheckQuery context)
        {
            base.LoadContext(context);
        }
    }
}
