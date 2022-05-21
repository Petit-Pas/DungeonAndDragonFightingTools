using System.Linq;
using DnDToolsLibrary.Memory;

namespace DnDToolsLibrary.Status
{
    public class StatusReferenceList : GenericList<StatusReference>
    {
        public StatusReferenceList()
        {
        }

        private void init_copy(StatusReferenceList toCopy)
        {
            toCopy.ToList().ForEach(x => this.Add(x.Clone() as StatusReference));
        }

        public StatusReferenceList(StatusReferenceList toCopy)
        {
            init_copy(toCopy);
        }


        public override object Clone()
        {
            return new StatusReferenceList(this);
        }
    }
}
