using DnDToolsLibrary.Attacks.AttacksCommands.DamageCommands.DamageResultListQueries;
using DnDToolsLibrary.Attacks.Damage;
using System.Windows;
using System.Windows.Input;
using WpfCustomControlLibrary.ModalWindows;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace WpfDnDCommandHandlers.AttackQueries.DamageQueries
{
    /// <summary>
    /// Logique d'interaction pour DamageResultListRollableWindow.xaml
    /// </summary>
    public partial class DamageResultListQueryHandlerWindow : Window, IResultWindow<DamageResultListQuery, GetInputDamageResultListResponse>
    {
        private DamageResultListQuery data_context
        {
            get => DataContext as DamageResultListQuery;
        }
        public bool Validated { get; set; }

        public DamageResultListQueryHandlerWindow()
        {
            DataContextChanged += DamageResultListRollableWindow_DataContextChanged;
            InitializeComponent();
            ValidateButton.IsEnabled = false;
        }

        private void DamageResultListRollableWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (data_context != null)
            {
                foreach (DamageResult dmg in data_context.DamageList)
                {
                    dmg.PropertyChanged += Dmg_PropertyChanged;
                }
            }
        }

        private void Dmg_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ValidateButton.IsEnabled = false;
            if (this.AreAllRollableChildrenRolled())
                ValidateButton.IsEnabled = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.IsRollControlPressed(e))
            {
                e.Handled = true;
                this.RollRollableChildren();
            }
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            Validated = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Validated == false || this.AreAllRollableChildrenRolled() == false)
            {
                YesNoWindow win = new YesNoWindow() { Text = "Are you sure you wish to cancel this ?", Validated = false };
                win.ShowCentered();

                if (win.Validated)
                {
                    e.Cancel = false;
                }
            }
        }

        public void LoadContext(DamageResultListQuery context)
        {
            DataContext = context;
        }

        public GetInputDamageResultListResponse GetResult()
        {
            return new GetInputDamageResultListResponse(data_context.DamageList.Clone() as DamageResultList);
        }
    }
}
