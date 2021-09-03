using WpfDnDCommandHandlers;
using WpfDnDCommandHandlers.AttackCommands.DamageCommands;
using WpfDnDCommandHandlers.AttackCommands.DamageCommands.GetInputResultList;
using WpfDnDCommandHandlers.AttackCommands.SpellCommands.GetInputAttackSpellResults;
using WpfDnDCommandHandlers.AttackCommands.SpellCommands.GetInputNonAttackSpellResults;
using WpfDnDCommandHandlers.AttackCommands.SpellCommands.GetInputSpellLevel.GetInputCantripLevel;
using WpfDnDCommandHandlers.AttackCommands.SpellCommands.GetInputSpellLevel.GetInputNormalSpellLevel;
using WpfDnDCommandHandlers.AttackCommands.SpellCommands.GetInputSpellTargets;

namespace DDFight
{
    public static class HandlerToUiConfig
    {
        public static void Configure()
        {
            HandlerToUILinker.AddNewPair(typeof(GetInputDamageResultListHandler), typeof(GetInputDamageResultListWindow));
            HandlerToUILinker.AddNewPair(typeof(GetInputSpellLevelHandler), typeof(GetInputNormalSpellLevelWindow));
            HandlerToUILinker.AddNewPair(typeof(GetInputCantripLevelHandler), typeof(GetInputCantripLevelWindow));
            HandlerToUILinker.AddNewPair(typeof(GetInputSpellTargetsHandler), typeof(GetInputSpellTargetsWindow));
            HandlerToUILinker.AddNewPair(typeof(GetInputAttackSpellResultsHandler), typeof(GetInputAttackSpellResultsWindow));
            HandlerToUILinker.AddNewPair(typeof(GetInputNonAttackSpellResultsHandler), typeof(GetInputNonAttackSpellResultsWindow));
        }
    }
}
