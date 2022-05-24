using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;
using DnDToolsLibrary.Entities;

namespace DnDToolsLibrary.Fight.FightCommands.FighterCommands.RemoveFighterCommands
{
    public class RemoveFighterCommand : SuperDndCommandBase
    {
        public RemoveFighterCommand(PlayableEntity entity)
        {
            Entity = entity;
        }

        public PlayableEntity Entity { get; set; }
    }
}
