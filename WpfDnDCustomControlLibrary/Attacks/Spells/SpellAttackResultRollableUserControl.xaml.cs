using DnDToolsLibrary.Attacks.Spells;
using DnDToolsLibrary.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace WpfDnDCustomControlLibrary.Attacks.Spells
{
    /// <summary>
    /// Interaction logic for SpellAttackResultRollableUserControl.xaml
    /// </summary>
    public partial class SpellAttackResultRollableUserControl : UserControl, INotifyPropertyChanged
    {
        private PlayableEntity data_context
        {
            get => (PlayableEntity)DataContext;
        }

        public SpellAttackResultRollableUserControl()
        {
            InitializeComponent();
        }

        public NewAttackSpellResult SpellResult
        {
            get { return (NewAttackSpellResult)this.GetValue(SpellResultProperty); }
            set 
            {
                this.SetValue(SpellResultProperty, value);
                NotifyPropertyChanged();
            }
        }
        public static readonly DependencyProperty SpellResultProperty = DependencyProperty.Register(
          nameof(SpellResult),
          typeof(NewAttackSpellResult), 
          typeof(SpellAttackResultRollableUserControl), 
          new PropertyMetadata(null));

        #region INotifyPropertyChanged

        /// <summary>
        ///     PropertyChanged EventHandler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
