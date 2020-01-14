using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDFight.Tools
{
    /// <summary>
    ///     intended to provide with a void CopyAssign (to_copy) object
    ///     This will allow to modify an object in a list for instance, without overriding the reference to this object, as there may be other owner of the reference
    /// </summary>
    public interface ICopyAssignable
    {
        void CopyAssign(object to_copy);
    }
}
