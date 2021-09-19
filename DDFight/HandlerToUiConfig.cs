using WpfDnDCommandHandlers;
using WpfDnDCommandHandlers.AttackQueries.DamageQueries;
using WpfDnDCommandHandlers.AttackQueries.DamageQueries.DamageResultListQueries;
using WpfDnDCommandHandlers.AttackQueries.SpellQueries.AttackSpellResultsQueries;
using WpfDnDCommandHandlers.AttackQueries.SpellQueries.NonAttackSpellResultsQueries;
using WpfDnDCommandHandlers.AttackQueries.SpellQueries.SpellLevelQueries.CantripLevelQueries;
using WpfDnDCommandHandlers.AttackQueries.SpellQueries.SpellLevelQueries.GetInputNormalSpellLevel;
using WpfDnDCommandHandlers.AttackQueries.SpellQueries.SpellTargetsQueries;

namespace DDFight
{
    public static class HandlerToUiConfig
    {
        public static void Configure()
        {
            HandlerToUILinker.AddNewPair(typeof(DamageResultListQueryHandler), typeof(DamageResultListQueryHandlerWindow));
            HandlerToUILinker.AddNewPair(typeof(GetInputSpellLevelHandler), typeof(GetInputNormalSpellLevelWindow));
            HandlerToUILinker.AddNewPair(typeof(CantripLevelQueryHandler), typeof(CantripLevelQueryWindow));
            HandlerToUILinker.AddNewPair(typeof(SpellTargetsQueryHandler), typeof(SpellTargetsQueryHandlerWindow));
            HandlerToUILinker.AddNewPair(typeof(AttackSpellResultsQueryHandler), typeof(AttackSpellResultsQueryHandlerWindow));
            HandlerToUILinker.AddNewPair(typeof(GetInputNonAttackSpellResultsHandler), typeof(NonAttackSpellResultsQueryHandlerWindow));
        }
    }
}
