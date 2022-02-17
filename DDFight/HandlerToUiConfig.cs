using DnDToolsLibrary.Dice.DiceCommancs.SavingThrowCommands.SavingThrowQueries;
using WpfDnDCommandHandlers;
using WpfDnDCommandHandlers.AttackQueries.DamageQueries;
using WpfDnDCommandHandlers.AttackQueries.DamageQueries.DamageResultListQueries;
using WpfDnDCommandHandlers.AttackQueries.SpellQueries.AttackSpellResultsQueries;
using WpfDnDCommandHandlers.AttackQueries.SpellQueries.NonAttackSpellResultsQueries;
using WpfDnDCommandHandlers.AttackQueries.SpellQueries.SpellLevelQueries.CantripLevelQueries;
using WpfDnDCommandHandlers.AttackQueries.SpellQueries.SpellLevelQueries.GetInputNormalSpellLevel;
using WpfDnDCommandHandlers.AttackQueries.SpellQueries.SpellTargetsQueries;
using WpfDnDCommandHandlers.DiceQueries.ConcentrationCheckQueries;
using WpfDnDCommandHandlers.DiceQueries.SavingThrowQueries;

namespace DDFight
{
    public static class HandlerToUiConfig
    {
        public static void Configure()
        {
            HandlerToUILinker.AddNewPair(typeof(DamageResultListQueryHandler), typeof(DamageResultListQueryHandlerWindow));
            HandlerToUILinker.AddNewPair(typeof(NormalSpellLevelQueryHandler), typeof(NormalSpellLevelQueryHandlerWindow));
            HandlerToUILinker.AddNewPair(typeof(CantripLevelQueryHandler), typeof(CantripLevelQueryWindow));
            HandlerToUILinker.AddNewPair(typeof(SpellTargetsQueryHandler), typeof(SpellTargetsQueryHandlerWindow));
            HandlerToUILinker.AddNewPair(typeof(AttackSpellResultsQueryHandler), typeof(AttackSpellResultsQueryHandlerWindow));
            HandlerToUILinker.AddNewPair(typeof(GetInputNonAttackSpellResultsHandler), typeof(NonAttackSpellResultsQueryHandlerWindow));
            HandlerToUILinker.AddNewPair(typeof(SavingThrowQueryHandler), typeof(SavingThrowQueryHandlerWindow));
            HandlerToUILinker.AddNewPair(typeof(ConcentrationCheckQueryHandler), typeof(SavingThrowQueryHandlerWindow));
        }
    }
}
