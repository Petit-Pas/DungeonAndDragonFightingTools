using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.Spells;
using System.Collections.Generic;

namespace DnDToolsLibrary.Attacks.AttacksCommands.SpellsCommands.AttackSpellResultsQueries
{
    public class AttackSpellResults : List<NewAttackSpellResult>, IMediatorCommandResponse
    {
    }
}
