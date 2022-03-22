using BaseToolsLibrary.Mediator;

namespace DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries
{
    public class SavingThrowQuery : IMediatorCommand
    {
        public SavingThrowQuery(SavingThrow saving, string reason)
        {
            SavingThrow = saving;
            Reason = reason;
        }

        public SavingThrow SavingThrow { get; set; }
        public string Reason { get; set; }
    }
}
