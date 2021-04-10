using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfToolsLibrary.Commands.WindowCommands;
using WpfToolsLibrary.Extensions;

namespace WpfCustomControlLibrary.CardControls
{
    public class WindowCardControl : UpCardControl
    {
        private static readonly ResourceDictionary styleDict = new ResourceDictionary()
        {
            Source = new Uri("/WpfCustomControlLibrary;component/CardControls/WindowCardStyle.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Style style = styleDict["WindowCardStyle"] as Style;

        public WindowCardControl() : base()
        {
#pragma warning disable 0067
#pragma warning disable 0168
            // a bug in this package forces us to use it in order for the dll to be loaded
            Microsoft.Xaml.Behaviors.TriggerAction nullable;
#pragma warning restore 0168
#pragma warning restore 0067

            if (style != null)
                this.Style = style;
        }

        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }
        private static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(WindowCardControl),
                new PropertyMetadata("Title")
            );
    }
}
