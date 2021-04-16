using DnDToolsLibrary.Attacks.HitAttacks;
using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using TempExtensionsOnHitStatus;
using TempExtensionsPlayableEntity;
using WpfToolsLibrary.ConsoleTools;
using WpfToolsLibrary.Extensions;

namespace DDFight.Commands.AttackCommands
{
    public class ApplyHitAttackResultCommand : DnDCommandBase
    {
        public ApplyHitAttackResultCommand(HitAttackResult attackResult, bool isInnerCommand) : base(isInnerCommand)
        {
            AttackResult = (HitAttackResult)attackResult.Clone();
        }

        protected HitAttackResult AttackResult { get; }

        protected Paragraph CommandParagraph { get; set; }

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

            CommandParagraph = (Paragraph)FightConsole.Instance.UserLogs.Blocks.LastBlock;

            CommandParagraph.Inlines.Add(RunExtensions.BuildRun(attacker.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            CommandParagraph.Inlines.Add(RunExtensions.BuildRun(" attacks ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            CommandParagraph.Inlines.Add(RunExtensions.BuildRun(target.DisplayName, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            CommandParagraph.Inlines.Add(RunExtensions.BuildRun(". ", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));
            CommandParagraph.Inlines.Add(RunExtensions.BuildRun(AttackResult.RollResult.Description, (Brush)Application.Current.Resources["Light"], 15, FontWeights.Bold));
            CommandParagraph.Inlines.Add(RunExtensions.BuildRun("\r\n", (Brush)Application.Current.Resources["Light"], 15, FontWeights.Normal));

            #endregion FightLog

            if (AttackResult.RollResult.Hits)
            {
                DnDCommandManager.Instance.TryExecute(new ApplyDamageResultListCommand(target, AttackResult.DamageList, true));
                foreach (OnHitStatus onHitStatus in AttackResult.OnHitStatuses.Elements)
                {
                    // TODO in order to allow a correct history here, the fact of handling Statuses should be commands as well
                    onHitStatus.CheckIfApply(attacker, target);
                }
            }
        }
    }
}
