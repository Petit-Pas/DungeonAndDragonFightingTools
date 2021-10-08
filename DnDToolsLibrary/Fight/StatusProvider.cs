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
    }
}
