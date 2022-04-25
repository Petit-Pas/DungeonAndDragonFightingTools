using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities;

namespace DnDToolsLibrary.Fight.FightCommands.FighterCommands.AddFighterCommands
{
    public class AddFighterCommand : IMediatorCommand
    {
        public AddFighterCommand(PlayableEntity entity)
        {
            Entity = entity;
        }

        public PlayableEntity Entity { get; set; }
    }
}
