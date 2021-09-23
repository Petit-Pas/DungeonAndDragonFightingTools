using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using TempExtensionsOnHitStatus;

namespace DDFight.Commands.AttackCommands
{
    public class ApplyHitAttackResultCommand : DnDCommandBase
    {
        private ICustomConsole console = DIContainer.GetImplementation<ICustomConsole>();
        private IFontWeightProvider fontWeightProvider = DIContainer.GetImplementation<IFontWeightProvider>();

        public ApplyHitAttackResultCommand(HitAttackResult attackResult, bool isInnerCommand) : base(isInnerCommand)
        {
            AttackResult = (HitAttackResult)attackResult.Clone();
        }

        protected HitAttackResult AttackResult { get; }

        public override bool CanExecute(object parameter)
        {
            // should check that both entities are ok?
            return true;
        }

        public override void Execute(object parameter)
        {
            PlayableEntity attacker = AttackResult.Owner;
            PlayableEntity target = AttackResult.Target;

            #region FightLog

            console.AddEntry($"{attacker.DisplayName}", fontWeightProvider.Bold);
            console.AddEntry(" attacks ");
            console.AddEntry($"{target.DisplayName}", fontWeightProvider.Bold);
            console.AddEntry(". ");
            console.AddEntry($"{AttackResult.RollResult.Description}\r\n", fontWeightProvider.Bold);

            #endregion FightLog

            if (AttackResult.RollResult.Hits)
            {
                DnDCommandManager.Instance.TryExecute(new ApplyDamageResultListCommand(target, AttackResult.DamageList, true));
                foreach (OnHitStatus onHitStatus in AttackResult.OnHitStatuses)
                {
                    // TODO in order to allow a correct history here, the fact of handling Statuses should be commands as well
                    onHitStatus.CheckIfApply(attacker, target);
                }
            }
        }
    }
}
