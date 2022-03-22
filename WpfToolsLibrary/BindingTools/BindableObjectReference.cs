using System.Windows;

namespace WpfToolsLibrary.BindingTools
{
    /// <summary>
    ///     This class was referenced by Martin on https://stackoverflow.com/questions/31785228/c-wpf-binding-to-an-element-outside-of-the-visual-logical-tree
    ///     as a solution to bind controls outside of named scope
    /// </summary>
    public class BindableObjectReference : DependencyObject
    {
        public object Object
        {
            get { return GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }

        public static readonly DependencyProperty ObjectProperty =
            DependencyProperty.Register("Object", typeof(object),
            typeof(BindableObjectReference), new PropertyMetadata(null));
    }
}
