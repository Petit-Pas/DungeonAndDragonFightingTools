using System;

namespace BaseToolsLibrary.Memory
{
    /// <summary>
    ///     intended to provide with a void CopyAssign (to_copy) object
    ///     This will allow to modify an object in a list for instance, without overriding the reference to this object, as there may be other owner of the reference
    /// </summary>
    public interface ICopyAssignable : ICloneable
    {
        void CopyAssign(object to_copy);
    }
}
