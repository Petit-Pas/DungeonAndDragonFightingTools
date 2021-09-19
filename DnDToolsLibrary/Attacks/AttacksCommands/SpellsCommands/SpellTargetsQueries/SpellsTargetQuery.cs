using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.SpellTargetsQueries
{
    public class GetInputSpellTargetsCommand : IMediatorCommand
    {
        public GetInputSpellTargetsCommand(int amountTargets, bool targetCanBeSelectedMoreThanOnce)
        {
            AmountTargets = amountTargets;
            TargetCanBeSelectedMoreThanOnce = targetCanBeSelectedMoreThanOnce;
        }

        public bool TargetCanBeSelectedMoreThanOnce { get; set; }
        public int AmountTargets { get; set; }
    }
}
