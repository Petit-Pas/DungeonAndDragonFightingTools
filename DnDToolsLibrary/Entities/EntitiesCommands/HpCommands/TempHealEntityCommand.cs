namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands
{
    public class TempHealEntityCommand : EntityCommand
    {
        public int Amount { get; set; }
        public int? From { get; set; }
        public int? To { get; set; }

        public TempHealEntityCommand(PlayableEntity target, int amount) : base(target.DisplayName)
        {
            Amount = amount;
        }
    }
}
