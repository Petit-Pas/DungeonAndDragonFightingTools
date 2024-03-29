﻿using System;
using System.Linq;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.BaseCommands;
using DnDToolsLibrary.Entities;

namespace DnDToolsLibrary.Fight.FightCommands.FighterCommands.AddFighterCommands
{
    public class AddFighterCommandHandler : DndCommandHandlerBase<AddFighterCommand, IMediatorCommandResponse>
    {
        public override IMediatorCommandResponse Execute(AddFighterCommand genericCommand)
        {
            if (genericCommand.Entity is Monster monster)
            {
                return AddMonsterToFight(monster.Clone() as Monster);
            }
            else if (genericCommand.Entity is Character character)
            {
                return AddCharacterToFights(character);
            }
            else
            {
                Console.WriteLine($"ERROR: Trying to add an entity of type {genericCommand.Entity.GetType()}, which is not handled by AddFighterCommandHandler");
                return MediatorCommandStatii.Error;
            }
        }

        private IMediatorCommandResponse AddMonsterToFight(Monster monster)
        {
            var others = FightersProvider.GetMonstersByName(monster.Name).ToArray();
            var i = 0;

            if (others.Any())
            {
                // if there are already monster of this type, we make sure that their initiative roll is the same
                monster.InitiativeRoll = others.First().InitiativeRoll;

                // we then check for the 1st available index to append to create the DisplayName
                var alreadyUsedNumbers = others.Select(x => int.Parse(x.DisplayName.Substring(x.Name.Length + 2))).ToArray();
                for (; i < others.Length; i += 1)
                {
                    if (!alreadyUsedNumbers.Contains(i))
                    {
                        break;
                    }
                }
            }

            monster.DisplayName = $"{monster.Name} - {i}";
            FightersProvider.AddFighter(monster);
            return MediatorCommandStatii.Success;
        }

        private IMediatorCommandResponse AddCharacterToFights(Character character)
        {
            if (FightersProvider.GetFighterByDisplayName(character.DisplayName) != null)
            {
                // Character already exists in fight
                return MediatorCommandStatii.Canceled;
            }
            FightersProvider.AddFighter(character);
            return MediatorCommandStatii.Success;
        }

        public override void Undo(AddFighterCommand command)
        {
            base.Undo(command);

            FightersProvider.RemoveFighter(command.Entity);
        }
    }
}
