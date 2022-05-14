using System;
using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Entities;
using System.Windows;
using System.Windows.Input;
using BaseToolsLibrary.DependencyInjection;
using BaseToolsLibrary.IO;
using BaseToolsLibrary.Mediator;
using DnDToolsLibrary.Entities.EntitiesCommands.DamageCommand.ApplyDamageResultList;
using TempExtensionsPlayableEntity;
using WpfToolsLibrary.Extensions;

namespace DDFight.Windows.FightWindows
{
    /// <summary>
    /// Logique d'interaction pour TakeDamageWindow.xaml
    /// </summary>
    public partial class TakeDamageWindow : Window
    {
        private static Lazy<IMediator> _lazyMediator = new (DIContainer.GetImplementation<IMediator>());
        private static IMediator _mediator => _lazyMediator.Value;

        private PlayableEntity data_context {
            get => (PlayableEntity)DataContext;
        }

        DamageTemplateList damage_list = new DamageTemplateList();

        public TakeDamageWindow()
        {
            InitializeComponent();
            
            Loaded += TakeDamageWindow_Loaded;
        }

        private void TakeDamageWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DamageControl.DataContext = damage_list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!this.AreAllChildrenValid())
            {
                ErrorControl.Visibility = Visibility.Visible;
                return;
            }

            if (damage_list.Count != 0)
            {
                var dmgList = damage_list.GetResultList();
                foreach (var dmg in dmgList)
                {
                    dmg.Damage.Roll();
                }

                var damageResultListCommand = new ApplyDamageResultListCommand(data_context, dmgList);
                _mediator.Execute(damageResultListCommand);
            }
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }
    }
}
