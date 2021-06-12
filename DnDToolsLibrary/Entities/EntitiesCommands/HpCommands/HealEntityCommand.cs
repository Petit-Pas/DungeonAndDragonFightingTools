using BaseToolsLibrary.Mediator;
using System;

namespace DnDToolsLibrary.Entities.EntitiesCommands.HpCommands
{
    public class HealEntityCommand : EntityCommand
    {
        public int Amount { get; set; }
        public int? From { get; set ; }
        public int? To { get; set; }

        public HealEntityCommand(PlayableEntity target, int amount) : base(target.DisplayName)
        {
            Amount = amount;
        }
    }
}
