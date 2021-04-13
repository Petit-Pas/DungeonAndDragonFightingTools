using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using WpfToolsLibrary.Commands.WindowCommands;
using WpfToolsLibrary.Extensions;

namespace WpfCustomControlLibrary.Buttons
{
    public class ExitButtonControl : BaseButtonControl
    {
        public ExitButtonControl() : base()
        {
            BaseColor = Brushes.DarkRed;
            HoverColor = Brushes.Red;
            ClickColor = Brushes.PaleVioletRed;
            Content = new TextBlock() {
                Text = "X",
                Foreground = (Brush)System.Windows.Application.Current.Resources["Light"],
                FontSize = 15,
                FontWeight = FontWeights.DemiBold,
            };
            Click += ExitButtonControl_Click;
            CloseCommand_Click = new CloseCommand();
        }

        private void ExitButtonControl_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CloseCommand_Click.Execute(Application.Current.GetCurrentWindow());
        }

        public CloseCommand CloseCommand_Click { get; set; }
    }
}
