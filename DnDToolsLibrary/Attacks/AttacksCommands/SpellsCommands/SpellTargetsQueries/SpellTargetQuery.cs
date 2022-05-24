using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellTargetsQueries
{
    public class SpellTargetQuery : DndCommandBase
    {
        public SpellTargetQuery(int amountTargets, bool targetCanBeSelectedMoreThanOnce)
        {
            AmountTargets = amountTargets;
            TargetCanBeSelectedMoreThanOnce = targetCanBeSelectedMoreThanOnce;
        }

        public bool TargetCanBeSelectedMoreThanOnce { get; set; }
        public int AmountTargets { get; set; }
    }
}
