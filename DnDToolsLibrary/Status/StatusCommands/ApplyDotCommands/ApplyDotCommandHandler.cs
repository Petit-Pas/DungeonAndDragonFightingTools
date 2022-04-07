using System;
using System.Linq;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Attacks.AttacksCommands.DamageCommands.DamageResultListQueries;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using DnDToolsLibrary.Entities.EntitiesCommands.StatusCommands.ApplyDotCommands;
using DnDToolsLibrary.Fight;

namespace DnDToolsLibrary.Status.StatusCommands.ApplyDotCommands
{
    public class ApplyDotCommandHandler : SuperCommandHandlerBase<ApplyDotCommand, IMediatorCommandResponse>
    {

        private static readonly Lazy<IStatusProvider> _lazyStatusProvider = new (DIContainer.GetImplementation<IStatusProvider>);
        private static  IStatusProvider _statusProvider => _lazyStatusProvider.Value;

        public override IMediatorCommandResponse Execute(ApplyDotCommand command)
        {
            var status = _statusProvider.GetOnHitStatusById(command.StatusReference);
            if (status == null)
            {
                Console.WriteLine("Error: Something went wrong when trying to execute ApplyDotCommand for status: " +
                                  $"{(command.StatusReference.ToString() ?? $"No Guid could be found for the command of type {command.GetType()}")}");
                return MediatorCommandStatii.Canceled;
            }

            var damageQuery = new DamageResultListQuery(GetDamageToApply(status, command), $"{status.TargetName} takes damage from {status.DisplayName}");

            if (damageQuery.DamageList.Count != 0)
            {
                var response = _mediator.Value.Execute(damageQuery) as ValidableResponse<GetInputDamageResultListResponse>;
                if (response == null || response.IsValid == false)
                {
                    return MediatorCommandStatii.Canceled;
                }

                var applyDamageCommand = new ApplyDamageResultListCommand(status.TargetName, response.Response.DamageResultList);
                _mediator.Value.Execute(applyDamageCommand);
                command.PushToInnerCommands(applyDamageCommand);
                return MediatorCommandStatii.Success;
            }
            return MediatorCommandStatii.Canceled;
        }

        private static DamageResultList GetDamageToApply(OnHitStatus status, ApplyDotCommand command)
        {
            var toApply = new DamageResultList();
            foreach (var dot in status.DotDamageList.Where(x => x.TriggersOnCastersTurn == command.CasterTurn && x.TriggersStartOfTurn == command.StartOfTurn))
            {
                toApply.AddElementSilent(new DamageResult(dot));
            }

            return toApply;
        }
    }
}
