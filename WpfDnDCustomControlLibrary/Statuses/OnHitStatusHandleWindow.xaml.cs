using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Dice;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WpfCustomControlLibrary.ModalWindows;
using WpfToolsLibrary.Extensions;
using WpfToolsLibrary.Navigation;

namespace WpfDnDCustomControlLibrary.Statuses
{
    /// <summary>
    ///     This window is to be used either for Savings (be it application or resist later on) and for damage application.
    /// </summary>
    public partial class OnHitStatusHandleWindow : Window, INotifyPropertyChanged
    {
        private OnHitStatus data_context
        {
            get => DataContext as OnHitStatus;
        }

        public SavingThrow Saving 
        {
            get => _saving;
            set {
                _saving = value;
                NotifyPropertyChanged();
            }
        }
        private SavingThrow _saving = null;

        public DamageResultList DamageList
        {
            get => _damageList;
            set
            {
                _damageList = value;
                NotifyPropertyChanged();
            }
        }
        private DamageResultList _damageList = null;

        /// <summary>
        /// </summary>
        /// <param name="application"> tells wether it is the application or a recurrent event </param>
        public OnHitStatusHandleWindow(bool application)
        {
            _application = application;
            Loaded += OnHitStatusHandleWindow_Loaded;
            InitializeComponent();
        }

        private void OnHitStatusHandleWindow_Loaded(object sender, EventArgs e)
        {
            DataContextChanged += OnHitStatusHandleWindow_DataContextChanged;
            refresh_all();
        }

        private void OnHitStatusHandleWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            refresh_all();
        }

        public bool Validated = false;
        private bool selfClosing = false;
        private readonly bool _application;

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            selfClosing = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (selfClosing == false)
            {
                YesNoWindow window = new YesNoWindow { 
                    Title = "Are you sure?",
                    Text = "Are you sure you wish to cancel this?"
                };
                window.ShowCentered();
                if (window.Validated == false)
                    e.Cancel = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.IsRollControlPressed(e))
            {
                this.RollRollableChildren();
                e.Handled = true;
            }
        }

        #region RefreshMethods
        private void refresh_all()
        {
            refresh_saving();
            refresh_damage();
        }

        private void refresh_saving()
        {
            if (data_context != null)
            {
                Saving = data_context.GetSavingThrow(data_context.Caster, data_context.Affected);
            }
        }

        private void refresh_damage()
        {
            if (data_context != null && IsLoaded)
                if (_application)
                    DamageList = data_context.OnApplyDamageList.GetResultList();
                else
                    DamageList = data_context.DotDamageList.GetResultList();
        }

        #endregion RefreshMethods

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
