using System.Windows;
using System.Windows.Data;

namespace WpfToolsLibrary.Extensions
{
    public static class BindingExtensions
    {
        public static BindingExpressionBase SetBinding(this DependencyObject target, DependencyProperty property, BindingBase binding)
        {
            return BindingOperations.SetBinding(target, property, binding);
        }
    }
}
