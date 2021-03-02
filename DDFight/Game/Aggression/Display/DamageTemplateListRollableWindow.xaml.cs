using DDFight.Tools.UXShortcuts;
using DDFight.Windows;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DDFight.Game.Aggression.Display
{
    /// <summary>
    /// Interaction logic for DamageTemplateListRollableWindow.xaml
    /// </summary>
    public partial class DamageTemplateListRollableWindow : Window
    {
        public bool Validated = false;

        private DamageTemplateList data_context
        {
            get => (DamageTemplateList)DataContext;
        }

        public DamageTemplateListRollableWindow()
        {
            InitializeComponent();
            ValidateButton.IsEnabled = false;
            DataContextChanged += DamageTemplateListRollableWindow_DataContextChanged;
        }

        private void DamageTemplateListRollableWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            foreach (DamageTemplate dmg in data_context.Elements)
            {
                dmg.PropertyChanged += Dmg_PropertyChanged;
            }
        }

        private void Dmg_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ValidateButton.IsEnabled = false;
            if (RollableWindowTool.AreAllRollableChildrenRolled(this))
                ValidateButton.IsEnabled = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (RollableWindowTool.IsRollControlPressed(e))
            {
                e.Handled = true;
                RollableWindowTool.RollRollableChildren(this);
            }
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            Validated = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Validated == false || RollableWindowTool.AreAllRollableChildrenRolled(this) == false)
            {
                AskYesNoDataContext ctx = new AskYesNoDataContext()
                {
                    Message = "Are you sure you wish to cancel this?",
                };
                AskYesNoWindow win = new AskYesNoWindow() { DataContext = ctx };
                win.ShowCentered();

                if (ctx.Yes == false)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
