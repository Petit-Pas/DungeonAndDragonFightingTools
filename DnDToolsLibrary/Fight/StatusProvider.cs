using DnDToolsLibrary.Entities;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Fight
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

        public List<OnHitStatus> GetOnHitStatusesAppliedBy(string casterName)
        {
            return this.OfType<OnHitStatus>().Where(x => x.Caster.DisplayName == casterName).Cast<OnHitStatus>().ToList();
        }

        public List<OnHitStatus> GetOnHitStatusesAppliedBy(PlayableEntity caster)
        {
            return GetOnHitStatusesAppliedBy(caster.DisplayName);
        }
    }
}
