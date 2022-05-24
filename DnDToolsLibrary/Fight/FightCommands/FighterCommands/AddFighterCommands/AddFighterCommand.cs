using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;
using DnDToolsLibrary.Entities;

namespace DnDToolsLibrary.Fight.FightCommands.FighterCommands.AddFighterCommands
{
    public class AddFighterCommand : DndCommandBase
    {
        public AddFighterCommand(PlayableEntity entity)
        {
            Entity = entity;
        }

        public PlayableEntity Entity { get; set; }
    }
}
