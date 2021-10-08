using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDToolsLibrary.Fight
{
    public interface IStatusProvider : IList<CustomVerboseStatus>
    {
        CustomVerboseStatus GetStatusById(Guid id);
    }
}
