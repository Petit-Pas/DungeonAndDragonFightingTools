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
        List<OnHitStatus> GetOnHitStatusesAppliedBy(string casterName);
        List<OnHitStatus> GetOnHitStatusesAppliedBy(PlayableEntity caster);// => GetOnHitStatusesAppliedBy(caster.DisplayName);

    }
}
