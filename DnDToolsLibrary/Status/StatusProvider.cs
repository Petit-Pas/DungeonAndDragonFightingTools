using System;
using System.Collections.Generic;
using System.Linq;
using DnDToolsLibrary.Entities;

namespace DnDToolsLibrary.Status
{
    public class StatusProvider : List<CustomVerboseStatus>, IStatusProvider
    {
        public CustomVerboseStatus GetStatusById(Guid id)
        {
            return this.FirstOrDefault(x => x.Id == id);
        }
        public OnHitStatus GetOnHitStatusById(Guid id)
        {
            return this.OfType<OnHitStatus>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<OnHitStatus> GetOnHitStatusesAppliedBy(string casterName)
        {
            return this.OfType<OnHitStatus>().Where(x => x.Caster.DisplayName == casterName).ToList();
        }

        public IEnumerable<OnHitStatus> GetOnHitStatusesAppliedBy(PlayableEntity caster)
        {
            return GetOnHitStatusesAppliedBy(caster.DisplayName);
        }

        public IEnumerable<OnHitStatus> GetOnHitStatusesAppliedOn(string affectedName)
        {
            return this.OfType<OnHitStatus>().Where(x => x.TargetName == affectedName);
        }
    }
}
