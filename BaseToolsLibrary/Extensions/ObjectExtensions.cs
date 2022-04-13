using System;

namespace BaseToolsLibrary.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsOfType(this object obj, Type type)
        {
            return obj.GetType() == type;
        }
    }
}
