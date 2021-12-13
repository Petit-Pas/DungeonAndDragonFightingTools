using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellTargetsQueries
{
    public class SpellTargetQuery : IMediatorCommand
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
