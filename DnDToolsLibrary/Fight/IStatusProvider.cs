using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;

namespace DnDToolsLibrary.Fight
{
    public interface IStatusProvider : IList<CustomVerboseStatus>
    {
        CustomVerboseStatus GetStatusById(Guid id);
        OnHitStatus GetOnHitStatusById(Guid id);
        IEnumerable<OnHitStatus> GetOnHitStatusesAppliedBy(string casterName);
        IEnumerable<OnHitStatus> GetOnHitStatusesAppliedBy(PlayableEntity caster);// => GetOnHitStatusesAppliedBy(caster.DisplayName);

        IEnumerable<OnHitStatus> GetOnHitStatusesAppliedOn(string affectedName);
    }
}
