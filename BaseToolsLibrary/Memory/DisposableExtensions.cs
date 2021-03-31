using System;
using System.Linq;
using System.Reflection;

namespace BaseToolsLibrary.Memory
{
    public static class DisposableExtensions
    {
        public static void DisposeAllDisposableMembers(this object target)
        {
            if (target == null) return;
            FieldInfo[] fields = target.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Concat(target.GetType().BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)).ToArray();
            var disposables = fields.Where(x => x.FieldType.GetInterfaces().Contains(typeof(IDisposable)));

            foreach (var disposableField in disposables)
            {
                var value = (IDisposable)disposableField.GetValue(target);
                if (value != null)
                    value.Dispose();
            }
        }
    }
}
