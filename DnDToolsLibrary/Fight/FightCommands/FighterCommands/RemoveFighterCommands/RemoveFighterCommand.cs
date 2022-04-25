using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities;

namespace DnDToolsLibrary.Fight.FightCommands.FighterCommands.RemoveFighterCommands
{
    public class RemoveFighterCommand : IMediatorCommand
    {
        public RemoveFighterCommand(PlayableEntity entity)
        {
            Entity = entity;
        }

        public PlayableEntity Entity { get; set; }
    }
}
